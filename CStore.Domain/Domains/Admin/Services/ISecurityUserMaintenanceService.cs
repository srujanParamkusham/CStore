using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityUserMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface ISecurityUserMaintenanceService
    {
        SecurityUserMaintenanceGetRolesForUserResponse GetRolesForUser(SecurityUserMaintenanceGetRolesForUserRequest request);

    }
}
