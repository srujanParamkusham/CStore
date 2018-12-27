using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppContentMaintenance
{
    /// <summary>
    /// Service request object to search for list of application content
    /// </summary>
    public class AppContentMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The template code to filter on
        /// </summary>
        public String ContentGroup { get; set; }

        /// <summary>
        /// The template name to filter on
        /// </summary>
        public String ContentName { get; set; }

        /// <summary>
        /// The template description to filter on
        /// </summary>
        public String ContentValue { get; set; }

        /// <summary>
        /// The email subject to filter on
        /// </summary>
        public Boolean Active { get; set; }


    }
}
