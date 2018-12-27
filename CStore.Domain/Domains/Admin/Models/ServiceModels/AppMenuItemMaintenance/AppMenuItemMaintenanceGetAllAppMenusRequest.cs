using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuItemMaintenance
{
    /// <summary>
    /// Service request object to get all of the app menus, used to create select lists with
    /// </summary>
    public class AppMenuItemMaintenanceGetAllAppMenusRequest : DomainServiceRequest
    {
        /// <summary>
        /// The menus with IDs in this list will not be included in the results.
        /// </summary>
        public List<int> AppMenuIdExcludeList { get; set; }
    }
}
