using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityRoleMaintenance
{
    /// <summary>
    /// Service response object to search for a list of security roles
    /// </summary>
    public class SecurityRoleMaintenanceListResponse : DomainServicePagedListResponse<SecurityRole>
    {

    }
}
