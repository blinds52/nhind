/* 
 * Copyright (c) 2010, NHIN Direct Project
 * All rights reserved.
 *  
 * Redistribution and use in source and binary forms, with or without 
 * modification, are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright 
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright 
 *    notice, this list of conditions and the following disclaimer in the 
 *    documentation and/or other materials provided with the distribution.  
 * 3. Neither the name of the the NHIN Direct Project (nhindirect.org)
 *    nor the names of its contributors may be used to endorse or promote products 
 *    derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY 
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND 
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

package org.nhindirect.transform.parse.ccd;

import junit.framework.TestCase;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.nhindirect.xd.transform.parse.ccd.CcdParser;

/**
 * Test methods in the CCDParser class.
 * 
 * TODO: This test class needs assertions.
 * 
 * @author vlewis
 */
public class CcdParserTest extends TestCase
{
    private static final Log LOGGER = LogFactory.getFactory().getInstance(CcdParserTest.class);

    public CcdParserTest(String testName)
    {
        super(testName);
    }

    @Override
    protected void setUp() throws Exception
    {
        super.setUp();
    }

    @Override
    protected void tearDown() throws Exception
    {
        super.tearDown();
    }

    /**
     * Test of parseCCD method, of class CCDParser.
     */
    public void testParseCCD() throws Exception
    {
        LOGGER.info("parseCCD");
        String ccdXml = "<ClinicalDocument>Test</ClinicalDocument>";
        CcdParser instance = new CcdParser();
        // instance.parseCCD(ccdXml);

    }

    /**
     * Test of getPatientId method, of class CCDParser.
     */
    public void testGetPatientId()
    {
        LOGGER.info("getPatientId");
        CcdParser instance = new CcdParser();
        String expResult = "";
        String result = instance.getPatientId();
        // assertEquals(expResult, result);
        // TODO review the generated test code

    }

    /**
     * Test of getOrgId method, of class CCDParser.
     */
    public void testGetOrgId()
    {
        LOGGER.info("getOrgId");
        CcdParser instance = new CcdParser();
        String expResult = "";
        String result = instance.getOrgId();
        // assertEquals(expResult, result);
        // TODO review the generated test code

    }

}
