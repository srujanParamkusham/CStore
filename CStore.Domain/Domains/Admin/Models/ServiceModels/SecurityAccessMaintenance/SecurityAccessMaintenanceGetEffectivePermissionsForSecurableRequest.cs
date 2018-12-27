using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Service request object to get the effective permissions for all of the roles for a securable.
    /// </summary>
    public class SecurityAccessMaintenanceGetEffectivePermissionsForSecurableRequest : DomainServiceRequest
    {
        public int? SecuritySecurableId { get; set; }

        public List<int> SelectedRoleIds { get; set; }

    }
}
