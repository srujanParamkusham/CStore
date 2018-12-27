using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityActionMaintenance
{
    /// <summary>
    /// Service request object to save a Security Action
    /// </summary>
    public class SecurityActionMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public Int32 SecurityActionId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
