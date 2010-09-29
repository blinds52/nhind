﻿/* 
 Copyright (c) 2010, NHIN Direct Project
 All rights reserved.

 Authors:
    Arien Malec     arien.malec@nhindirect.org
  
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
using System.Xml.Linq;

using NHINDirect.Metadata;
using NHINDirect;

namespace NHINDirect.Xd
{
    /// <summary>
    /// A pair of objects (poor man's Tuple)
    /// </summary>
    public class Pair<T1, T2>
    {
        private T1 m_first;
        private T2 m_second;

        /// <summary>
        /// Initializes a pair
        /// </summary>
        public Pair(T1 first, T2 second)
        {
            m_first = first;
            m_second = second;
        }

        /// <summary>
        /// The first item in the pair
        /// </summary>
        public T1 First { get { return m_first; } }
        /// <summary>
        /// The second item in the pair
        /// </summary>
        public T2 Second { get { return m_second; } }

        /// <summary>
        /// Makes a pair
        /// </summary>
        public static Pair<T1,T2> Make(T1 first, T2 second)
        {
            return new Pair<T1, T2>(first, second);
        }
    }




    /// <summary>
    /// Extension and other Methods for generating XElements for IHE XD* ebXML from metadata objects.
    /// </summary>
    public static class XDMetadataGenerator
    {

        private static Pair<Object, Func<XObject>> Map(Object o, Func<XObject> e)
        {
            return Pair<Object, Func<XObject>>.Make(o, e);
        }


        // Submission

        /// <summary>
        /// Returns an <see cref="XElement"/> for this package in IHE XD* ebXML
        /// </summary>
        public static XElement Generate(this DocumentPackage docPackage)
        {
            XElement package = new XElement("SubmitObjectsRequest");
            XElement packageList = new XElement("RegistryObjectList");
            package.Add(packageList);
            packageList.Add(GeneratePackage(docPackage));

            foreach (DocumentMetadata m in docPackage.Documents)
            {
                packageList.Add(m.Generate());
            }
            return package;
        }

        /// <summary>
        /// Generates an <see cref="XElement"/> for a RegistryPackage
        /// </summary>
        public static XElement GeneratePackage(DocumentPackage docPackage)
        {
            string packageId = "urn:uuid:" + Guid.NewGuid();
            XElement packageMetadata = new XElement("RegistryPackage",
                new XAttribute("id", packageId),
                new Classification(XDMetadataStandard.SubmissionSetClassificationUUID, packageId));

            List<Pair<Object, Func<XObject>>> specs = new List<Pair<Object, Func<XObject>>>
            {   
                Map(docPackage.Author,
                    () => new MultiSlotClassification(XDMetadataStandard.SubmissionSetAuthorUUID, "", packageId,
                                AuthorSlots(docPackage.Author))),
                Map(docPackage.Comments,
                    () => new Description(docPackage.Comments)),
                Map(docPackage.ContentTypeCode,
                    () => new CodedValueClassification(XDAttribute.ContentTypeCode, packageId, docPackage.ContentTypeCode)),
                // TODO: IntendedRecipients
                Map(docPackage.PatientId,
                    () => new ExternalIdentifier(XDMetadataStandard.SubmissionSetPatientIdUUID, docPackage.PatientId.ToEscapedCx(), "XDSSubmissionSet.patientId")),
                Map(docPackage.SourceId,
                    () => new ExternalIdentifier(XDMetadataStandard.SubmissionSetSourceIdUUID, docPackage.SourceId, "XDSSubmissionSet.sourceId")),
                Map(docPackage.SubmissionTime,
                    () => new Slot(XDMetadataStandard.SubmissionTimeSlot, docPackage.SubmissionTime.ToHL7Date())),
                Map(docPackage.Title,
                    () => new Name(docPackage.Title)),
                Map(docPackage.UniqueId,
                    () => new ExternalIdentifier(XDMetadataStandard.SubmissionSetUniqueIdUUID, docPackage.UniqueId, "XDSSubmissionSet.uniqueId")),
            };

            foreach (Pair<Object, Func<XObject>> p in specs)
            {
                if (p.First != null) packageMetadata.Add(p.Second());
            }


            return packageMetadata;
        }



        // Document

        /// <summary>
        /// Returns an <see cref="XElement"/> for this document metadata in IHE XD* ebXML
        /// </summary>
        /// <param name="docMetadata">This document metadata</param>
        /// <returns>An <see cref="XElement"/> for this document metadata in IHE XD* ebXML</returns>
        public static XElement Generate(this DocumentMetadata docMetadata)
        {

            // TODO: this method cries out for a domain specific language, but this turns out to be harder to
            // do in C# than in Ruby :-)
            string documentName = "urn:uuid:" + Guid.NewGuid();

            XElement docEbX = new XElement("ExtrinsicObjectType",
                new XAttribute("objectType", XDMetadataStandard.DocumentEntryUUID));


            List<Pair<Object, Func<XObject>>> specs = new List<Pair<Object, Func<XObject>>>
            {
                Map(docMetadata.Author,
                   () => new MultiSlotClassification(XDMetadataStandard.DocumentAuthorUUID, "", documentName,
                       AuthorSlots(docMetadata.Author))),
                Map(docMetadata.Class,
                    () => new CodedValueClassification(XDAttribute.ClassCode, documentName, docMetadata.Class)),
                Map(docMetadata.Comments,
                    () => new Description(docMetadata.Comments)),
                Map(docMetadata.Confidentiality,
                    () => new CodedValueClassification(XDAttribute.ConfidentialityCode, documentName, docMetadata.Confidentiality)),
                Map(docMetadata.CreatedOn,
                    () => new Slot(XDMetadataStandard.CreationTimeSlot, docMetadata.CreatedOn.Value.ToHL7Date())),
                Map(docMetadata.EventCodes,
                    () => EventCodeClassifications(docMetadata.EventCodes, documentName)),
                Map(docMetadata.FormatCode,
                    () =>  new CodedValueClassification(XDAttribute.FormatCode, documentName, docMetadata.FormatCode)),
                Map(docMetadata.Hash,
                    () => new Slot(XDMetadataStandard.HashSlot, docMetadata.Hash)),
                Map(docMetadata.FaciltyCode,
                    () => new CodedValueClassification(XDAttribute.FacilityType, documentName,docMetadata.FaciltyCode)),
                Map(docMetadata.LanguageCode,
                    () => new Slot(XDMetadataStandard.LanguageCodeSlot, docMetadata.LanguageCode)),
                Map(docMetadata.LegalAuthenticator,
                    () => new Slot(XDMetadataStandard.LegalAuthenticatorSlot, docMetadata.LegalAuthenticator.ToXCN())),
                Map(docMetadata.MediaType,
                    () => new XAttribute("mimeType", docMetadata.MediaType) ),
                Map(docMetadata.PatientID,
                    () => new ExternalIdentifier(XDMetadataStandard.PatientIdentitySchemeUUID,
                        docMetadata.PatientID.ToEscapedCx(), "XDSDocumentEntry.patientId")),
                Map(docMetadata.ServiceStart,
                    () => new Slot(XDMetadataStandard.ServiceStartSlot, docMetadata.ServiceStart.Value.ToHL7Date())),
                Map(docMetadata.ServiceStop,
                    () =>  new Slot(XDMetadataStandard.ServiceStopSlot, docMetadata.ServiceStop.Value.ToHL7Date()) ),
                Map(docMetadata.PracticeSetting,
                    () => new CodedValueClassification(XDAttribute.PracticeSettingCode, documentName, docMetadata.PracticeSetting) ),
                Map(docMetadata.Size,
                    () => new Slot(XDMetadataStandard.SizeSlot, docMetadata.Size.ToString()) ),
                Map(docMetadata.SourcePtId,
                    () => new Slot(XDMetadataStandard.SourcePatientIDSlot, docMetadata.SourcePtId.ToEscapedCx()) ),
                Map(docMetadata.Patient,
                    () => new Slot(XDMetadataStandard.SourcePatientInfoSlot, docMetadata.Patient.ToSourcePatientInfoValues(docMetadata.SourcePtId))),
                Map(docMetadata.Title,
                    () => new Name(docMetadata.Title) ),
                // typeCode is the same as class.
                Map(docMetadata.Class,
                    () => new CodedValueClassification(XDAttribute.TypeCode, documentName, docMetadata.Class) ),
                Map(docMetadata.UniqueId,
                    () => new ExternalIdentifier(XDMetadataStandard.DocumentUniqueIdIdentitySchemeUUID, docMetadata.UniqueId,"XDSDocumentEntry.uniqueId")),
                Map(docMetadata.Uri,
                    () => new Slot(XDMetadataStandard.UriSlot, UriValues(docMetadata.Uri))),
            };

            foreach (Pair<Object, Func<XObject>> p in specs)
            {
                if (p.First != null) docEbX.Add(p.Second());
            }

            return docEbX;
        }

        /// <summary>
        /// Generates Event Code coded value classifications for each event code
        /// </summary>
        public static XElement EventCodeClassifications(IEnumerable<CodedValue> values, string documentName)
        {
            var elts = from c in values
                       select new CodedValueClassification(XDAttribute.EventCodeList, documentName, c);
            if (elts.Count() == 0)
                return null;
            XElement e = null;
            foreach (XElement el in elts)
            {
                if (e == null) e = el;
                else e.AddAfterSelf (el);
            }
            return e;
        }

        // Author

        /// <summary>
        /// Slots for the Author classification
        /// </summary>
        public static IEnumerable<Slot> AuthorSlots(Author a)
        {
            if (a == null)
                yield break;
            if (a.Specialities != null)
                yield return new Slot("authorSpecialty", a.Specialities);
            if (a.Roles != null)
                yield return new Slot("authorRole", a.Roles);
            if (a.Institutions != null)
                yield return new Slot("authorInstitution", a.Institutions);
            if (a.Person != null)
                yield return new Slot("authorPerson", a.Person.ToXCN());
        }

        /// <summary>
        /// Implements the URI break algorithm for the IHE XD* URI document attribute
        /// </summary>
        public static IEnumerable<string> UriValues(string Uri)
        {
            IEnumerable<string> strings = Uri.Break(128);
            return strings.Select((string s, int i) => String.Format("{0}|{1}", i + 1, s));
        }
    }
}
