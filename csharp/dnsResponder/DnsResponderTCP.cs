﻿/* 
 Copyright (c) 2010, Direct Project
 All rights reserved.

 Authors:
    Umesh Madan     umeshma@microsoft.com
 
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
Neither the name of The Direct Project (directproject.org) nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 
*/
using System;

using Health.Direct.Common.DnsResolver;

namespace Health.Direct.DnsResponder
{
    /// <summary>
    /// Simple DNS TCP Responder
    /// </summary>
    public class DnsResponderTCP : DnsResponder, IHandler<DnsTcpContext>
    {
        readonly TcpServer<DnsTcpContext> m_tcpServer;

        public DnsResponderTCP(IDnsStore store, DnsServerSettings settings)
            : base(store, settings)
        {
            m_tcpServer = new TcpServer<DnsTcpContext>(this.Settings.Endpoint, this.Settings.TcpServerSettings, this);
        }

        public TcpServer<DnsTcpContext> Server
        {
            get
            {
                return m_tcpServer;
            }
        }
        
        public override void Start()
        {
            m_tcpServer.Start();
        }

        public override void Stop()
        {
            m_tcpServer.Stop();
        }

        public bool Process(DnsTcpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            context.Init(this);
            
            //
            // If we fail at parsing or receiving the request, then any exceptions will get logged and
            // the socket will be silently closed
            // 
            context.ReceiveRequest();
                                        
            DnsResponse response = base.ProcessRequest(context.DnsBuffer);
            if (response != null)
            {
                base.Serialize(response, context.DnsBuffer, ushort.MaxValue);
                context.SendResponse();
            }
            
            return true;
        }

        protected override void HandleException(Exception ex)
        {
            m_tcpServer.NotifyError(ex);
        }
    }
}