using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Service request object to get an individual security Action
    /// </summary>
    public class SecurityAccessMaintenanceGetSecurableTreeRequest : DomainServiceRequest
    {
        public int? ParentSecuritySecurableId { get; set; }
    }
}
