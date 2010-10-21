﻿/* 
 Copyright (c) 2010, Direct Project
 All rights reserved.

 Authors:
    Umesh Madan     umeshma@microsoft.com
    Sean Nolan      seannol@microsoft.com
 
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
Neither the name of the The Direct Project (nhindirect.org). nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnsResolver
{
    /// <summary>
    /// Represents a DNS server response.
    /// </summary>
    public class DnsResponse : DnsMessage
    {
        DnsResourceRecordCollection m_answerRecords;
        DnsResourceRecordCollection m_nameServerRecords;
        DnsResourceRecordCollection m_additionalRecords;
        
        /// <summary>
        /// Creates a fresh new blank Dns response
        /// </summary>
        public DnsResponse()
        {
        }
        
        /// <summary>
        /// Instantiate a new instance with the provided <paramref name="reader"/>
        /// </summary>
        /// <param name="reader">The reader that has been initialized with the response buffer.</param>
        public DnsResponse(DnsBufferReader reader)
            : base(ref reader)
        {
        }
        
        /// <summary>
        /// Construct a response to a request
        /// </summary>
        /// <param name="request"></param>
        public DnsResponse(DnsRequest request)
            : base()
        {
            this.Init(request);
         }        
                        
        /// <summary>
        /// Gets the answer records for this response.
        /// </summary>
        /// <value>A possibly empty collection of answer records.</value>
        public DnsResourceRecordCollection AnswerRecords
        {
            get
            {
                if (m_answerRecords == null)
                {
                    m_answerRecords = new DnsResourceRecordCollection();
                }
                return m_answerRecords;
            }
        }
        
        /// <summary>
        /// Gets if this response has answer records.
        /// </summary>
        public bool HasAnswerRecords
        {
            get
            {
                return (m_answerRecords != null && m_answerRecords.Count > 0);
            }
        }        
        

        /// <summary>
        /// Gets NS records for this response.
        /// </summary>
        /// <value>A possibly empty collection of NS records.</value>
        public DnsResourceRecordCollection NameServerRecords
        {
            get
            {
                if (m_nameServerRecords == null)
                {
                    m_nameServerRecords = new DnsResourceRecordCollection();
                }
                return m_nameServerRecords;
            }
            
        }

        /// <summary>
        /// Gets if this response has nameserver records.
        /// </summary>
        public bool HasNameServerRecords
        {
            get
            {
                return (m_nameServerRecords != null && m_nameServerRecords.Count > 0);
            }
        }

        /// <summary>
        /// Gets other records (not A or NS) for this reponse
        /// </summary>
        /// <value>A possibly empty collection of records.</value>
        public DnsResourceRecordCollection AdditionalRecords
        {
            get
            {
                if (m_additionalRecords == null)
                {
                    m_additionalRecords = new DnsResourceRecordCollection();
                }
                return m_additionalRecords;
            }
        }
        
        /// <summary>
        /// Gets if this response has non-A or NS records.
        /// </summary>
        public bool HasAdditionalRecords
        {
            get
            {
                return (m_additionalRecords != null && m_additionalRecords.Count > 0);
            }
        }
        
        /// <summary>
        /// Initialize a response to the given request
        /// </summary>
        public void Init(DnsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            
            this.RequestID = request.RequestID;
            this.Header.IsRecursionDesired = request.Header.IsRecursionDesired;
            this.Header.IsRequest = false;
            this.Question = request.Question;
        }
        
        /// <summary>
        /// Clear the request...
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            this.ClearAnswers();
        }        
        
        /// <summary>
        /// Wipe all answers
        /// </summary>
        public void ClearAnswers()
        {
            if (this.HasAnswerRecords)
            {
                m_answerRecords.Clear();
            }
            if (this.HasNameServerRecords)
            {
                m_nameServerRecords.Clear();
            }
            if (this.HasAdditionalRecords)
            {
                m_additionalRecords.Clear();
            }
            this.UpdateAnswerCountsInHeader();
        }
                
        /// <summary>
        /// Serialize this DnsResponse to the buffer as DNS wire format data.
        /// </summary>
        /// <param name="buffer">The buffer to which to serialize</param>
        public override void Serialize(DnsBuffer buffer)
        {
            this.UpdateAnswerCountsInHeader();
            
            base.Serialize(buffer);
            
            if (this.HasAnswerRecords)            
            {
                this.AnswerRecords.Serialize(buffer);
            }
            if (this.HasNameServerRecords)
            {
                this.NameServerRecords.Serialize(buffer);
            }
            if (this.HasAdditionalRecords)
            {
                this.AdditionalRecords.Serialize(buffer);
            }
        } 
        
        void UpdateAnswerCountsInHeader()
        {
            if (this.HasAnswerRecords)
            {
                if (this.AnswerRecords.Count > short.MaxValue)
                {
                    throw new DnsProtocolException(DnsProtocolError.InvalidAnswerCount);
                }
                
                this.Header.AnswerCount = (short)this.AnswerRecords.Count;
            }
            else
            {
                this.Header.AnswerCount = 0;
            }
            
            if (this.HasNameServerRecords)
            {
                if (this.NameServerRecords.Count > short.MaxValue)
                {
                    throw new DnsProtocolException(DnsProtocolError.InvalidNameServerAnswerCount);
                }
                this.Header.NameServerAnswerCount = (short)this.NameServerRecords.Count;
            }
            else
            {
                this.Header.NameServerAnswerCount = 0;
            }
            
            if (this.HasAdditionalRecords)
            {
                if (this.AdditionalRecords.Count > short.MaxValue)
                {
                    throw new DnsProtocolException(DnsProtocolError.InvalidAdditionalAnswerCount);
                }
                this.Header.AdditionalAnswerCount = (short)this.AdditionalRecords.Count;
            }
            else
            {
                this.Header.AdditionalAnswerCount = 0;
            }
        }
        
        /// <summary>
        /// Reads DNS wire format data to this response.
        /// </summary>
        /// <param name="reader">The reader that has already buffered response data.</param>
        protected override void Deserialize(ref DnsBufferReader reader)
        {
            base.Deserialize(ref reader);
            if (reader.IsDone)
            {
                //
                // No answers!
                //
                return;
            }
            
            if (this.Header.AnswerCount > 0)
            {
                this.AnswerRecords.Deserialize(this.Header.AnswerCount, ref reader);
            }
            if (this.Header.NameServerAnswerCount > 0)
            {
                this.NameServerRecords.Deserialize(this.Header.NameServerAnswerCount, ref reader);
            }
            if (this.Header.AdditionalAnswerCount > 0)
            {
                this.AdditionalRecords.Deserialize(this.Header.AdditionalAnswerCount, ref reader);
            }
        }
        
        /// <summary>
        /// Validate the response. Throws DnsProtcolException if validation fails
        /// </summary>
        public override void Validate()
        {
            base.Validate();            
            if (this.Header.IsRequest)              
            {
                throw new DnsProtocolException(DnsProtocolError.InvalidResponse);
            }
        }
        
        /// <summary>
        /// Truncate the response. Clear all answers and set the truncate flag
        /// </summary>
        public void Truncate()
        {
            this.Header.IsTruncated = true;
            this.ClearAnswers();
        }
    }
}
