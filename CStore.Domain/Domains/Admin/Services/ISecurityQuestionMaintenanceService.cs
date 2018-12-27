using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityQuestionMaintenance;

namespace CStore.Domain.Domains.Admin.Services
{
    public interface ISecurityQuestionMaintenanceService
    {
        SecurityQuestionMaintenanceListResponse List(SecurityQuestionMaintenanceListRequest request);
        SecurityQuestionMaintenanceGetResponse Get(SecurityQuestionMaintenanceGetRequest request);
        SecurityQuestionMaintenanceSaveResponse Save(SecurityQuestionMaintenanceSaveRequest request);
        SecurityQuestionMaintenanceDeleteResponse Delete(SecurityQuestionMaintenanceDeleteRequest request);
    }
}
