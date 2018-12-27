using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppEmailTemplateMaintenance
{
    /// <summary>
    /// Service request object to save a application email template
    /// </summary>
    public class AppEmailTemplateMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public Int32 AppEmailTemplateId { get; set; }
        public String TemplateCode { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String EmailTo { get; set; }
        public String EmailCC { get; set; }
        public String EmailBCC { get; set; }
        public String EmailFrom { get; set; }
        public String EmailFromDisplayName { get; set; }
        public String EmailSubject { get; set; }
        public String EmailBody { get; set; }
        public Boolean Html { get; set; }
        public Boolean Active { get; set; }
    }
}
