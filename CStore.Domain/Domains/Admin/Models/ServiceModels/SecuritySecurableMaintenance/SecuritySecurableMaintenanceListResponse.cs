using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecuritySecurableMaintenance
{
    /// <summary>
    /// Service response object to search for a list of security securables
    /// </summary>
    public class SecuritySecurableMaintenanceListResponse : DomainServicePagedListResponse<SecuritySecurable>
    {

    }
}
