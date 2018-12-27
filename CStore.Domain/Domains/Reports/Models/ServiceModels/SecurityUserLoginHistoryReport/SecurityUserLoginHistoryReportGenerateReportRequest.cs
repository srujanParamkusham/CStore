using System;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Reports.Models.ServiceModels.SecurityUserLoginHistoryReport
{
    /// <summary>
    /// Request object to generate the report
    /// </summary>
    public class SecurityUserLoginHistoryReportGenerateReportRequest : DomainServiceGenerateReportRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SecurityUserId { get; set; }
    }
}
