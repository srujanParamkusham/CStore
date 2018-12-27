using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Util.Extensions;
using CStore.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecuritySecurableMaintenance
{

    public class SecuritySecurableMaintenanceEditViewModel : DomainViewModel
    {
        public int SecuritySecurableTemplateId { get; set; }

        [Display(Name = "SecuritySecurableId")]
        [Required]
        public Int32 SecuritySecurableId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Parent Securable")]
        public Int32? ParentSecuritySecurableId { get; set; }

        [Display(Name = "Available Securables")]
        public List<SecuritySecurable> PossibleParentSecuritySecurables { get; set; }

        [Display(Name = "Available Securables")]
        public List<SelectListItem> PossibleParentSecuritySecurablesSelectList
        {
            get
            {
                if (PossibleParentSecuritySecurables == null)
                {
                    return new List<SelectListItem>();
                }
                else
                {
                    return PossibleParentSecuritySecurables.ToSelectListWithDefaultEmptyOption(p => p.Name, p => p.SecuritySecurableId.ToString());
                }
            }
        }

    }
}