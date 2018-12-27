using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Service request object to get the roles permissions can be edited for
    /// </summary>
    public class SecurityAccessMaintenanceGetRolesRequest : DomainServiceRequest
    {
        public List<int> SelectedRoleIds { get; set; }
    }
}
