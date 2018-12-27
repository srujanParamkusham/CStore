using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuItemMaintenance
{
    /// <summary>
    /// Service response object to get all of the app menus, used to create select lists with
    /// </summary>
    public class AppMenuItemMaintenanceGetAllAppMenusResponse : DomainServiceResponse
    {
        public List<AppMenu> AppMenus { get; set; }
    }
}
