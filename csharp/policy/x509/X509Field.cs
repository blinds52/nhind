﻿using System;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;

namespace Health.Direct.Policy.X509
{
    public abstract class X509Field<T> : IX509Field<T>
    {
        [NonSerialized] protected IPolicyValue<T> PolicyValue;
        [NonSerialized] protected X509Certificate2 Certificate;

        protected bool Required;

        /// <inheritdoc />
        public PolicyExpressionReferenceType GetPolicyExpressionReferenceType()
        {
            return PolicyExpressionReferenceType.Certificate;
        }

       

        /// <inheritdoc />
        public PolicyExpressionType GetExpressionType()
        {
            return PolicyExpressionType.REFERENCE;
        }

        /// <inheritdoc />
        public IPolicyValue<T> GetPolicyValue()
        {
            if (PolicyValue == null)
                throw new InvalidOperationException("Policy value is null");

            return PolicyValue;
        }

        /// <inheritdoc />
        public bool IsRequired()
        {
            return Required;
        }

        /// <inheritdoc />
        public void SetRequired(bool required)
        {
            Required = required;
        }



        /// <summary>
        /// Converts an encoded internal octet string object to a DERObject
        /// </summary>
        /// <param name="ext">The encoded octet string as a byte array</param>
        /// <returns>The converted DerObjectIdentifier</returns>
        protected DerObjectIdentifier GetObject(byte[] ext)
        {
            try
            {
                Asn1InputStream aIn;
                using (aIn = new Asn1InputStream(ext))
                {
                    Asn1OctetString octs = (Asn1OctetString) aIn.ReadObject();
                    using (aIn = new Asn1InputStream(octs.GetOctets()))
                    {
                        return aIn.ReadObject() as DerObjectIdentifier;
                    }
                }
            }
            catch (Exception e)
            {
                throw new PolicyProcessException("Exception processing data ", e);
            }
        }


        /// <summary>
        /// Converts an encoded internal sequence object to a DERObject
        /// </summary>
        /// <param name="ext">The encoded sequence as a byte array</param>
        /// <returns>The converted DERObjectIdentifier</returns>
        protected DerObjectIdentifier GetDERObject(byte[] ext)
        {

            try
            {
                Asn1InputStream aIn;
                using (aIn = new Asn1InputStream(ext))
                {
                    DerSequence seq = (DerSequence) aIn.ReadObject();
                    using (aIn = new Asn1InputStream(seq.GetDerEncoded()))
                    {
                        return aIn.ReadObject() as DerObjectIdentifier;
                    }
                }
            }
            catch (Exception e)
            {
                throw new PolicyProcessException("Exception processing data ", e);
            }

        }

        //TODO: this feels wrong.  Had to do this to compile during the Java port.
        public abstract X509FieldType GetX509FieldType();
        public abstract void InjectReferenceValue(X509Certificate2 value);

        /// <inheritdoc />
        public override String ToString()
        {
            if (PolicyValue == null)
            {
                return "Unevaluated X509 field: " + GetX509FieldType();
            }
            return PolicyValue.ToString();
        }
    }
}