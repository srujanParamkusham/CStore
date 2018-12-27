using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuItemMaintenance
{
    /// <summary>
    /// Service request object to get all of the app menus items, used to create select lists with
    /// </summary>
    public class AppMenuItemMaintenanceGetAllAppMenuItemsRequest : DomainServiceRequest
    {
        /// <summary>
        /// The menu items with IDs in this list will not be included in the results.
        /// </summary>
        public List<int> AppMenuItemIdExcludeList { get; set; }
    }
}
