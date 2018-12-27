using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppMenuMaintenance
{

    public class AppMenuMaintenanceEditViewModel : DomainViewModel
    {
        public int AppMenuId { get; set; }

        [Display(Name = "Menu Code")]
        [StringLength(2000)]
        [Required]
        public String MenuCode { get; set; }

        [Display(Name = "Name")]
        [StringLength(2000)]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000)]
        public String Description { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }
    }
}