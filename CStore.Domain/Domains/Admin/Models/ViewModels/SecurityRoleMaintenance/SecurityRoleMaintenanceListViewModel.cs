using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityRoleMaintenance

{

    public class SecurityRoleMaintenanceListViewModel : DomainViewModel
    {
        [Display(Name = "Name")]
        [StringLength(100)]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(100)]
        public String Description { get; set; }

        [Display(Name = "AD Group Name")]
        [StringLength(100)]
        public String ADGroupName { get; set; }


    }
}