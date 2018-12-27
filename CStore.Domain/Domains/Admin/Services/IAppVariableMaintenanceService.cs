using CStore.Domain.Domains.Admin.Models.ServiceModels.AppVariableMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface IAppVariableMaintenanceService
    {
        AppVariableMaintenanceListResponse List(AppVariableMaintenanceListRequest request);
        AppVariableMaintenanceGetResponse Get(AppVariableMaintenanceGetRequest request);
        AppVariableMaintenanceSaveResponse Save(AppVariableMaintenanceSaveRequest request);
        AppVariableMaintenanceDeleteResponse Delete(AppVariableMaintenanceDeleteRequest request);
    }
}
