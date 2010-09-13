package org.nhindirect.config.service.ws;
/* 
Copyright (c) 2010, NHIN Direct Project
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer 
   in the documentation and/or other materials provided with the distribution.  
3. Neither the name of the The NHIN Direct Project (nhindirect.org) nor the names of its contributors may be used to endorse or promote 
   products derived from this software without specific prior written permission.
   
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS 
BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
THE POSSIBILITY OF SUCH DAMAGE.
*/
import java.util.List;

import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.xml.ws.FaultAction;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.nhindirect.config.service.AddressService;
import org.nhindirect.config.service.ConfigurationFault;
import org.nhindirect.config.service.ConfigurationServiceException;
import org.nhindirect.config.store.Address;
import org.nhindirect.config.store.EntityStatus;
import org.nhindirect.config.store.dao.AddressDao;
import org.nhindirect.config.store.dao.DomainDao;
import org.springframework.beans.factory.annotation.Autowired;

@WebService(endpointInterface = "org.nhindirect.config.service.AddressService")
public class AddressServiceWS implements AddressService {

	private static final Log log = LogFactory.getLog(AddressServiceWS.class);
	
	private AddressDao dao;
	
	public void init() {
		log.info("AddressService initialized");
	}
	
	@FaultAction(className=ConfigurationFault.class)
	public void addAddress(List<Address> Address) throws ConfigurationServiceException {
		// TODO Auto-generated method stub

	}

	@FaultAction(className=ConfigurationFault.class)
	public void updateAddress(Address Address) throws ConfigurationServiceException {
		// TODO Auto-generated method stub

	}

	@FaultAction(className=ConfigurationFault.class)
	public int getAddressCount() throws ConfigurationServiceException {
		// TODO Auto-generated method stub
		return 0;
	}

	@FaultAction(className=ConfigurationFault.class)
	public List<Address> getAddress(List<String> AddressNames,
			EntityStatus status) throws ConfigurationServiceException {
		// TODO Auto-generated method stub
		return null;
	}

	@FaultAction(className=ConfigurationFault.class)
	public void removeAddress(String AddressName) throws ConfigurationServiceException {
		// TODO Auto-generated method stub

	}

	@FaultAction(className=ConfigurationFault.class)
	public List<Address> ListAddresss(String lastAddressName, int maxResults) throws ConfigurationServiceException {
		// TODO Auto-generated method stub
		return null;
	}

	@Autowired
	public void setDao(AddressDao dao) {
		this.dao = dao;
	}

}
