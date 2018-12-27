using CStore.Domain.Domains.Admin.Models.ServiceModels.SecuritySecurableMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface ISecuritySecurableMaintenanceService
    {
        SecuritySecurableMaintenanceListResponse List(SecuritySecurableMaintenanceListRequest request);
        SecuritySecurableMaintenanceGetResponse Get(SecuritySecurableMaintenanceGetRequest request);
        SecuritySecurableMaintenanceSaveResponse Save(SecuritySecurableMaintenanceSaveRequest request);
        SecuritySecurableMaintenanceDeleteResponse Delete(SecuritySecurableMaintenanceDeleteRequest request);

        SecuritySecurableMaintenanceGetAllSecurablesResponse GetAllSecurables(SecuritySecurableMaintenanceGetAllSecurablesRequest request);
    }
}
