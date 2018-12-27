using CStore.Domain.Models.ServiceModels;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecuritySecurableMaintenance
{
    /// <summary>
    /// Service request object to save a security securable
    /// </summary>
    public class SecuritySecurableMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public int SecuritySecurableId { get; set; }
        public string Name { get; set; }
        public int? ParentSecuritySecurableId { get; set; }
    }
}
