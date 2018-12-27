using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppAnnouncementMaintenance
{
    /// <summary>
    /// Service response object to search for a list of application announcements
    /// </summary>
    public class AppAnnouncementMaintenanceListResponse : DomainServicePagedListResponse<AppAnnouncement>
    {

    }
}
