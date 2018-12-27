using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppEmailTemplateMaintenance
{
    /// <summary>
    /// Service request object to search for a list of application email templates
    /// </summary>
    public class AppEmailTemplateMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The template code to filter on
        /// </summary>
        public String TemplateCode { get; set; }

        /// <summary>
        /// The template name to filter on
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The template description to filter on
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The email subject to filter on
        /// </summary>
        public String EmailSubject { get; set; }


    }
}
