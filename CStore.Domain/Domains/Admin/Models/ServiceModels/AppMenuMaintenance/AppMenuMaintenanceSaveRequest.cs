using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuMaintenance
{
    /// <summary>
    /// Service request object to save a application menu
    /// </summary>
    public class AppMenuMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public Int32 AppMenuId { get; set; }

        public String MenuCode { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Boolean Active { get; set; }
    }
}
