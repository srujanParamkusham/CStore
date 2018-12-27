using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuItemMaintenance
{
    /// <summary>
    /// Service request object to search for a list of application menus
    /// </summary>
    public class AppMenuItemMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The name to ParentAppMenuItemId on
        /// </summary>
        public String ParentAppMenuItemId { get; set; }

        /// <summary>
        /// The name to AppMenuId on
        /// </summary>
        public String AppMenuId { get; set; }

        /// <summary>
        /// The name to filter on
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The handler to filter on
        /// </summary>
        public String Handler { get; set; }

        /// <summary>
        /// The image to filter on
        /// </summary>
        public String Image { get; set; }

        /// <summary>
        /// The text to filter on
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// The style to filter on
        /// </summary>
        public String Style { get; set; }

        /// <summary>
        /// The tooltip to filter on
        /// </summary>
        public String ToolTip { get; set; }

    }
}
