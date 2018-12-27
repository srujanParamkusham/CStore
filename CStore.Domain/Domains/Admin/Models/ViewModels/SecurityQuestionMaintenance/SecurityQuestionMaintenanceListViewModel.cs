using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityQuestionMaintenance
{

    public class SecurityQuestionMaintenanceListViewModel : DomainViewModel
    {
        /// The Security Question to filter on
        /// </summary>
        [Display(Name = "ID")]
        public Int32 SecurityQuestionId { get; set; }

        /// <summary>
        /// The Security Question to filter on
        /// </summary>
        [Display(Name = "Question")]
        [StringLength(100)]
        public String Question { get; set; }
    }
}