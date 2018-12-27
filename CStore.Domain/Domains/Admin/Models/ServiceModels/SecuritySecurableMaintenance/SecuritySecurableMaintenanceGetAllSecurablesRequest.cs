using CStore.Domain.Models.ServiceModels;
using System.Collections.Generic;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecuritySecurableMaintenance
{
    /// <summary>
    /// Service request object to get all of the security securables, used to create select lists with
    /// </summary>
    public class SecuritySecurableMaintenanceGetAllSecurablesRequest : DomainServiceRequest
    {
        /// <summary>
        /// The securables with IDs in this list will not be included in the results.
        /// </summary>
        public List<int> SecuritySecurableIdExcludeList { get; set; }
    }
}
