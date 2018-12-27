using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance
{

    public class AppEmailTemplateMaintenanceEditViewModel : DomainViewModel
    {
        public int AppEmailTemplateId { get; set; }

        [Display(Name = "Code")]
        [StringLength(255)]
        [Required]
        public String TemplateCode { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000)]
        public String Description { get; set; }

        [Display(Name = "Email To")]
        [StringLength(2000)]
        public String EmailTo { get; set; }

        [Display(Name = "Email CC")]
        [StringLength(2000)]
        public String EmailCC { get; set; }

        [Display(Name = "Email BCC")]
        [StringLength(2000)]
        public String EmailBCC { get; set; }

        [Display(Name = "Email From")]
        [StringLength(2000)]
        public String EmailFrom { get; set; }

        [Display(Name = "Email From Display Name")]
        [StringLength(2000)]
        public String EmailFromDisplayName { get; set; }

        [Display(Name = "Email Subject")]
        [StringLength(2000)]
        public String EmailSubject { get; set; }

        [Display(Name = "Email Body")]
        [StringLength(16000)]
        [AllowHtml]
        public String EmailBody { get; set; }

        [Display(Name = "HTML Email")]
        public Boolean Html { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }

    }
}