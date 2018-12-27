using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityQuestionMaintenance
{
    /// <summary>
    /// Service request object to save a Security Question
    /// </summary>
    public class SecurityQuestionMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public Int32 SecurityQuestionId { get; set; }
        public String Question { get; set; }
        public Boolean Active { get; set; }
    }
}
