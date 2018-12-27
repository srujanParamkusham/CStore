using System;
using System.ComponentModel.DataAnnotations;
using CStore.Domain.Models.ViewModels;

namespace CStore.Domain.Domains.Reports.Models.ViewModels.SecurityUserLoginHistoryReport
{
    public class SecurityUserLoginHistoryReportParamsViewModel : DomainReportParamsViewModel
    {
        [Display(Name = "From Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? StartDate { get; set; }

        [Display(Name = "To Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? EndDate { get; set; }

        [Display(Name = "User")]
        public int? SecurityUserId { get; set; }
    }
}
