using CStore.Domain.Models.ServiceModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.AppVariableMaintenance
{
    /// <summary>
    /// Service request object to save a app variables
    /// </summary>
    public class AppVariableMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public Int32 AppVariableId { get; set; }
        public string VariableGroup { get; set; }
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
        public Boolean Active { get; set; }
    }
}
