using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppCodeDetailMaintenance
{

    public class AppCodeDetailMaintenanceListViewModel : DomainViewModel
    {
        [Display(Name = "App CodeDetail Id")]
        [Required]
        public Int32 AppCodeDetailId { get; set; }

        [Display(Name = "Code Group")]
        [StringLength(255)]
        public String CodeGroup { get; set; }

        [Display(Name = "Code Value")]
        [StringLength(255)]
        public String CodeValue { get; set; }

        [Display(Name = "Description")]
        [StringLength(255)]
        public String Description { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }

    }
}