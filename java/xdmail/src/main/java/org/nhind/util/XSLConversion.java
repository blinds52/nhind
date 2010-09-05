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

package org.nhind.util;

import java.io.CharArrayReader;
import java.io.CharArrayWriter;
import java.io.InputStream;
import java.sql.Timestamp;
import java.util.Hashtable;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.xml.transform.Templates;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;

public class XSLConversion {
    // private static StreamConversion conv;

    private static Hashtable<String, Templates> conversions = new Hashtable<String, Templates>(10);
    private static int count = 0;
    static final String SERVERNAME = "XSLEngine";


    /*    public static void main (String[] args){

    //    System.setSecurityManager(new RMISecurityManager());
    try{
    XSLConversion server = new XSLConversion();
    Naming.rebind(SERVERNAME,server);
    Logger.getLogger(this.getClass().getPackage().getName()).log(Level.INFO,"XSLEngine Server Ready");
    }catch(Exception x){
    Logger.getLogger(this.getClass().getPackage().getName()).log(Level.INFO,"XSLEngine Server startup error "+ x.getMessage());
    x.printStackTrace();
    }

    }

     */
    public XSLConversion() {
    }

    public String run(String mapFile, String message)throws Exception{
     String retXml = "";
     Transformer transformer = null;

    try{



        long start = System.currentTimeMillis();
        if(conversions.containsKey(mapFile)){
          Templates temp = conversions.get(mapFile);
          transformer = temp.newTransformer();
          Logger.getLogger(this.getClass().getPackage().getName()).log(Level.INFO,"from xsl cache");
        }else{
          synchronized(conversions){
            if(!conversions.containsKey(mapFile)){
              // Use the static TransformerFactory.newInstance() method to instantiate
              // a TransformerFactory. The javax.xml.transform.TransformerFactory
              // system property setting determines the actual class to instantiate --
              // org.apache.xalan.transformer.TransformerImpl.
              TransformerFactory tFactory = TransformerFactory.newInstance();

              // Use the TransformerFactory to instantiate a Template that is thread safe for use
              // in generating Transfomers
              InputStream is = this.getClass().getResourceAsStream(mapFile);

              if(is==null){
                  System.out.println("MAPFILE DID NOT READ " + mapFile);
              }
              Templates temp = tFactory.newTemplates(new StreamSource(is));
              transformer = temp.newTransformer();
              conversions.put(mapFile, temp);
            }
          }
        }

       // retXml = xslt(message, conv);
        CharArrayWriter to = new CharArrayWriter();
      //  conv.process(new XSLTInputSource(new CharArrayReader(message.toCharArray())),
     //                 null,// precompiled stylesheet is set.
      //                new XSLTResultTarget(to));
          transformer.transform(new StreamSource(new CharArrayReader(message.toCharArray())), new StreamResult(to));
          retXml = to.toString();
      //     Logger.getLogger(this.getClass().getPackage().getName()).log(Level.INFO,retXml);
          long elapse = System.currentTimeMillis() - start;
          Logger.getLogger(this.getClass().getPackage().getName()).log(Level.INFO,"Started at " + new Timestamp(start).toString() + " Elapsed conversion time = " + elapse);

      }
      catch(Exception x){
        x.printStackTrace();
        throw new Exception(x.getMessage());

      }
      return retXml;
    }

}
