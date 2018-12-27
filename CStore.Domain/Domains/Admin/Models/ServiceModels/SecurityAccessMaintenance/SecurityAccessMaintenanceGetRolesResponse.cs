using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Service response object to get the roles permissions can be edited for
    /// </summary>
    public class SecurityAccessMaintenanceGetRolesResponse : DomainServiceResponse
    {
        public List<SecurityRole> AvailableRoles { get; set; }

        public List<SecurityRole> SelectedRoles { get; set; }
    }
}
