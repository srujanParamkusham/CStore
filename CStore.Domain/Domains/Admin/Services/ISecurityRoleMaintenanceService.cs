using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityRoleMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface ISecurityRoleMaintenanceService
    {
        SecurityRoleMaintenanceListResponse List(SecurityRoleMaintenanceListRequest request);
        SecurityRoleMaintenanceGetResponse Get(SecurityRoleMaintenanceGetRequest request);
        SecurityRoleMaintenanceSaveResponse Save(SecurityRoleMaintenanceSaveRequest request);
        SecurityRoleMaintenanceDeleteResponse Delete(SecurityRoleMaintenanceDeleteRequest request);
    }
}
