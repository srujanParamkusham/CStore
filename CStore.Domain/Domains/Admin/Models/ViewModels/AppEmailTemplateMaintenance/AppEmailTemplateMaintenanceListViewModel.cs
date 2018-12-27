using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance
{

    public class AppEmailTemplateMaintenanceListViewModel : DomainViewModel
    {
        /// <summary>
        /// The template code to filter on
        /// </summary>
        [Display(Name = "Code")]
        [StringLength(100)]
        public String TemplateCode { get; set; }

        /// <summary>
        /// The template name to filter on
        /// </summary>
        [Display(Name = "Name")]
        [StringLength(100)]
        public String Name { get; set; }

        /// <summary>
        /// The template description to filter on
        /// </summary>
        [Display(Name = "Description")]
        [StringLength(100)]
        public String Description { get; set; }

        /// <summary>
        /// The email subject to filter on
        /// </summary>
        [Display(Name = "Subject")]
        [StringLength(100)]
        public String EmailSubject { get; set; }
    }
}