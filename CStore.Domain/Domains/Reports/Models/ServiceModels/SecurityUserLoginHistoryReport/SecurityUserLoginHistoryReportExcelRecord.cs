using System;

namespace CStore.Domain.Domains.Reports.Models.ServiceModels.SecurityUserLoginHistoryReport
{
    /// <summary>
    /// Object to model the record to be written out to an excel file
    /// </summary>
    public class SecurityUserLoginHistoryReportExcelRecord
    {
        public Int32 SecurityUserLoginHistoryId { get; set; }
        public Int32? SecurityUserId { get; set; }
        public String UserName { get; set; }
        public String MachineName { get; set; }
        public String ApplicationCode { get; set; }
        public Boolean SuccessfulLogin { get; set; }
        public Boolean AccountWasLocked { get; set; }
        public String IPAddress { get; set; }
        public String Browser { get; set; }
        public String ScreenResolution { get; set; }
        public String Message { get; set; }
        public String SessionId { get; set; }
        public DateTime? SessionEndDate { get; set; }
        public DateTime? LastRequestDate { get; set; }
        public DateTime? SessionTimeoutDate { get; set; }
        public String LastRequestUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public String CreateUser { get; set; }
        public DateTime? ModifyDate { get; set; }
        public String ModifyUser { get; set; }
    }
}
