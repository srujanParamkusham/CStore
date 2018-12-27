using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AppMenuMaintenance
{

    public class AppMenuMaintenanceListViewModel : DomainViewModel
    {
        /// <summary>
        /// The menu code to filter on
        /// </summary>
        [Display(Name = "Menu Code")]
        [StringLength(100)]
        public String MenuCode { get; set; }

        /// <summary>
        /// The name to filter on
        /// </summary>
        [Display(Name = "Name")]
        [StringLength(100)]
        public String Name { get; set; }

        /// <summary>
        /// The description to filter on
        /// </summary>
        [Display(Name = "Description")]
        [StringLength(100)]
        public String Description { get; set; }

    }
}