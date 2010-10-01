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

namespace NHINDirect.Metadata
{

    /// <summary>
    /// Enumeration for biological sex (male, female, other (XYY, etc.))
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// XY chromosomes
        /// </summary>
        Male,
        /// <summary>
        /// XX chromosomes
        /// </summary>
        Female,
        /// <summary>
        /// Other than XX or XY or other case of unclear biological sex
        /// </summary>
        Other
    }


    /// <summary>
    /// Represents a human person
    /// </summary>
    public class Person
    {

        /// <summary>
        /// First name
        /// </summary>
        public string First
        {
            get;
            set;
        }

        /// <summary>
        /// Last name
        /// </summary>
        public string Last
        {
            get;
            set;
        }

        /// <summary>
        /// Middle name or initial
        /// </summary>
        public string MI
        {
            get;
            set;
        }

        /// <summary>
        /// Suffix (e.g, III, Jr.)
        /// </summary>
        public string Suffix
        {
            get;
            set;
        }

        /// <summary>
        /// Prefix (e.g., Dr., Mr., etc.)
        /// </summary>
        public string Prefix
        {
            get;
            set;
        }

        /// <summary>
        /// Degree (e.g., M.D., M.D, PhD)
        /// </summary>
        public string Degree
        {
            get;
            set;
        }


        /// <summary>
        /// Biological sex of the person
        /// </summary>
        public Sex? Sex { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public DateTime? Dob { get; set; }


        /// <summary>
        /// Home or primary address for this person.
        /// </summary>
        public PostalAddress? Address { get; set; }

        /// <summary>
        /// Tests if this instance equals another
        /// </summary>
        public bool Equals(Person other)
        {
            bool firstEquals = (First == null && other.First == null) || First == other.First;
            bool lastEquals = (Last == null && other.Last == null) || Last == other.Last;
            bool miEquals = (MI == null && other.MI == null) || MI == other.MI;
            bool suffixEquals = (Suffix == null && other.Suffix == null) || Suffix == other.Suffix;
            bool prefixEquals = (Prefix == null && other.Prefix == null) || Prefix == other.Prefix;
            bool degreeEquals = (Degree == null && other.Degree == null) || Degree == other.Degree;
            bool sexEquals = (Sex == null && other.Sex == null) || Sex == other.Sex;
            bool dobEquals = (Dob == null && other.Dob == null) || Dob == other.Dob;
            return firstEquals && lastEquals && miEquals && suffixEquals && prefixEquals && degreeEquals && sexEquals && dobEquals;
        }

        /// <summary>
        /// Provides a string representation of the Person
        /// </summary>
        /// <returns>A string representation of the person</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Prefix == null ? "" : Prefix + " ");
            if (First != null) // Special case first name in case you only have a first name
            {
                sb.Append(First);
                sb.Append(MI != null || Last != null ? " " : "");
            }
            sb.Append(MI == null ? "" : MI + " ");
            sb.Append(Last == null ? "" : Last);
            sb.Append(Suffix == null ? "" : ", " + Suffix);
            sb.Append(Degree == null ? "" : ", " + Degree);
            sb.Append(Sex == null ? "" : " Sex: " + Sex.AsString());
            sb.Append(Dob == null ? "" : " Dob: " + Dob.Value.ToShortDateString());
            return sb.ToString();
        }

        /// <summary>
        /// Formats the person as an XCN HL7 datatype
        /// </summary>
        /// <returns>A string HL7 representation of the person</returns>
        public string ToXCN()
        {
            return String.Format("^{0}^{1}^{2}^{3}^{4}^{5}",
                Last ?? "",
                First ?? "",
                MI ?? "",
                Suffix ?? "",
                Prefix ?? "",
                Degree ?? "");
        }

        /// <summary>
        /// Returns the trimmed field or null if the trimmed field is empty
        /// </summary>
        private static string HL7Field(string field)
        {
            field.Trim();
            if (field == "") return null;
            return field;
        }

        /// <summary>
        /// Parses an XCN and returns a Person
        /// </summary>
        public static Person FromXCN(string xcn)
        {
            if (xcn == null) throw new ArgumentException();

            Person p = new Person();

            string[] fields = xcn.Split('^');
            if (fields.Length < 2) throw new ArgumentException();
            p.Last =                          HL7Field(fields[1]);
            if (fields.Length >= 2) p.First = HL7Field(fields[2]);
            if (fields.Length >= 3) p.MI =    HL7Field(fields[3]);
            if (fields.Length >= 4) p.Suffix =HL7Field(fields[4]);
            if (fields.Length >= 5) p.Prefix =HL7Field(fields[5]);
            if (fields.Length >= 6) p.Degree =HL7Field(fields[6]);

            return p;
        }

        string FormatHL7Value(string prop)
        {
            return (prop == null ? "^" : prop + "^");
        }

        /// <summary>
        /// Generates string values suitable for inclusion in a Slot for source patient information
        /// </summary>
        public IEnumerable<string> ToSourcePatientInfoValues(PatientID id)
        {
            string idValue = (id == null) ? "" : id.ToEscapedCx();
            string nameValue =  ToXCN();
            string dateValue =  Dob == null ? "" : Dob.Value.ToHL7Date();
            string sexValue = Sex == null ? "" : Sex.AsString();
            string addressValue = Address == null ? "" : Address.Value.ToHL7Ad();
            yield return "PID-3|" + idValue;
            yield return "PID-5|" + nameValue;
            yield return "PID-7|" + dateValue;
            yield return "PID-8|" + sexValue;
            yield return "PID-11|" + addressValue;
        }


    }
}
