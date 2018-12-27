using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Service request object to save permission updates for a security securable action id
    /// and a role id.
    /// </summary>
    public class SecurityAccessMaintenanceSavePermissionsRequest : DomainServiceRequest
    {
        public int? SecuritySecurableId { get; set; }
        public List<PermissionToSave> PermissionsToSave { get; set; }
    }
}
