using CStore.Domain.Models.ServiceModels;
using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppVariableMaintenance
{
    /// <summary>
    /// Service request object to search for a list of app variables
    /// </summary>
    public class AppVariableMaintenanceListRequest : DomainServicePagedListRequest
    {
        /// <summary>
        /// The Variable Group to filter on
        /// </summary>
        public String VariableGroup { get; set; }

        /// <summary>
        /// The Variable Name to filter on
        /// </summary>
        public String VariableName { get; set; }

        /// <summary>
        /// The Variable Value to filter on
        /// </summary>
        public String VariableValue { get; set; }

    }
}
