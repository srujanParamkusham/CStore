using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityActionMaintenance
{

    public class SecurityActionMaintenanceListViewModel : DomainViewModel
    {
        [Display(Name = "Name")]
        [StringLength(255)]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(255)]
        public String Description { get; set; }

    }
}