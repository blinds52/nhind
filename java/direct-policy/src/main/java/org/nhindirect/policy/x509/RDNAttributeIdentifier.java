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

package org.nhindirect.policy.x509;

/**
 * Enumeration of relative distinguished name object identifiers (OIDs) found in the subject and issuer fields of the TBS section of an X509 Certificate.
 * These OIDs are used to select a specific attribute from a distinguished name.
 * <p>
 * @author Greg Meyer
 * @since 1.0
 */
public enum RDNAttributeIdentifier 
{
	/*
	 * From RFC5280 section 4.1.2.4 and RFC4519
	 */
	
	/**
	 * Common name attribute
	 */
	COMMON_NAME("2.5.4.3", "CN"),
	
	/**
	 * Country attribute
	 */
	COUNTRY("2.5.4.6", "C"),
	
	/**
	 * Organization attribute
	 */
	ORGANIZATION("2.5.4.10", "O"),
	
	/**
	 * Organizational unit attribute
	 */
	ORGANIZATIONAL_UNIT("2.5.4.11", "OU"),
	
	/**
	 * State attribute
	 */
	STATE("2.5.4.8", "ST"),
	
	/**
	 * Locality (city) attribute
	 */
	LOCALITY("2.5.4.7", "L"),
	
	/**
	 * Legacy email attribute
	 */
	EMAIL("1.2.840.113549.1.9.1", "E"),
	
	/**
	 * Domain component attribute
	 */
	DOMAIN_COMPONENT("0.9.2342.19200300.100.1.25", "DC"),
	
	/**
	 * Distinguished name qualifier attribute
	 */
	DISTINGUISHED_NAME_QUALIFIER("2.5.4.46", "DNQUALIFIER"),
	
	/**
	 * Serial number attribute
	 */
	SERIAL_NUMBER("2.5.4.5", "SERIALNUMBER"),
	
	/**
	 * Surname attribute
	 */
	SURNAME("2.5.4.4", "SN"),
	
	/**
	 * Title name attribute
	 */
	TITLE("2.5.4.12", "TITLE"),
	
	/**
	 * Given name attribute
	 */
	GIVEN_NAME("2.5.4.42", "GIVENNAME"),

	/**
	 * Initials attribute
	 */
	INITIALS("2.5.4.43", "INITIALS"),
	
	/**
	 * Pseudonym attribute
	 */
	PSEUDONYM("2.5.4.65", "PSEUDONYM"),

	/**
	 * General qualifier attribute
	 */
	GERNERAL_QUALIFIER("2.5.4.64", "GERNERAL_QUALIFIER"),
	
	/**
	 * Distinguished name attribute
	 * <p>
	 * This attribute is overloaded by the policy engine and returns the full relative distinguished name using RFC2253 formatting
	 * 
	 */
	DISTINGUISHED_NAME("2.5.4.49", "DN");
	
	protected final String id;
	
	protected final String name;
	
	private RDNAttributeIdentifier(String id, String name)
	{
		this.id = id;
		this.name = name;
	}
	
	/**
	 * Gets the object identifier (OID) of the RDN attribute.
	 * @return The object identifier (OID) of the RDN attribute.
	 */
	public String getId()
	{
		return id;
	}
	
	/**
	 * Gets the name of the attribute as it is commonly displayed in an X509 certificate viewer
	 * @return The name of the attribute as it is commonly displayed in an X509 certificate viewer
	 */
	public String getName()
	{
		return name;
	}
}
