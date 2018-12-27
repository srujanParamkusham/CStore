using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppContentMaintenance
{
    /// <summary>
    /// Service request object to save application content
    /// </summary>
    public class AppContentMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public Int32 AppContentId { get; set; }
        public String ContentGroup { get; set; }
        public String ContentName { get; set; }
        public String ContentValue { get; set; }
        public Boolean Active { get; set; }
    }
}
