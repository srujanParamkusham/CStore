using CStore.Domain.Domains.Admin.Models.ServiceModels.AppCodeDetailMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface IAppCodeDetailMaintenanceService
    {
        AppCodeDetailMaintenanceListResponse List(AppCodeDetailMaintenanceListRequest request);
        AppCodeDetailMaintenanceGetResponse Get(AppCodeDetailMaintenanceGetRequest request);
        AppCodeDetailMaintenanceSaveResponse Save(AppCodeDetailMaintenanceSaveRequest request);
        AppCodeDetailMaintenanceDeleteResponse Delete(AppCodeDetailMaintenanceDeleteRequest request);
    }
}
