using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuItemMaintenance
{
    /// <summary>
    /// Service request object to save a application menu item
    /// </summary>
    public class AppMenuItemMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public Int32 AppMenuItemId { get; set; }
        public Int32 AppMenuId { get; set; }
        public Int32? ParentAppMenuItemId { get; set; }
        public String Name { get; set; }
        public String Handler { get; set; }
        public String Image { get; set; }
        public String Text { get; set; }
        public String Style { get; set; }
        public String ToolTip { get; set; }
        public Int32? Sort { get; set; }
        public Boolean Active { get; set; }
    }
}
