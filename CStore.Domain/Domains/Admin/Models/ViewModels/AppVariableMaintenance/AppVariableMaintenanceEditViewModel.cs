using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppVariableMaintenance
{

    public class AppVariableMaintenanceEditViewModel : DomainViewModel
    {
        public int AppVariableId { get; set; }

        [Display(Name = "Group")]
        [StringLength(255)]
        public String VariableGroup { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public String VariableName { get; set; }

        [Display(Name = "Value")]
        [StringLength(255)]
        public String VariableValue { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }
    }
}