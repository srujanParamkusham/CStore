using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface ISecurityAccessMaintenanceService
    {
        SecurityAccessMaintenanceGetSecurableTreeResponse GetSecurableTree(
            SecurityAccessMaintenanceGetSecurableTreeRequest request);

        SecurityAccessMaintenanceGetEffectivePermissionsForSecurableResponse GetEffectivePermissionsForSecurable(
            SecurityAccessMaintenanceGetEffectivePermissionsForSecurableRequest request);

        SecurityAccessMaintenanceGetRolesResponse GetRoles(SecurityAccessMaintenanceGetRolesRequest request);

        SecurityAccessMaintenanceSavePermissionsResponse SavePermissions(
                    SecurityAccessMaintenanceSavePermissionsRequest request);
    }
}
