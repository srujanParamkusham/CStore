using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppCodeDetailMaintenance
{
    /// <summary>
    /// Service request object to search for a list of application code
    /// </summary>
    public class AppCodeDetailMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The code group to filter on
        /// </summary>
        public String CodeGroup { get; set; }

        /// <summary>
        /// The code value to filter on
        /// </summary>
        public String CodeValue { get; set; }

        /// <summary>
        /// The code description to filter on
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The code description to filter on
        /// </summary>
        public Boolean Active { get; set; }


    }
}
