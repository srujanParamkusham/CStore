using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppVariableMaintenance
{

    public class AppVariableMaintenanceListViewModel : DomainViewModel
    {
        /// <summary>
        /// The Variable Group to filter on
        /// </summary>
        [Display(Name = "Group")]
        [StringLength(100)]
        public String VariableGroup { get; set; }

        /// <summary>
        /// The Variable Name to filter on
        /// </summary>
        [Display(Name = "Name")]
        [StringLength(100)]
        public String VariableName { get; set; }

        /// <summary>
        /// The Variable Name to filter on
        /// </summary>
        [Display(Name = "Value")]
        [StringLength(100)]
        public String VariableValue { get; set; }
    }
}