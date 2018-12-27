using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityActionMaintenance
{
    /// <summary>
    /// Service request object to search for a list of security Actions
    /// </summary>
    public class SecurityActionMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The Name to filter on
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The Description to filter on
        /// </summary>
        public String Description { get; set; }
    }
}
