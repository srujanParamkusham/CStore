using CStore.Domain.Domains.Admin.Models.ServiceModels.AppContentMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface IAppContentMaintenanceService
    {
        AppContentMaintenanceListResponse List(AppContentMaintenanceListRequest request);
        AppContentMaintenanceGetResponse Get(AppContentMaintenanceGetRequest request);
        AppContentMaintenanceSaveResponse Save(AppContentMaintenanceSaveRequest request);
        AppContentMaintenanceDeleteResponse Delete(AppContentMaintenanceDeleteRequest request);
    }
}
