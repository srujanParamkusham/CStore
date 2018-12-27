using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppEmailTemplateMaintenance
{
    /// <summary>
    /// Service response object to search for a list of application email templates
    /// </summary>
    public class AppEmailTemplateMaintenanceListResponse : DomainServicePagedListResponse<AppEmailTemplate>
    {

    }
}
