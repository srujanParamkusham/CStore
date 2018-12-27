using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuMaintenance
{
    /// <summary>
    /// Service request object to search for a list of application menus
    /// </summary>
    public class AppMenuMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The menu code to filter on
        /// </summary>
        public String MenuCode { get; set; }

        /// <summary>
        /// The name to filter on
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The description to filter on
        /// </summary>
        public String Description { get; set; }

    }
}
