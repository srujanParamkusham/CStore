using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityQuestionMaintenance
{
    /// <summary>
    /// Service request object to search for a list of security questions
    /// </summary>
    public class SecurityQuestionMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The ID to filter on
        /// </summary>
        public Int32 SecurityQuestionId { get; set; }

        /// <summary>
        /// The Question to filter on
        /// </summary>
        public String Question { get; set; }
    }
}
