using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityUserMaintenance
{
    /// <summary>
    /// Service response object to get the roles an individual user has
    /// </summary>
    public class SecurityUserMaintenanceGetRolesForUserResponse : DomainServiceResponse
    {
        public int? SecurityUserId { get; set; }

        public List<SecurityRole> AvailableRoles { get; set; }

        public List<SecurityRole> AssignedRoles { get; set; }
    }
}
