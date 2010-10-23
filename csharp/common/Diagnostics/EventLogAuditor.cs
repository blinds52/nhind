using System;
using System.Diagnostics;

namespace Health.Direct.Common.Diagnostics
{
    ///<summary>
    /// A Null version of the <see cref="IAuditor"/> interface.
    ///</summary>
    public class EventLogAuditor : IAuditor
    {
        ///<summary>
        /// Log to the audit sub-system that a specific category event has occurred.
        ///</summary>
        ///<param name="category">A category usually named in <see cref="AuditNames"/></param>
        public void Log(string category)
        {
            Log(category, null);
        }

        ///<summary>
        /// Log to the audit sub-system that a specific category event has occurred with optional
        /// message.
        ///</summary>
        ///<param name="category">A category usually named in <see cref="AuditNames"/></param>
        ///<param name="message">A string message to include with the audited event.</param>
        public void Log(string category, string message)
        {
            try
            {
                EventLogHelper.WriteInformation("Health.Direct.Audit", 
                    string.Format("AUDIT: {0} - {1}", category, message));
            }
            catch (Exception ex)
            {
                Diagnostics.Log.For(this).Warn("While auditing", ex);
            }
        }
    }
}