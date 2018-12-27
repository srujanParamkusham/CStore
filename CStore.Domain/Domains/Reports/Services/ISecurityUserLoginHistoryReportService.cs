using CStore.Domain.Domains.Reports.Models.ServiceModels.SecurityUserLoginHistoryReport;

namespace CStore.Domain.Domains.Reports.Services
{
    public interface ISecurityUserLoginHistoryReportService
    {
        SecurityUserLoginHistoryReportGenerateReportResponse GenerateReport(SecurityUserLoginHistoryReportGenerateReportRequest request);
    }
}
