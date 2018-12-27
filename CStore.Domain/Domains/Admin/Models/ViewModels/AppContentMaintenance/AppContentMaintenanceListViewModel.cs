using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppContentMaintenance
{

    public class AppContentMaintenanceListViewModel : DomainViewModel
    {
        [Display(Name = "App Content Id")]
        [Required]
        public Int32 AppContentId { get; set; }

        [Display(Name = "Content Group")]
        [StringLength(255)]
        public String ContentGroup { get; set; }

        [Display(Name = "Content Name")]
        [StringLength(255)]
        public String ContentName { get; set; }

        [Display(Name = "Content Value")]
        [StringLength(255)]
        public String ContentValue { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }
    }
}