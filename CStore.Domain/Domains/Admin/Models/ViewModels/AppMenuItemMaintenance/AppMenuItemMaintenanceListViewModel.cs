using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppMenuItemMaintenance
{

    public class AppMenuItemMaintenanceListViewModel : DomainViewModel
    {
        [Display(Name = "ParentAppMenuItemId")]
        [StringLength(2000)]
        public String ParentAppMenuItemId { get; set; }

        [Display(Name = "AppMenuId")]
        [StringLength(2000)]
        public String AppMenuId { get; set; }

        [Display(Name = "Name")]
        [StringLength(2000)]
        public String Name { get; set; }

        [Display(Name = "Handler")]
        [StringLength(2000)]
        public String Handler { get; set; }

        [Display(Name = "Image")]
        [StringLength(2000)]
        public String Image { get; set; }

        [Display(Name = "Text")]
        [StringLength(2000)]
        public String Text { get; set; }

        [Display(Name = "Style")]
        [StringLength(2000)]
        public String Style { get; set; }

        [Display(Name = "Tooltip")]
        [StringLength(2000)]
        public String Tooltip { get; set; }

    }
}