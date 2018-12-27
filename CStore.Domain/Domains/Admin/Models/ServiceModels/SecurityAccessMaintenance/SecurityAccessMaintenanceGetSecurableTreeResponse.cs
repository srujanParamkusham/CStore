using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Service response object to get the tree model to display of securables
    /// </summary>
    public class SecurityAccessMaintenanceGetSecurableTreeResponse : DomainServiceResponse
    {
        public List<SecuritySecurableTreeModel> SecuritySecurableTreeModel { get; set; }
    }
}
