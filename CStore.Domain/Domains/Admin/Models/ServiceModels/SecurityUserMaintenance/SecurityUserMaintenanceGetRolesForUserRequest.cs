using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityUserMaintenance
{
    /// <summary>
    /// Service request object to get the roles an individual user has
    /// </summary>
    public class SecurityUserMaintenanceGetRolesForUserRequest : DomainServiceRequest
    {
        public int? SecurityUserId { get; set; }
    }
}
