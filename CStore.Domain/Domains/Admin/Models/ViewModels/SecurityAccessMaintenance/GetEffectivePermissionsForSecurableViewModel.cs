using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Util.Extensions;
using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance;
using CStore.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityAccessMaintenance
{

    public class GetEffectivePermissionsForSecurableViewModel : DomainViewModel
    {
        public int? SecuritySecurableId { get; set; }

        public SecurityAccessMaintenanceGetEffectivePermissionsForSecurableResponse EffectivePermissionsForSecurable { get; set; }

        /// <summary>
        /// This contains the settings posted to the controller. 
        /// The key is the the security securable action ID + "~" + security role Id
        /// The setting is if its allowed, not allowed or inherited.
        /// </summary>
        public Dictionary<String, String> SecurityAccessSettings { get; set; }

        /// <summary>
        /// The roles available to configure permissions for
        /// </summary>
        [Display(Name = "Available Roles")]
        public List<SecurityRole> AvailableRoles { get; set; }

        public List<SelectListItem> AvailableRolesSelectList
        {
            get
            {
                if (AvailableRoles == null)
                {
                    return new List<SelectListItem>();
                }
                else
                {
                    return AvailableRoles.ToSelectList(p => p.Name, p => p.SecurityRoleId.ToString());
                }
            }
        }

        /// <summary>
        /// The roles selected to configure permissions for
        /// </summary>
        [Display(Name = "Selected Roles")]
        public List<SecurityRole> SelectedRoles { get; set; }

        public List<SelectListItem> SelectedRolesSelectList
        {
            get
            {
                if (SelectedRoles == null)
                {
                    return new List<SelectListItem>();
                }
                else
                {
                    return SelectedRoles.ToSelectList(p => p.Name, p => p.SecurityRoleId.ToString());
                }
            }
        }

        public List<int> SelectedRoleIds { get; set; }
    }
}