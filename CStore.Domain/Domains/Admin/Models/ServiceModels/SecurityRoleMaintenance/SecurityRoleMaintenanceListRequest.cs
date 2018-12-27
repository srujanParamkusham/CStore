using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityRoleMaintenance
{
    /// <summary>
    /// Service request object to search for a list of security roles
    /// </summary>
    public class SecurityRoleMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The name to filter on
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The description to filter on
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The AD Group Name to filter on
        /// </summary>
        public String ADGroupName { get; set; }

    }
}
