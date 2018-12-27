using CStore.Domain.Domains.Admin.Models.ServiceModels.AppEmailTemplateMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface IAppEmailTemplateMaintenanceService
    {
        AppEmailTemplateMaintenanceListResponse List(AppEmailTemplateMaintenanceListRequest request);
        AppEmailTemplateMaintenanceGetResponse Get(AppEmailTemplateMaintenanceGetRequest request);
        AppEmailTemplateMaintenanceSaveResponse Save(AppEmailTemplateMaintenanceSaveRequest request);
        AppEmailTemplateMaintenanceDeleteResponse Delete(AppEmailTemplateMaintenanceDeleteRequest request);
    }
}
