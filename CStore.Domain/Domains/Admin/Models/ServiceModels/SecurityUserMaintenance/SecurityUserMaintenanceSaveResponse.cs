using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityUserMaintenance
{
    /// <summary>
    /// Service response object to save a user
    /// </summary>
    public class SecurityUserMaintenanceSaveResponse : DomainServiceSaveResponse<SecurityUser>
    {
        public List<SecurityRole> AvailableRoles { get; set; }

        public List<SecurityRole> AssignedRoles { get; set; }
    }
}
