﻿/* 
 Copyright (c) 2010, NHIN Direct Project
 All rights reserved.

 Authors:
    Umesh Madan     umeshma@microsoft.com
    Sean Nolan      seannol@microsoft.com
 
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

namespace DnsResolver
{
    /*
     *+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
      /                   PTRDNAME                    /
      +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
     */

    /// <summary>
    /// Represents a PTR DNS RR
    /// </summary>
    /// <remarks>
    /// RFC 1035, 
    /// +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    /// /                   PTRDNAME                    /
    /// +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    ///
    /// where:
    ///
    /// PTRDNAME        A &lt;domain-name&gt; which points to some location in the
    ///                domain name space.
    /// </remarks>
    public class PtrRecord : DnsResourceRecord
    {    
        string m_domain;
        
        internal PtrRecord()
        {
        }

        /// <summary>
        /// Gets and sets the domain name (PTRDNAME)
        /// </summary>
        /// <value>A <see cref="string"/> representation of the domain name.</value>
        public string Domain
        {
            get
            {
                return m_domain;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new DnsProtocolException(DnsProtocolError.InvalidPtrRecord);
                }
                
                m_domain = value;
            }
        }

        /// <summary>
        /// Reads values into this instance from the reader
        /// </summary>
        /// <param name="reader">A reader which has a buffer already filled with raw data for this RR.</param>
        protected override void DeserializeRecordData(ref DnsBufferReader reader)
        {
            this.Domain = reader.ReadString();
        }
    }
}
