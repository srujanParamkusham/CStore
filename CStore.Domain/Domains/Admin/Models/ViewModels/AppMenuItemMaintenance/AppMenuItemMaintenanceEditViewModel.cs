using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Util.Extensions;
using CStore.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppMenuItemMaintenance
{
    public class AppMenuItemMaintenanceEditViewModel : DomainViewModel
    {
        public int AppMenuItemId { get; set; }

        [Display(Name = "Menu")]
        [Required]
        public String AppMenuId { get; set; }

        [Display(Name = "Parent Menu Item")]
        public int ParentAppMenuItemId { get; set; }

        [Display(Name = "Name")]
        [StringLength(2000)]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Handler")]
        [StringLength(2000)]
        public String Handler { get; set; }

        [Display(Name = "Image")]
        [StringLength(2000)]
        public String Image { get; set; }

        [Display(Name = "Text")]
        [StringLength(2000)]
        public String Text { get; set; }

        [Display(Name = "Style")]
        [StringLength(2000)]
        public String Style { get; set; }

        [Display(Name = "Tooltip")]
        [StringLength(2000)]
        public String Tooltip { get; set; }

        [Display(Name = "Sort")]
        public Int32? Sort { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }

        /// <summary>
        /// The elements for the select list to choose the parent app menu for this item
        /// </summary>
        [Display(Name = "App Menu")]
        public List<AppMenu> AppMenuList { get; set; }

        [Display(Name = "App Menu")]
        public List<SelectListItem> AppMenuSelectList
        {
            get
            {
                if (AppMenuList == null)
                {
                    return new List<SelectListItem>();
                }
                else
                {
                    return AppMenuList.ToSelectListWithDefaultEmptyOption(p => p.Name, p => p.AppMenuId.ToString());
                }
            }
        }

        /// <summary>
        /// The elements for the select list to choose the parent app menu item for this item
        /// </summary>
        [Display(Name = "Parent Menu Item")]
        public List<AppMenuItem> ParentAppMenuItemList { get; set; }

        [Display(Name = "Parent Menu Item")]
        public List<SelectListItem> ParentAppMenuItemSelectList
        {
            get
            {
                if (ParentAppMenuItemList == null)
                {
                    return new List<SelectListItem>();
                }
                else
                {
                    return ParentAppMenuItemList.ToSelectListWithDefaultEmptyOption(p => p.Name, p => p.AppMenuId.ToString());
                }
            }
        }

    }
}