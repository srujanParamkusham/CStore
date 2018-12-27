using System.Data.Entity.ModelConfiguration;
using CStore.Domain.Entities;

namespace CStore.Domain.Mappings
{
    public class VWSecurityUserMapping : EntityTypeConfiguration<VWSecurityUser>
    {
        public VWSecurityUserMapping()
        {
            HasKey(item => item.SecurityUserId);
            Property(item => item.SecurityUserId).HasColumnName("SecurityUserId");
            ToTable("vw_SecurityUser");
        }
    }
}