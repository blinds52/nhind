﻿/* 
 Copyright (c) 2010, Direct Project
 All rights reserved.

 Authors:
    Arien Malec     arien.malec@nhindirect.org
  
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
Neither the name of The Direct Project (directproject.org) nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Health.Direct.Common.Metadata;
using Health.Direct.Xd;
using Ionic.Zip;
using System.IO;

namespace Health.Direct.Xdm
{
    public class XDMZipPackager
    {
        // Use Default
        private XDMZipPackager() { }


        private static XDMZipPackager m_Instance = new XDMZipPackager();

        /// <summary>
        /// The default instance
        /// </summary>
        public static XDMZipPackager Default
        {
            get
            {
                return m_Instance;
            }
        }


        /// <summary>
        /// Packages a <see cref="DocumentPackage"/> as an XDM zip file
        /// </summary>
        public ZipFile Package(DocumentPackage package)
        {
            ZipFile z = new ZipFile();

            AddDocuments(package, z); // Alters URI by side effect
            AddManifests(package, z);
            AddMetadata(package, z);

            return z;
        }

        private void AddManifests(DocumentPackage package, ZipFile z)
        {
            AddIndex(package, z);
            AddReadme(z);
        }

        private void AddReadme(ZipFile z)
        {
            z.AddEntry(XDMStandard.ReadmeFilename, XDMStandard.ReadmeFileString);
        }

        private void AddIndex(DocumentPackage package, ZipFile z)
        {
            z.AddEntry(XDMStandard.IndexHtmFile, GenerateIndexFile(package));
        }

        private void AddMetadata(DocumentPackage package, ZipFile z)
        {
            
            StringBuilder sb = new StringBuilder();
            using (StringWriter w = new StringWriter(sb))
            {
                package.Generate().Save(w);
            }

            z.AddEntry(XDMStandard.DefaultMetadataFilePath, sb.ToString());
        }

        private void AddDocuments(DocumentPackage package, ZipFile z)
        {
            int i = 1;
            foreach (DocumentMetadata doc in package.Documents)
            {
                if (doc.DocumentBytes == null) throw new XdMetadataException(XdError.MissingDocumentBytes);
                string suffix = i.ToString("000");
                string name = String.Format("{0}/{1}/{2}", XDMStandard.MainDirectory, XDMStandard.DefaultSubmissionSet, XDMStandard.DocPrefix + suffix);
                doc.Uri = name;
                z.AddEntry(name, doc.DocumentBytes);
            }
        }

        private string GenerateIndexFile(DocumentPackage package)
        {
            var liElts = from d in package.Documents
                         select new XElement("li",
                             new XElement("a", d.Title,
                                 new XAttribute("href", d.Uri)));
            XDocument index = new XDocument(
                new XDocumentType("html", "-//W3C//DTD XHTML Basic 1.1//EN", "http://www.w3.org/TR/xhtml-basic/xhtml-basic11.dtd", null),
                new XElement("html",
                    new XElement("head",
                        new XElement("title", "Content index")),
                    new XElement("body",
                        new XElement("h2", "Content index"),
                        new XElement("ul", liElts))));

            return index.ToString();
        }



    }
}