using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Service request object to save permission updates for a security securable action id
    /// and a role id.
    /// </summary>
    public class SecurityAccessMaintenanceSavePermissionsResponse : DomainServiceResponse
    {
        public int NumPermissionsUpdated { get; set; }
    }
}
