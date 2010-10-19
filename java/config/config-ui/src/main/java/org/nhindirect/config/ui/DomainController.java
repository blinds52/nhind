package org.nhindirect.config.ui;
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
import java.io.IOException;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;

import javax.inject.Inject;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import org.apache.commons.io.FileUtils;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.nhindirect.config.service.AddressService;
import org.nhindirect.config.service.AnchorService;
import org.nhindirect.config.service.CertificateService;
import org.nhindirect.config.service.ConfigurationServiceException;
import org.nhindirect.config.service.DomainService;
import org.nhindirect.config.service.impl.CertificateGetOptions;
import org.nhindirect.config.store.Domain;
import org.nhindirect.config.store.Address;
import org.nhindirect.config.store.EntityStatus;
import org.nhindirect.config.ui.form.DomainForm;
import org.nhindirect.config.ui.form.AddressForm;
import org.nhindirect.config.ui.form.LoginForm;
import org.nhindirect.config.ui.form.SearchDomainForm;
import org.nhindirect.config.ui.form.SimpleForm;
import org.nhindirect.config.ui.form.AnchorForm;
import org.nhindirect.config.ui.form.CertificateForm;
import org.nhindirect.config.ui.util.AjaxUtils;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.util.ClassUtils;
import org.springframework.web.bind.ServletRequestDataBinder;
import org.springframework.web.bind.WebDataBinder;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.InitBinder;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.view.RedirectView;
import org.nhindirect.config.store.Certificate;
import org.nhindirect.config.store.Anchor;
import java.io.FileOutputStream;

import java.io.File;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.multipart.support.ByteArrayMultipartFileEditor;
import org.springframework.validation.BindingResult;

@Controller
@RequestMapping("/domain")
public class DomainController {
	private final Log log = LogFactory.getLog(getClass());
	private static final String certBasePath = "c:/";	
	@Inject
	private DomainService dService;

	@Inject
	private AddressService aService;
	
	@Inject
	private AnchorService anchorService;
	
	@Inject
	private CertificateService certService;

	public DomainController() {
		if (log.isDebugEnabled()) log.debug("DomainController initialized");
	}
	
	@RequestMapping(value="/addanchor", method = RequestMethod.POST)
	public ModelAndView addAnchor (
								@RequestHeader(value="X-Requested-With", required=false) String requestedWith, 
						        HttpSession session,
						        @ModelAttribute AnchorForm anchorForm,
						        Model model,
						        @RequestParam(value="submitType") String actionPath
						        ) { 		

		ModelAndView mav = new ModelAndView(); 
		String strid = "";
		if (log.isDebugEnabled()) log.debug("Enter domain/addanchor");
		if (isLoggedIn(session)) {
			if(actionPath.equalsIgnoreCase("newanchor")){
				strid = ""+anchorForm.getId();
				Domain dom = dService.getDomain(Long.parseLong(strid));
				String owner = "";
				if(dom != null){
					owner = dom.getPostMasterEmail();
				}
				// insert the new address into the Domain list of Addresses
				EntityStatus estatus = anchorForm.getStatus();
				if (log.isDebugEnabled()) log.debug("beginning to evaluate filedata");		
				try{
					if (!anchorForm.getFileData().isEmpty()) {
						byte[] bytes = anchorForm.getFileData().getBytes();
						// store the bytes somewhere
						owner = anchorForm.getOwner();
						Anchor ank = new Anchor();
						ank.setData(bytes);
						ank.setIncoming(anchorForm.isIncoming());
						ank.setOutgoing(anchorForm.isOutgoing());
						ank.setOwner(owner);
						ank.setStatus(anchorForm.getStatus());

						ArrayList<Anchor> anchorlist = new ArrayList<Anchor>();
						anchorlist.add(ank);
						
						anchorService.addAnchors(anchorlist);
						if (log.isDebugEnabled()) log.debug("store the anchor certificate into database");
					} else {
						if (log.isDebugEnabled()) log.debug("DO NOT store the anchor certificate into database BECAUSE THERE IS NO FILE");
					}

				} catch (ConfigurationServiceException ed) {
					if (log.isDebugEnabled())
						log.error(ed);
				} catch (Exception e) {
					if (log.isDebugEnabled()) log.error(e.getMessage());
					e.printStackTrace();
				}
				// certificate and anchor forms and results
				try {
					Collection<Certificate> certs = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
					model.addAttribute("certificatesResults", certs);
					 
					Collection<Anchor> anchors = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
					model.addAttribute("anchorsResults", anchors);
					
					CertificateForm cform = new CertificateForm();
					cform.setId(dom.getId());
					model.addAttribute("certificateForm",cform);
					
					AnchorForm aform = new AnchorForm();
					aform.setId(dom.getId());
					model.addAttribute("anchorForm",aform);
					
				} catch (ConfigurationServiceException e1) {
					e1.printStackTrace();
				}
				model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
				SimpleForm simple = new SimpleForm();
				simple.setId(Long.parseLong(strid));
				model.addAttribute("simpleForm",simple);
	
				model.addAttribute("addressesResults", dom.getAddresses());
				mav.setViewName("domain"); 
				// the Form's default button action
				String action = "Update";
				DomainForm form = (DomainForm) session.getAttribute("domainForm");
				if (form == null) {
					form = new DomainForm();
					form.populate(dom);
				}
				model.addAttribute("domainForm", form);
				model.addAttribute("action", action);
				model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
		
				mav.addObject("statusList", EntityStatus.getEntityStatusList());
			}
		}else{
			model.addAttribute(new LoginForm());
			mav.setViewName("login");
			mav.setView(new RedirectView("/config-ui/config/login", false));
		}
		AddressForm addressForm2 = new AddressForm();
		
		addressForm2.setDisplayName("");
		addressForm2.setEmailAddress("");
		addressForm2.setType("");
		addressForm2.setId(Long.parseLong(strid));
		
		model.addAttribute("addressForm",addressForm2);
		
		return mav;
	}			
	
	@RequestMapping(value="/removeanchors", method = RequestMethod.POST)
	public ModelAndView removeAnchors (@RequestHeader(value="X-Requested-With", required=false) String requestedWith, 
						        HttpSession session,
						        @ModelAttribute AnchorForm simpleForm,
						        Model model,
						        @RequestParam(value="submitType") String actionPath)  { 		

		ModelAndView mav = new ModelAndView(); 
	
		if (log.isDebugEnabled()) log.debug("Enter domain/removeanchor");
		if(simpleForm.getRemove() != null){
			if (log.isDebugEnabled()) log.debug("the list of checkboxes checked or not is: "+simpleForm.getRemove().toString());
		}
		if (isLoggedIn(session)) {
			String strid = ""+simpleForm.getId();
			Domain dom = dService.getDomain(Long.parseLong(strid));
			String owner = "";
			String domname = "";
			if( dom != null){
				domname = dom.getPostMasterEmail();
				owner = domname;
			}
			if (dService != null && simpleForm != null && actionPath != null && actionPath.equalsIgnoreCase("deleteanchors") && simpleForm.getRemove() != null) {
				int cnt = simpleForm.getRemove().size();
				if (log.isDebugEnabled()) log.debug("removing anchors for domain with name: " + domname);
				try{
					// get list of certificates for this domain
					Collection<Anchor> certs = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
					ArrayList<Long> certtoberemovedlist = new ArrayList<Long>();
					// now iterate over each one and remove the appropriate ones
					for (int x = 0; x < cnt; x++) {
						String removeid = simpleForm.getRemove().get(x);
						for (Iterator iter = certs.iterator(); iter.hasNext();) {
							Anchor t = (Anchor) iter.next();
							   //rest of the code block removed
					    	if(t.getId() == Long.parseLong(removeid)){
						    	if (log.isDebugEnabled()){
						    		log.debug(" ");
						    		log.debug("domain address id: " + t.getId());
						    		log.debug(" ");
						    	}
						    	// create a collection of matching anchor ids
						    	certtoberemovedlist.add(t.getId());
						    	break;
					    	}
						}			
					}
					// with the collection of anchor ids now remove them from the anchorService
					if (log.isDebugEnabled()) log.debug(" Trying to remove anchors from database");
					anchorService.removeAnchors(certtoberemovedlist);
		    		if (log.isDebugEnabled()) log.debug(" SUCCESS Trying to update the domain with removed anchors");
					AddressForm addrform = new AddressForm();
					addrform.setId(dom.getId());
					model.addAttribute("addressForm",addrform);
				} catch (ConfigurationServiceException e) {
					if (log.isDebugEnabled())
						log.error(e);
				}
			}
			model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
			// BEGIN: temporary code for mocking purposes
			CertificateForm cform = new CertificateForm();
			cform.setId(dom.getId());
			model.addAttribute("certificateForm",cform);
			
			AnchorForm aform = new AnchorForm();
			aform.setId(dom.getId());
			model.addAttribute("anchorForm",aform);
			
			
			model.addAttribute("addressesResults", dom.getAddresses());
			mav.setViewName("domain"); 
			// the Form's default button action
			String action = "Update";
			DomainForm form = (DomainForm) session.getAttribute("domainForm");
			if (form == null) {
				form = new DomainForm();
				form.populate(dom);
			}
			model.addAttribute("domainForm", form);
			model.addAttribute("action", action);
			model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
			mav.addObject("action", action);
	
			// SETTING THE ADDRESSES OBJECT
			model.addAttribute("addressesResults", form.getAddresses());
			Collection<Certificate> certlist = null;
			try {
				certlist = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
			} catch (ConfigurationServiceException e) {
				e.printStackTrace();
			}
			
			Collection<Anchor> anchorlist = null;
			try {
				anchorlist = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
			} catch (ConfigurationServiceException e) {
				e.printStackTrace();
			}
			
			model.addAttribute("certificatesResults", certlist);
			model.addAttribute("anchorsResults", anchorlist);			
			// END: temporary code for mocking purposes			
			mav.addObject("statusList", EntityStatus.getEntityStatusList());
		}else{
			model.addAttribute(new LoginForm());
			mav.setViewName("login");
			mav.setView(new RedirectView("/config-ui/config/login", false));
		}
		model.addAttribute("simpleForm",simpleForm);
		String strid = ""+simpleForm.getId();
		if (log.isDebugEnabled()) log.debug(" the value of id of simpleform is: "+strid);
		
		return mav;
	}			
		
	
	
	@RequestMapping(value="/addcertificate", method = RequestMethod.POST)
	public ModelAndView addCertificate (
								@RequestHeader(value="X-Requested-With", required=false) String requestedWith, 
						        HttpSession session,
						        @ModelAttribute CertificateForm certificateForm,
						        Model model,
						        @RequestParam(value="submitType") String actionPath
						        ) { 		

		ModelAndView mav = new ModelAndView(); 
		String strid = "";
		if (log.isDebugEnabled()) log.debug("Enter domain/addcertificate");
		if (isLoggedIn(session)) {
			if(actionPath.equalsIgnoreCase("newcertificate")){
				strid = ""+certificateForm.getId();
				Domain dom = dService.getDomain(Long.parseLong(strid));
				String owner = "";
				if(dom != null){
					owner = dom.getPostMasterEmail();
				}
				// insert the new address into the Domain list of Addresses
				EntityStatus estatus = certificateForm.getStatus();
				if (log.isDebugEnabled()) log.debug("beginning to evaluate filedata");		
				try{
					if (!certificateForm.getFileData().isEmpty()) {
						byte[] bytes = certificateForm.getFileData().getBytes();
						owner = certificateForm.getOwner();
						Certificate cert = new Certificate();
						cert.setData(bytes);
						cert.setOwner(owner);
						cert.setStatus(certificateForm.getStatus());

						ArrayList<Certificate> certlist = new ArrayList<Certificate>();
						certlist.add(cert);
						certService.addCertificates(certlist);
						// store the bytes somewhere
						if (log.isDebugEnabled()) log.debug("store the certificate into database");
					} else {
						if (log.isDebugEnabled()) log.debug("DO NOT store the certificate into database BECAUSE THERE IS NO FILE");
					}

				} catch (ConfigurationServiceException ed) {
					if (log.isDebugEnabled())
						log.error(ed);
				} catch (Exception e) {
					if (log.isDebugEnabled()) log.error(e);
					e.printStackTrace();
				}
				// certificate and anchor forms and results
				try {
					Collection<Certificate> certs = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
					model.addAttribute("certificatesResults", certs);
					 
					Collection<Anchor> anchors = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
					model.addAttribute("anchorsResults", anchors);
					
					CertificateForm cform = new CertificateForm();
					cform.setId(dom.getId());
					model.addAttribute("certificateForm",cform);
					
					AnchorForm aform = new AnchorForm();
					aform.setId(dom.getId());
					model.addAttribute("anchorForm",aform);
					
				} catch (ConfigurationServiceException e1) {
					e1.printStackTrace();
				}
				model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
				SimpleForm simple = new SimpleForm();
				simple.setId(Long.parseLong(strid));
				model.addAttribute("simpleForm",simple);
	
				model.addAttribute("addressesResults", dom.getAddresses());
				mav.setViewName("domain"); 
				// the Form's default button action
				String action = "Update";
				DomainForm form = (DomainForm) session.getAttribute("domainForm");
				if (form == null) {
					form = new DomainForm();
					form.populate(dom);
				}
				model.addAttribute("domainForm", form);
				model.addAttribute("action", action);
				model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
		
				mav.addObject("statusList", EntityStatus.getEntityStatusList());
			}
		}else{
			model.addAttribute(new LoginForm());
			mav.setViewName("login");
			mav.setView(new RedirectView("/config-ui/config/login", false));
		}
		AddressForm addressForm2 = new AddressForm();
		
		addressForm2.setDisplayName("");
		addressForm2.setEmailAddress("");
		addressForm2.setType("");
		addressForm2.setId(Long.parseLong(strid));
		
		model.addAttribute("addressForm",addressForm2);
		
		return mav;
	}			
	
	@RequestMapping(value="/removecertifcates", method = RequestMethod.POST)
	public ModelAndView removeCertificates (@RequestHeader(value="X-Requested-With", required=false) String requestedWith, 
						        HttpSession session,
						        @ModelAttribute CertificateForm simpleForm,
						        Model model,
						        @RequestParam(value="submitType") String actionPath)  { 		

		ModelAndView mav = new ModelAndView(); 
	
		if (log.isDebugEnabled()) log.debug("Enter domain/removecertificates");
		if(simpleForm.getRemove() != null){
			if (log.isDebugEnabled()) log.debug("the list of checkboxes checked or not is: "+simpleForm.getRemove().toString());
		}
		if (isLoggedIn(session)) {
			String strid = ""+simpleForm.getId();
			Domain dom = dService.getDomain(Long.parseLong(strid));
			String owner = "";
			String domname = "";
			if( dom != null){
				domname = dom.getPostMasterEmail();
				owner = domname;
			}
			if (dService != null && simpleForm != null && actionPath != null && actionPath.equalsIgnoreCase("deletecertificate") && simpleForm.getRemove() != null) {
				int cnt = simpleForm.getRemove().size();
				if (log.isDebugEnabled()) log.debug("removing certificates for domain with name: " + domname);
				try{
					// get list of certificates for this domain
					Collection<Certificate> certs = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
					ArrayList<Long> certtoberemovedlist = new ArrayList<Long>();
					// now iterate over each one and remove the appropriate ones
					for (int x = 0; x < cnt; x++) {
						String removeid = simpleForm.getRemove().get(x);
						for (Iterator iter = certs.iterator(); iter.hasNext();) {
							   Certificate t = (Certificate) iter.next();
							   //rest of the code block removed
					    	if(t.getId() == Long.parseLong(removeid)){
						    	if (log.isDebugEnabled()){
						    		log.debug(" ");
						    		log.debug("domain address id: " + t.getId());
						    		log.debug(" ");
						    	}
						    	// create a collection of matching anchor ids
						    	certtoberemovedlist.add(t.getId());
						    	break;
					    	}
						}			
					}
					// with the collection of anchor ids now remove them from the anchorService
					if (log.isDebugEnabled()) log.debug(" Trying to remove certificates from database");
					certService.removeCertificates(certtoberemovedlist);
		    		if (log.isDebugEnabled()) log.debug(" SUCCESS Trying to update the domain with removed certificates");
					AddressForm addrform = new AddressForm();
					addrform.setId(dom.getId());
					model.addAttribute("addressForm",addrform);
				} catch (ConfigurationServiceException e) {
					if (log.isDebugEnabled())
						log.error(e);
				}
			}
			model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
			// BEGIN: temporary code for mocking purposes
			CertificateForm cform = new CertificateForm();
			cform.setId(dom.getId());
			model.addAttribute("certificateForm",cform);
			
			AnchorForm aform = new AnchorForm();
			aform.setId(dom.getId());
			model.addAttribute("anchorForm",aform);
			
			
			model.addAttribute("addressesResults", dom.getAddresses());
			mav.setViewName("domain"); 
			// the Form's default button action
			String action = "Update";
			DomainForm form = (DomainForm) session.getAttribute("domainForm");
			if (form == null) {
				form = new DomainForm();
				form.populate(dom);
			}
			model.addAttribute("domainForm", form);
			model.addAttribute("action", action);
			model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
			mav.addObject("action", action);
	
			// SETTING THE ADDRESSES OBJECT
			model.addAttribute("addressesResults", form.getAddresses());
			Collection<Certificate> certlist = null;
			try {
				certlist = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
			} catch (ConfigurationServiceException e) {
				e.printStackTrace();
			}
			
			Collection<Anchor> anchorlist = null;
			try {
				anchorlist = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
			} catch (ConfigurationServiceException e) {
				e.printStackTrace();
			}
			
			model.addAttribute("certificatesResults", certlist);
			model.addAttribute("anchorsResults", anchorlist);			
			// END: temporary code for mocking purposes			
			mav.addObject("statusList", EntityStatus.getEntityStatusList());
		}else{
			model.addAttribute(new LoginForm());
			mav.setViewName("login");
			mav.setView(new RedirectView("/config-ui/config/login", false));
		}
		model.addAttribute("simpleForm",simpleForm);
		String strid = ""+simpleForm.getId();
		if (log.isDebugEnabled()) log.debug(" the value of id of simpleform is: "+strid);
		
		return mav;
	}			
	
	@RequestMapping(value="/addaddress", method = RequestMethod.POST)
	public ModelAndView addAddress (@RequestHeader(value="X-Requested-With", required=false) String requestedWith, 
						        HttpSession session,
						        @ModelAttribute AddressForm addressForm,
						        Model model,
						        @RequestParam(value="submitType") String actionPath) { 		

		ModelAndView mav = new ModelAndView(); 
		String strid = "";
		if (log.isDebugEnabled()) log.debug("Enter domain/addaddress");
		if (isLoggedIn(session)) {
			if(actionPath.equalsIgnoreCase("newaddress")){
				strid = ""+addressForm.getId();
				Domain dom = dService.getDomain(Long.parseLong(strid));
				String owner = dom.getPostMasterEmail();
				// insert the new address into the Domain list of Addresses
				String anEmail = addressForm.getEmailAddress();
				String displayname = addressForm.getDisplayName();
				EntityStatus estatus = addressForm.getaStatus();
				String etype = addressForm.getType();
				
				if (log.isDebugEnabled()) log.debug(" Trying to add address: "+anEmail);
				Address e = new Address();
				e.setEmailAddress(anEmail);
				e.setDisplayName(displayname);
				e.setStatus(estatus);
				e.setType(etype);
				
				dom.getAddresses().add(e);
				
				try{
					dService.updateDomain(dom);
					if (log.isDebugEnabled()) log.debug(" After attempt to insert new email address ");
				} catch (ConfigurationServiceException ed) {
					if (log.isDebugEnabled())
						log.error(ed);
				}
				// certificate and anchor forms and results
				try {
					Collection<Certificate> certs = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
					model.addAttribute("certificatesResults", certs);
					 
					Collection<Anchor> anchors = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
					model.addAttribute("anchorsResults", anchors);
					
					CertificateForm cform = new CertificateForm();
					cform.setId(dom.getId());
					model.addAttribute("certificateForm",cform);
					
					AnchorForm aform = new AnchorForm();
					aform.setId(dom.getId());
					model.addAttribute("anchorForm",aform);
					
				} catch (ConfigurationServiceException e1) {
					e1.printStackTrace();
				}
				model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
				SimpleForm simple = new SimpleForm();
				simple.setId(Long.parseLong(strid));
				model.addAttribute("simpleForm",simple);
	
				model.addAttribute("addressesResults", dom.getAddresses());
				mav.setViewName("domain"); 
				// the Form's default button action
				String action = "Update";
				DomainForm form = (DomainForm) session.getAttribute("domainForm");
				if (form == null) {
					form = new DomainForm();
					form.populate(dom);
				}
				model.addAttribute("domainForm", form);
				model.addAttribute("action", action);
				model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
		
				mav.addObject("statusList", EntityStatus.getEntityStatusList());
			}
		}else{
			model.addAttribute(new LoginForm());
			mav.setViewName("login");
			mav.setView(new RedirectView("/config-ui/config/login", false));
		}
		AddressForm addressForm2 = new AddressForm();
		
		addressForm2.setDisplayName("");
		addressForm2.setEmailAddress("");
		addressForm2.setType("");
		addressForm2.setId(Long.parseLong(strid));
		
		model.addAttribute("addressForm",addressForm2);
		
		return mav;
	}		
	
	
	@RequestMapping(value="/removeaddresses", method = RequestMethod.POST)
	public ModelAndView removeAddresses (@RequestHeader(value="X-Requested-With", required=false) String requestedWith, 
						        HttpSession session,
						        @ModelAttribute SimpleForm simpleForm,
						        Model model,
						        @RequestParam(value="submitType") String actionPath)  { 		

		ModelAndView mav = new ModelAndView(); 
	
		if (log.isDebugEnabled()) log.debug("Enter domain/removeaddresses");
		if(simpleForm.getRemove() != null){
			if (log.isDebugEnabled()) log.debug("the list of checkboxes checked or not is: "+simpleForm.getRemove().toString());
		}
		if (isLoggedIn(session)) {
			String strid = ""+simpleForm.getId();
			Domain dom = dService.getDomain(Long.parseLong(strid));
			String domname = "";
			if( dom != null){
				domname = dom.getDomainName();
			}
			if (dService != null && simpleForm != null && actionPath != null && actionPath.equalsIgnoreCase("delete") && simpleForm.getRemove() != null) {
				int cnt = simpleForm.getRemove().size();
				if (log.isDebugEnabled()) log.debug("removing addresses for domain with name: " + domname);
				try{
					for (int x = 0; x < cnt; x++) {
						String removeid = simpleForm.getRemove().get(x);
					    for (Address t : dom.getAddresses()){
					    	if(t.getId() == Long.parseLong(removeid)){
						    	if (log.isDebugEnabled()){
						    		log.debug(" ");
						    		log.debug("domain address id: " + t.getId());
						    		log.debug(" ");
						    	}
					    		dom.getAddresses().remove(t);
					    		if(aService != null){
					    			if (log.isDebugEnabled()) log.debug("Address Service is not null. Now trying to remove address: "+t.getEmailAddress());
					    			aService.removeAddress(t.getEmailAddress());
					    		}
						    	if (log.isDebugEnabled()){
						    		log.debug(" REMOVED ");
						    		log.debug(" ");
						    		break;
						    	}
					    	}
						}			
					}
					if (log.isDebugEnabled()) log.debug(" Trying to update the domain with removed addresses");
					dService.updateDomain(dom);
					dom = dService.getDomain(Long.parseLong(strid));
		    		if (log.isDebugEnabled()) log.debug(" SUCCESS Trying to update the domain with removed addresses");
					AddressForm addrform = new AddressForm();
					addrform.setId(dom.getId());
					model.addAttribute("addressForm",addrform);
					// BEGIN: temporary code for mocking purposes
					String owner = "";
					owner = dom.getPostMasterEmail();
					model.addAttribute("addressesResults", dom.getAddresses());

					Collection<Certificate> certlist = null;
					try {
						certlist = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
					} catch (ConfigurationServiceException e) {
						e.printStackTrace();
					}
					
					Collection<Anchor> anchorlist = null;
					try {
						anchorlist = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
					} catch (ConfigurationServiceException e) {
						e.printStackTrace();
					}
					
					model.addAttribute("certificatesResults", certlist);
					model.addAttribute("anchorsResults", anchorlist);			
				
					// END: temporary code for mocking purposes			
					
				} catch (ConfigurationServiceException e) {
					if (log.isDebugEnabled())
						log.error(e);
				}
			}else if (dService != null && actionPath.equalsIgnoreCase("newaddress")) {
				// insert the new address into the Domain list of Addresses
				String anEmail = simpleForm.getPostmasterEmail();
				if (log.isDebugEnabled()) log.debug(" Trying to add address: "+anEmail);
				Address e = new Address();
				e.setEmailAddress(anEmail);
				dom.getAddresses().add(e);
				simpleForm.setPostmasterEmail("");
				try{
					dService.updateDomain(dom);
					if (log.isDebugEnabled()) log.debug(" After attempt to insert new email address ");
				} catch (ConfigurationServiceException ed) {
					if (log.isDebugEnabled())
						log.error(ed);
				}
			}
			model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
			mav.addObject("statusList", EntityStatus.getEntityStatusList());
			String action = "Update";
			model.addAttribute("action", action);			
			DomainForm form = (DomainForm) session.getAttribute("domainForm");
			if (form == null) {
				form = new DomainForm();
				form.populate(dom);
			}
			model.addAttribute("domainForm", form);
			mav.setViewName("domain");
			String owner = dom.getPostMasterEmail();
			// certificate and anchor forms and results
			try {
				Collection<Certificate> certs = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
				model.addAttribute("certificatesResults", certs);
				 
				Collection<Anchor> anchors = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
				model.addAttribute("anchorsResults", anchors);
				
				CertificateForm cform = new CertificateForm();
				cform.setId(dom.getId());
				model.addAttribute("certificateForm",cform);
				
				AnchorForm aform = new AnchorForm();
				aform.setId(dom.getId());
				model.addAttribute("anchorForm",aform);
				
			} catch (ConfigurationServiceException e1) {
				e1.printStackTrace();
			}
			
		}else{
			model.addAttribute(new LoginForm());
			mav.setViewName("login");
			mav.setView(new RedirectView("/config-ui/config/login", false));
		}
		model.addAttribute("simpleForm",simpleForm);
		String strid = ""+simpleForm.getId();
		if (log.isDebugEnabled()) log.debug(" the value of id of simpleform is: "+strid);
		 
		return mav;
	}		
	
	@RequestMapping(value="/remove", method = RequestMethod.POST)
	public ModelAndView removeDomain (@RequestHeader(value="X-Requested-With", required=false) String requestedWith, 
						        HttpSession session,
						        @ModelAttribute SimpleForm simpleForm,
						        Model model,
						        @RequestParam(value="submitType") String actionPath)  { 		

		ModelAndView mav = new ModelAndView(); 
	
		if (log.isDebugEnabled()) log.debug("Enter domain/remove");
		if (log.isDebugEnabled()) log.debug("the list of checkboxes checked or not is: "+simpleForm.getRemove().toString());
		if (isLoggedIn(session)) {
			if (dService != null) {
				int cnt = simpleForm.getRemove().size();
				for (int x = 0; x < cnt; x++) {
					try {
						String strid = simpleForm.getRemove().remove(x);
						Domain dom = dService.getDomain(Long.parseLong(strid));
						String domname = dom.getDomainName();
						if (log.isDebugEnabled()) log.debug("removing domain with name: " + domname);
						dService.removeDomain(domname);
					} catch (ConfigurationServiceException e) {
						if (log.isDebugEnabled())
							log.error(e);
					}
				}
			}
			SearchDomainForm form2 = (SearchDomainForm) session.getAttribute("searchDomainForm");
			model.addAttribute(form2 != null ? form2 : new SearchDomainForm());
			model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
	
			mav.setViewName("main");
			mav.addObject("statusList", EntityStatus.getEntityStatusList());
		}else{
			model.addAttribute(new LoginForm());
			mav.setViewName("login");
			mav.setView(new RedirectView("/config-ui/config/login", false));
		}
		
		return mav;
	}	
	
	
	@RequestMapping(method = RequestMethod.POST)
	public ModelAndView onSubmitAndView(Object command){
		if (log.isDebugEnabled()) log.debug("Enter onSubmit");
		return new ModelAndView(new RedirectView("main"));
	}
	
    @RequestMapping(value = "/simpleForm", method = RequestMethod.GET)
    public void simpleForm(Model model) {
            model.addAttribute(new SimpleForm());
    }	
	/**
	 * Display a Domain
	 */
	@RequestMapping(method=RequestMethod.GET)
	public ModelAndView viewDomain (@RequestHeader(value="X-Requested-With", required=false) String requestedWith, 
							        @RequestParam(required=false) String id,
									HttpSession session, 
							        Model model) { 		
		if (log.isDebugEnabled()) log.debug("Enter");		
		ModelAndView mav = new ModelAndView(); 
		if (isLoggedIn(session)) {
			mav.setViewName("domain"); 
			
			// the Form's default button action
			String action = "Add";
			
			DomainForm form = (DomainForm) session.getAttribute("domainForm");
			if (form == null) {
				form = new DomainForm();
			}
			model.addAttribute("domainForm", form);
			model.addAttribute("action", action);
			model.addAttribute("ajaxRequest", AjaxUtils.isAjaxRequest(requestedWith));
			
			mav.addObject("action", action);
			mav.addObject("statusList", EntityStatus.getEntityStatusList());
			
			if ((id != null) &&
				(id.length() > 0)) {
				if (log.isDebugEnabled()) log.debug("Need to search for Domain ID: " + id);		
				
				Domain results = null;
				Long dId = Long.decode(id);
				
				AddressForm addrform = new AddressForm();
				addrform.setId(dId);
				model.addAttribute("addressForm",addrform);

				CertificateForm cform = new CertificateForm();
				cform.setId(dId);
				AnchorForm aform = new AnchorForm();
				aform.setId(dId);
				
				model.addAttribute("certificateForm",cform);
				model.addAttribute("anchorForm",aform);
				if (dService != null) {
					results = dService.getDomain(dId);
					if (results != null) {
						if (log.isDebugEnabled()) log.debug("Found a valid domain" + results.toString());		
						form.populate(results);
						action = "Update";
						model.addAttribute("action", action);
						// SETTING THE ADDRESSES OBJECT
						model.addAttribute("addressesResults", results.getAddresses());
						
						// BEGIN: temporary code for mocking purposes
						String owner = "";
						owner = results.getPostMasterEmail();
						model.addAttribute("addressesResults", results.getAddresses());
						Collection<Certificate> certlist = null;
						try {
							certlist = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
						} catch (ConfigurationServiceException e) {
							e.printStackTrace();
						}
						
						Collection<Anchor> anchorlist = null;
						try {
							anchorlist = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
						} catch (ConfigurationServiceException e) {
							e.printStackTrace();
						}
						
						model.addAttribute("certificatesResults", certlist);
						model.addAttribute("anchorsResults", anchorlist);			
					
						// END: temporary code for mocking purposes			

						SimpleForm simple = new SimpleForm();
						simple.setId(dId);
						model.addAttribute("simpleForm",simple);
						mav.addObject("action", action);
					}
					else {
						log.warn("Service returned a null Domain for a known key: " + dId);		
					}
				}
				else { 
					log.error("Web Service bean is null.  Configuration error detected.");		
				}
				if (AjaxUtils.isAjaxRequest(requestedWith)) {
					// prepare model for rendering success message in this request
					model.addAttribute("message", "");
					model.addAttribute("ajaxRequest", true);
					model.addAttribute("action", action);
					return null;
				}
			}
			
		}
		else {
			model.addAttribute(new LoginForm());
			mav.setViewName("login");
			mav.setView(new RedirectView("/config-ui/config/login", false));
		}
		if (log.isDebugEnabled()) log.debug("Exit");
		return mav;
	}
	
	/**
	 * Execute the save and return the results
	 */
	@RequestMapping(value="/saveupdate", method=RequestMethod.POST )
	public ModelAndView saveDomain (@RequestHeader(value="X-Requested-With", required=false) String requestedWith, 							   
									HttpSession session, 
									@RequestParam(value="submitType") String actionPath,
							        @ModelAttribute("domainForm") DomainForm form,
							        Model model)  { 		
		if (log.isDebugEnabled()) log.debug("Enter");
		if (log.isDebugEnabled()) log.debug("Entered saveDomain");
		if (log.isDebugEnabled()) log.debug("The value of actionPath: "+actionPath);
		ModelAndView mav = new ModelAndView(); 
		if (isLoggedIn(session) && actionPath.equalsIgnoreCase("cancel")) {
			if (log.isDebugEnabled()) log.debug("trying to cancel from saveupdate");
			SearchDomainForm form2 = (SearchDomainForm) session
					.getAttribute("searchDomainForm");
			model.addAttribute(form2 != null ? form2 : new SearchDomainForm());
			model.addAttribute("ajaxRequest", AjaxUtils
					.isAjaxRequest(requestedWith));

			mav.setViewName("main");
			mav.addObject("statusList", EntityStatus.getEntityStatusList());
			return mav;
		} else if (isLoggedIn(session) && (actionPath.equalsIgnoreCase("update") || actionPath.equalsIgnoreCase("add"))){
			HashMap<String, String> msgs = new HashMap<String, String>();
			mav.addObject("msgs", msgs);
			if (log.isDebugEnabled()) log.debug("Inside update else if: submitType: " + actionPath);
			if (isLoggedIn(session)) {
				mav.setViewName("domain");
//				if (form.isValid()) {
					if (log.isDebugEnabled()) log.debug("Form passed validation");
					try {
						if (actionPath.equals("add")) {
							dService.addDomain(form.getDomainFromForm());
							List<Domain> result = new ArrayList<Domain>(
									dService.searchDomain(form.getDomainName(),
											form.getStatus()));
							if (result.size() > 0) {
								form = new DomainForm();
								form.populate(result.get(0));
								msgs.put("msg", "domain.add.success");
							}
						} else if (actionPath.equals("update")) {
							dService.updateDomain(form.getDomainFromForm());
							List<Domain> result = new ArrayList<Domain>(
									dService.searchDomain(form.getDomainName(),
											form.getStatus()));
							if (result.size() > 0) {
								form = new DomainForm();
								form.populate(result.get(0));
							}
							msgs.put("msg", "domain.update.success");
						}

						AddressForm addrform = new AddressForm();
						addrform.setId(form.getDomainFromForm().getId());
						model.addAttribute("domainForm",form);
						model.addAttribute("addressForm",addrform);

						CertificateForm cform = new CertificateForm();
						cform.setId(form.getDomainFromForm().getId());
						AnchorForm aform = new AnchorForm();
						aform.setId(form.getDomainFromForm().getId());
						
						model.addAttribute("certificateForm",cform);
						model.addAttribute("anchorForm",aform);
						SimpleForm simple = new SimpleForm();
						simple.setId(form.getDomainFromForm().getId());
						model.addAttribute("simpleForm",simple);
						
						// once certificates and anchors are available change code accordingly
						// begin: add these dummy records too
						String owner = form.getDomainFromForm().getPostMasterEmail();

						// BEGIN: temporary code for mocking purposes
						Collection<Certificate> certlist = null;
						try {
							certlist = certService.getCertificatesForOwner(owner, CertificateGetOptions.DEFAULT);
						} catch (ConfigurationServiceException e) {
							e.printStackTrace();
						}
						
						Collection<Anchor> anchorlist = null;
						try {
							anchorlist = anchorService.getAnchorsForOwner(owner, CertificateGetOptions.DEFAULT);
						} catch (ConfigurationServiceException e) {
							e.printStackTrace();
						}
						
						model.addAttribute("certificatesResults", certlist);
						model.addAttribute("anchorsResults", anchorlist);			
					
						// END: temporary code for mocking purposes			

							
						//  end: add these dummy records too
						model.addAttribute("addressesResults", form.getDomainFromForm().getAddresses());

						model.addAttribute("action", "update");
						if (log.isDebugEnabled())
							log.debug("Stored domain: "
									+ form.getDomainFromForm().toString());

					} catch (ConfigurationServiceException e) {
						log.error(e);
						msgs.put("domainService", "domainService.add.error");
					}catch(Exception ed){
						log.error(ed);
					}
//				}
			} else {
				model.addAttribute(new LoginForm());
				mav.setViewName("login");
				mav.setView(new RedirectView("/config-ui/config/login", false));
			}
		}else {
			model.addAttribute(new LoginForm());
			mav.setViewName("login");
			mav.setView(new RedirectView("/config-ui/config/login", false));
		}
		if (log.isDebugEnabled()) log.debug("Exit");
		return mav;
	}
	private boolean isLoggedIn(HttpSession session) {
		Boolean result = (Boolean)session.getAttribute("authComplete");
		if (result == null) {
			result = new Boolean(false);
		}
		return result.booleanValue();
	}
	
	/**
	 * Handle exceptions as gracefully as possible
	 * @param ex
	 * @param request
	 * @return
	 */
	@ExceptionHandler(IOException.class) 
	public String handleIOException(IOException ex, HttpServletRequest request) {
		return ClassUtils.getShortName(ex.getClass() + ":" + ex.getMessage());
	}

	public void setdService(DomainService service) {
		this.dService = service;
	}
	
	private static byte[] loadCertificateData(String certFileName) throws Exception
	{
		File fl = new File(certBasePath + certFileName);
        
		return FileUtils.readFileToByteArray(fl);
	}
	
}
