using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppAnnouncementMaintenance
{

    public class AppAnnouncementMaintenanceListViewModel : DomainViewModel
    {
        [Display(Name = "Subject")]
        [StringLength(100)]
        public String Subject { get; set; }

        [Display(Name = "Announcement Text")]
        public String AnnouncementText { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Effective Date Between")]
        public DateTime? EffectiveDateStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Effective Date On or Before")]
        public DateTime? EffectiveDateEnd { get; set; }

    }
}