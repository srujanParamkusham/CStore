using CStore.Domain.Domains.Admin.Models.ServiceModels.AppAnnouncementMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface IAppAnnouncementMaintenanceService
    {
        AppAnnouncementMaintenanceListResponse List(AppAnnouncementMaintenanceListRequest request);
        AppAnnouncementMaintenanceGetResponse Get(AppAnnouncementMaintenanceGetRequest request);
        AppAnnouncementMaintenanceSaveResponse Save(AppAnnouncementMaintenanceSaveRequest request);
        AppAnnouncementMaintenanceDeleteResponse Delete(AppAnnouncementMaintenanceDeleteRequest request);
    }
}
