using CStore.Domain.Domains.General.Models.ServiceModels.AppAnnouncements;

namespace CStore.Domain.Domains.General.Services
{
    public interface IAppAnnouncementsService
    {
        GetActiveAnnouncementsResponse GetActiveAnnouncements(GetActiveAnnouncementsRequest request);
    }
}
