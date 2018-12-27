using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityActionMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface ISecurityActionMaintenanceService
    {
        SecurityActionMaintenanceListResponse List(SecurityActionMaintenanceListRequest request);
        SecurityActionMaintenanceGetResponse Get(SecurityActionMaintenanceGetRequest request);
        SecurityActionMaintenanceSaveResponse Save(SecurityActionMaintenanceSaveRequest request);
        SecurityActionMaintenanceDeleteResponse Delete(SecurityActionMaintenanceDeleteRequest request);
    }
}
