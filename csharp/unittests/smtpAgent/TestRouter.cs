﻿/* 
 Copyright (c) 2010, NHIN Direct Project
 All rights reserved.

 Authors:
    Umesh Madan     umeshma@microsoft.com
  
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
Neither the name of the The NHIN Direct Project (nhindirect.org). nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHINDirect.Agent;
using NHINDirect.Mail;
using NHINDirect.SmtpAgent;
using NHINDirect.Config.Store;
using Xunit;
using Xunit.Extensions;

namespace SmtpAgentTests
{
    public class TestRouter : SmtpAgentTester
    {
        SmtpAgent m_agent;
        Dictionary<string, int> m_routeCounts;

        static TestRouter()
        {
            AgentTests.AgentTester.EnsureStandardMachineStores();        
        }
        
        public TestRouter()
        {
            m_agent = new SmtpAgent(base.LoadTestSettings("TestSmtpAgentConfig.xml"));
            m_routeCounts = new Dictionary<string,int>();
        }
        
        [Fact]
        public void TestBasic()
        {
            Message message = Message.Load(MultiToMessage);            
            IncomingMessage envelope = new IncomingMessage(message);            
            envelope.EnsureRecipientsCategorizedByDomain(m_agent.Domains);
            //
            // The idea here:
            //   1. We have multiple addresses of each type, and 1 of no type
            //   2. We want to make sure that the router:
            //      a. Picks a route
            //      b. Calls the route method exactly ONCE for each message folder
            //   3. Verify that at the end, the DomainRecipients has 1 recipient
            //
            Address address;
            foreach (NHINDAddress recipient in envelope.DomainRecipients)
            {
                address = null;
                switch(recipient.Host.ToLower())
                {
                    default:
                        break;
                        
                    case "nhind.hsgincubator.com":
                        if (!recipient.User.Equals("frank", StringComparison.OrdinalIgnoreCase))
                        {
                            address = new Address();
                            address.Type = "SMTP";
                        }
                        break;
                        
                    case "redmond.hsgincubator.com":
                        address = new Address();
                        address.Type = "XDR";
                        break;                        
                }
                
                recipient.Tag = address;
            }      
            
            m_agent.Router.Route(new DummySmtpMessage(), envelope, this.RouteHandler);
            //
            // Verify counts
            //
            foreach(int count in m_routeCounts.Values)
            {
                Assert.True(count == 1);
            }
            
            Assert.True(envelope.HasDomainRecipients && envelope.DomainRecipients.Count == 1);
        }
        
        void RouteHandler(ISmtpMessage message, MessageRoute route)
        {
            int count = 0;
            if (!m_routeCounts.TryGetValue(route.CopyFolder, out count))
            {
                count = 1;
                m_routeCounts[route.CopyFolder] = count;
            }
            else
            {
                m_routeCounts[route.CopyFolder] = count++;
            }
        }
    }
}