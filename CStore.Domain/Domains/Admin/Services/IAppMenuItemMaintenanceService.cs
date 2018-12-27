using CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuItemMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface IAppMenuItemMaintenanceService
    {
        AppMenuItemMaintenanceListResponse List(AppMenuItemMaintenanceListRequest request);
        AppMenuItemMaintenanceGetResponse Get(AppMenuItemMaintenanceGetRequest request);
        AppMenuItemMaintenanceSaveResponse Save(AppMenuItemMaintenanceSaveRequest request);
        AppMenuItemMaintenanceDeleteResponse Delete(AppMenuItemMaintenanceDeleteRequest request);

        AppMenuItemMaintenanceGetAllAppMenusResponse GetAllAppMenus(
            AppMenuItemMaintenanceGetAllAppMenusRequest request);

        AppMenuItemMaintenanceGetAllAppMenuItemsResponse GetAllAppMenuItems(
            AppMenuItemMaintenanceGetAllAppMenuItemsRequest request);
    }
}
