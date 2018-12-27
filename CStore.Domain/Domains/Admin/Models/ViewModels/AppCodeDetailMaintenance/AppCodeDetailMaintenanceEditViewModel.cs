using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppCodeDetailMaintenance
{

    public class AppCodeDetailMaintenanceEditViewModel : DomainViewModel
    {
        public int AppCodeDetailId { get; set; }

        [Display(Name = "Code Group")]
        [StringLength(255)]
        [Required]
        public String CodeGroup { get; set; }

        [Display(Name = "Code Value")]
        [StringLength(255)]
        [Required]
        public String CodeValue { get; set; }

        [Display(Name = "Description")]
        [StringLength(255)]
        public String Description { get; set; }

        [Display(Name = "Sort")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32? Sort { get; set; }

        [Display(Name = "Default")]
        public Boolean Default { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }

    }
}