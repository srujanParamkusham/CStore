using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecuritySecurableMaintenance
{
    /// <summary>
    /// Service request object to search for a list of security securables
    /// </summary>
    public class SecuritySecurableMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The name to filter on
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The parent securable name to filter on
        /// </summary>
        public String ParentSecuritySecurableName { get; set; }

    }
}
