using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppAnnouncementMaintenance
{
    /// <summary>
    /// Service request object to save a application announcement
    /// </summary>
    public class AppAnnouncementMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public Int32 AppAnnouncementId { get; set; }

        [Range(typeof(DateTime), "01/01/1900", "01/01/2099")]
        public DateTime? EffectiveDate { get; set; }

        [Range(typeof(DateTime), "01/01/1900", "01/01/2099")]
        public DateTime? ExpirationDate { get; set; }
        public String Subject { get; set; }
        public String AnnouncementText { get; set; }
        public Boolean ForceToTopOfList { get; set; }
        public Int32? Sort { get; set; }
        public Boolean Active { get; set; }

    }
}
