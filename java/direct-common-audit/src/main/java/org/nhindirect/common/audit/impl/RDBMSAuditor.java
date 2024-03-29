/* 
Copyright (c) 2010, NHIN Direct Project
All rights reserved.

Authors:
   Greg Meyer      gm2552@cerner.com
 
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer 
in the documentation and/or other materials provided with the distribution.  Neither the name of the The NHIN Direct Project (nhindirect.org). 
nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS 
BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
THE POSSIBILITY OF SUCH DAMAGE.
*/

package org.nhindirect.common.audit.impl;

import java.lang.management.ManagementFactory;
import java.util.Calendar;
import java.util.Collection;
import java.util.Locale;
import java.util.UUID;
import java.util.Vector;

import javax.management.JMException;
import javax.management.MBeanServer;
import javax.management.ObjectName;
import javax.management.StandardMBean;
import javax.management.openmbean.ArrayType;
import javax.management.openmbean.CompositeData;
import javax.management.openmbean.CompositeDataSupport;
import javax.management.openmbean.CompositeType;
import javax.management.openmbean.OpenDataException;
import javax.management.openmbean.OpenType;
import javax.management.openmbean.SimpleType;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.Query;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.nhindirect.common.audit.AbstractAuditor;
import org.nhindirect.common.audit.AuditContext;
import org.nhindirect.common.audit.AuditEvent;
import org.nhindirect.common.audit.AuditorMBean;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

/**
 * Implementation of the DirectProject RI auditor that writes records to a configurable database.
 * Also implements the AuditorMBean interface for management access to audit events.
 * @author Greg Meyer
 * @since 1.0
 */
@Repository
public class RDBMSAuditor extends AbstractAuditor implements RDBMSDao
{
	private final Log LOGGER = LogFactory.getFactory().getInstance(RDBMSAuditor.class);
	
    @PersistenceContext
    @Autowired
    private EntityManager entityManager;
    
	private String[] itemNames;
	private CompositeType eventType;
    
	/**
	 * Constructor
	 */
    public RDBMSAuditor()
    {
		// register the auditor as an MBean
		registerMBean();
    }
    
    /**
     * Constructor 
     * @param entityManager Entity manager for accessing the underlying data store medium
     */
    ///CLOVER:OFF
    public RDBMSAuditor(EntityManager entityManager)
    {
    	super();
    	
    	setEntityManager(entityManager);
    }
    ///CLOVER:ON
    
	/**
	 * Sets the entity manager for access to the underlying data store medium.
	 * @param entityManager The entity manager.
	 */
    ///CLOVER:OFF
	public void setEntityManager(EntityManager entityManager)
	{
		this.entityManager = entityManager;
	}
    ///CLOVER:ON
	
	/**
	 * Validate that we have a connection to the DAO
	 */
	protected void validateState()
	{	
    	if (entityManager == null)
    		throw new IllegalStateException("entityManger has not been initialized");
	}
	
	
	/*
	 * Register the MBean
	 */
	private void registerMBean()
	{
		
		LOGGER.info("Registering RDBMSAuditor MBean");
		
		try
		{
			itemNames = new String[] {"Event Id", "Event Time", "Event Principal", "Event Name", "Event Type", "Contexts"};
			
			OpenType<?>[] types = {SimpleType.STRING, SimpleType.STRING, SimpleType.STRING, SimpleType.STRING, 
					SimpleType.STRING, ArrayType.getArrayType(SimpleType.STRING)};
			
			eventType = new CompositeType("AuditEvent", "Direct Auditable Event", itemNames, itemNames, types);
		}
		catch (OpenDataException e)
		{
			LOGGER.error("Failed to create settings composite type: " + e.getLocalizedMessage(), e);
			return;
		}
		
		final Class<?> clazz = this.getClass();
		final StringBuilder objectNameBuilder = new StringBuilder(clazz.getPackage().getName());
		objectNameBuilder.append(":type=").append(clazz.getSimpleName());
		objectNameBuilder.append(",name=").append(UUID.randomUUID());
				
		try
		{			
			final StandardMBean mbean = new StandardMBean(this, AuditorMBean.class);
		
			final MBeanServer mbeanServer = ManagementFactory.getPlatformMBeanServer();
			mbeanServer.registerMBean(mbean, new ObjectName(objectNameBuilder.toString()));
		}
		catch (JMException e)
		{
			LOGGER.error("Unable to register the RDBMSAuditors MBean", e);
		}		
	}
	
	/**
	 * {@inheritDoc}
	 */
	@Override
	@Transactional(readOnly = false)	
	public void writeEvent(UUID eventId, Calendar eventTimeStamp,
			String principal, AuditEvent event, Collection<? extends AuditContext> contexts) 
	{
    	validateState();

		final org.nhindirect.common.audit.impl.entity.AuditEvent newEvent =
				new org.nhindirect.common.audit.impl.entity.AuditEvent();

		newEvent.setEventName(event.getName());
		newEvent.setEventType(event.getType());

		newEvent.setEventTime(Calendar.getInstance(Locale.getDefault()));
		newEvent.setPrincipal(principal);
		newEvent.setUUID(eventId.toString());

		if (contexts != null)
		{
			final Collection<org.nhindirect.common.audit.impl.entity.AuditContext> entityContexts = newEvent.getAuditContexts();
			for (AuditContext context : contexts)
			{
				final org.nhindirect.common.audit.impl.entity.AuditContext newContext = 
						new org.nhindirect.common.audit.impl.entity.AuditContext();
				
				newContext.setContextName(context.getContextName());
				newContext.setContextValue(context.getContextValue());
				newContext.setAuditEvent(newEvent);
				entityContexts.add(newContext);
			}
		}
		
		entityManager.persist(newEvent);
		entityManager.flush();
	}
	

	/**
	 * {@inheritDoc}
	 */
	@Override
	@Transactional(readOnly = true)	
	public Integer getEventCount() 
	{
    	validateState();
    	
    	final Query select = entityManager.createQuery("SELECT count(ae) from AuditEvent ae");
    	
    	return new Integer(((Long)select.getSingleResult()).intValue());
	}

	/**
	 * {@inheritDoc}
	 */
	@SuppressWarnings("unchecked")
	@Override
	public CompositeData[] getEvents(Integer eventCount) 
	{
		if (eventType == null || eventCount == 0)
			return null;
		
		final Vector<CompositeData> retVal = new Vector<CompositeData>();
		
    	final Query select = 
    			entityManager.createQuery("SELECT ae from AuditEvent ae ORDER BY ae.eventTime desc").setMaxResults(eventCount);
		
        final Collection<org.nhindirect.common.audit.impl.entity.AuditEvent> rs = select.getResultList();
        if (rs.size() == 0)
        	return null;
        
        for (org.nhindirect.common.audit.impl.entity.AuditEvent event : rs)
        {
        	String[] contexts = null;
        	
        	if (event.getAuditContexts() != null && !event.getAuditContexts().isEmpty())
        	{
        		contexts = new String[event.getAuditContexts().size()];
        		int idx = 0;
				for (org.nhindirect.common.audit.impl.entity.AuditContext ctx : event.getAuditContexts())
				{
					contexts[idx++] = ctx.getContextName() + ":" + ctx.getContextValue();
				}
        	}
				
			if (contexts == null)
				contexts = new String[] {" "};

			try
			{
			
				final Object[] eventValues = {event.getUUID(), event.getEventTime().toString(), event.getPrincipal(), 
						event.getEventName(), event.getEventType(), contexts};
				
				retVal.add(new CompositeDataSupport(eventType, itemNames, eventValues));
			}
			catch (OpenDataException e)
			{
				LOGGER.error("Error create composit data for audit event.", e);
			}
        }
        
		return retVal.toArray(new CompositeData[retVal.size()]);
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public CompositeData getLastEvent() 
	{
		final CompositeData[] events = getEvents(1); 

		if (events == null)
			return null;
		
		return events[0];
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public void clear() 
	{
		// truncating the tables
		
    	Query delete = 
    			entityManager.createQuery("delete from AuditContext");
    	
    	delete.executeUpdate();
    	
    	delete = 
    			entityManager.createQuery("delete from AuditEvent");
    	
    	delete.executeUpdate();
    	
    	entityManager.flush();
		
	}
}
