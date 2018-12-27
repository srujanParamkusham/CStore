using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppAnnouncementMaintenance
{
    /// <summary>
    /// Service request object to search for a list of application announcements
    /// </summary>
    public class AppAnnouncementMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The subject to filter on
        /// </summary>
        public String Subject { get; set; }

        /// <summary>
        /// The announcement text to filter on
        /// </summary>
        public String AnnouncementText { get; set; }

        /// <summary>
        /// The starting Effective date to filter on
        /// </summary>
        public DateTime? EffectiveDateStart { get; set; }

        /// <summary>
        /// The ending Effective date to filter on
        /// </summary>
        public DateTime? EffectiveDateEnd { get; set; }


    }
}
