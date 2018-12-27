using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppAnnouncementMaintenance
{

    public class AppAnnouncementMaintenanceEditViewModel : DomainViewModel
    {
        public int AppAnnouncementId { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EffectiveDate { get; set; }

        [Display(Name = "Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Subject")]
        [StringLength(2000)]
        [Required]
        public String Subject { get; set; }

        [Display(Name = "Announcement Text")]
        [StringLength(16000)]
        [AllowHtml]
        public String AnnouncementText { get; set; }

        [Display(Name = "Sticky / Force To Top Of List")]
        public Boolean ForceToTopOfList { get; set; }

        [Display(Name = "Sort")]
        public Int32? Sort { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }

    }
}