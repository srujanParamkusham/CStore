using CStore.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityUserMaintenance
{

    public class SecurityUserMaintenanceListViewModel : DomainViewModel
    {
        [Display(Name = "User Name")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Display(Name = "Role")]
        public List<int> SecurityRoleIds { get; set; }

        [Display(Name = "Is a System Admin")]
        public bool SystemAdmin { get; set; }

        [Display(Name = "Only Show Active")]
        public bool Active { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Login Date Between")]
        public DateTime? LastLoginDateStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Last Login On or Before")]
        public DateTime? LastLoginDateEnd { get; set; }

    }
}