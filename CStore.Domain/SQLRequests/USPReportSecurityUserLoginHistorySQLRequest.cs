using System;
using Catalyst.MVC.Infrastructure.Attributes.Entity;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Entities;

namespace CStore.Domain.SQLRequests
{
    [ProcedureName("usp_Report_SecurityUserLoginHistory")]
    public class USPReportSecurityUserLoginHistorySQLRequest : SQLRequest
    {
        [ParameterName("@StartDate")]
        public DateTime? StartDate { get; set; }

        [ParameterName("@EndDate")]
        public DateTime? EndDate { get; set; }

        [ParameterName("@SecurityUserId")]
        public int? SecurityUserId { get; set; }
    }
}
