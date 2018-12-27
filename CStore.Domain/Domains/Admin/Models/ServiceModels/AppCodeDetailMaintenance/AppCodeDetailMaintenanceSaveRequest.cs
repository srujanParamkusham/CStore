using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppCodeDetailMaintenance
{
    /// <summary>
    /// Service request object to save application code details
    /// </summary>
    public class AppCodeDetailMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public Int32 AppCodeDetailId { get; set; }
        public String CodeGroup { get; set; }
        public String CodeValue { get; set; }
        public String Description { get; set; }
        public Int32 Sort { get; set; }
        public Boolean Default { get; set; }
        public Boolean Active { get; set; }
    }
}
