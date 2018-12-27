using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityActionMaintenance
{

    public class SecurityActionMaintenanceEditViewModel : DomainViewModel
    {
        public int SecurityActionId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000)]
        public String Description { get; set; }


    }
}