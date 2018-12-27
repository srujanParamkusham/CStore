using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecuritySecurableMaintenance
{

    public class SecuritySecurableMaintenanceListViewModel : DomainViewModel
    {
        /// <summary>
        /// The name to filter on
        /// </summary>
        [Display(Name = "Name")]
        [StringLength(100)]
        public String Name { get; set; }


        /// <summary>
        /// The parent securable name to filter on
        /// </summary>
        [Display(Name = "Parent Securable Name")]
        [StringLength(100)]
        public String ParentSecuritySecurableName { get; set; }
    }
}