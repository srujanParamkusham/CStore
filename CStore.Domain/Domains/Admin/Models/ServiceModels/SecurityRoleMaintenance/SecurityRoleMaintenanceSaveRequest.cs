using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityRoleMaintenance
{
    /// <summary>
    /// Service request object to save a security role
    /// </summary>
    public class SecurityRoleMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public int SecurityRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ADGroupName { get; set; }
        public Boolean Default { get; set; }
        public Boolean Active { get; set; }
    }
}
