using Catalyst.MVC.Infrastructure.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CStore.Domain.Models.ViewModels
{
    /// <summary>
    /// Base MVC report parameters page view model that all other report parameter view models should extend.
    /// </summary>
    [Serializable]
    public class DomainReportParamsViewModel : BaseReportParamsViewModel
    {
        public List<SelectListItem> OutputFormatsSelectList
        {
            get
            {
                var items = new List<SelectListItem>();
                items.Add(new SelectListItem() { Text = "PDF", Value = "PDF" });
                items.Add(new SelectListItem() { Text = "CSV", Value = "CSV" });
                items.Add(new SelectListItem() { Text = "Excel", Value = "EXCEL" });
                return items;
            }
        }
    }
}
