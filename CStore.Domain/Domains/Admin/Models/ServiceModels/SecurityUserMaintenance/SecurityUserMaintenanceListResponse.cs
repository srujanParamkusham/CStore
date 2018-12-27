using CStore.Domain.Entities;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityUserMaintenance
{
    /// <summary>
    /// Service response object to search for a list of users
    /// </summary>
    public class SecurityUserMaintenanceListResponse : DomainServicePagedListResponse<VWSecurityUser>
    {

    }
}
