using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityQuestionMaintenance
{

    public class SecurityQuestionMaintenanceEditViewModel : DomainViewModel
    {
        public int SecurityQuestionId { get; set; }

        [Display(Name = "Question")]
        [StringLength(255)]
        [Required]
        public String Question { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }
    }
}