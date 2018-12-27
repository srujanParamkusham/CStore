using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityRoleMaintenance
{

    public class SecurityRoleMaintenanceEditViewModel : DomainViewModel
    {
        public int SecurityRoleId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000)]
        public String Description { get; set; }

        [Display(Name = "AD Group Name")]
        [StringLength(255)]
        public String ADGroupName { get; set; }

        [Display(Name = "Default For New Users")]
        public Boolean Default { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }

    }
}