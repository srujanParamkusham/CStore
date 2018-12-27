using CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface IAppMenuMaintenanceService
    {
        AppMenuMaintenanceListResponse List(AppMenuMaintenanceListRequest request);
        AppMenuMaintenanceGetResponse Get(AppMenuMaintenanceGetRequest request);
        AppMenuMaintenanceSaveResponse Save(AppMenuMaintenanceSaveRequest request);
        AppMenuMaintenanceDeleteResponse Delete(AppMenuMaintenanceDeleteRequest request);
    }
}
