using System.Data.Entity.ModelConfiguration;
using CStore.Domain.Entities;

namespace CStore.Domain.Mappings
{
    public class USPReportSecurityUserLoginHistoryMapping : EntityTypeConfiguration<USPReportSecurityUserLoginHistory>
    {
        public USPReportSecurityUserLoginHistoryMapping()
        {
            //Specify the primary key of the table
            HasKey(item => new { item.SecurityUserLoginHistoryId });
        }
    }
}