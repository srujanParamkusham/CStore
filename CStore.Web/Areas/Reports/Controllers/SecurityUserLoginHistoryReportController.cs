using System;
using System.Linq;
using System.Web.Mvc;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Reports.Models.ServiceModels.SecurityUserLoginHistoryReport;
using CStore.Domain.Domains.Reports.Models.ViewModels.SecurityUserLoginHistoryReport;
using CStore.Domain.Domains.Reports.Services;


namespace CStore.Web.Areas.Reports.Controllers
{
    /// <summary>
    /// Controller used for handling the report params page and returning the result
    /// </summary>
    public partial class SecurityUserLoginHistoryReportController : DomainReportController
    {
        #region Member Variables

        private IRepository _repository;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public SecurityUserLoginHistoryReportController(IRepository repository)
            : base()
        {
            _repository = repository;
        }
        #endregion

        #region Index Screen
        /// <summary>
        /// Display the report parameter screen.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Displays the initial view of the page</returns>
        public virtual ActionResult Index(SecurityUserLoginHistoryReportParamsViewModel model)
        {
            if (String.IsNullOrWhiteSpace(model.SubmitAction))
            {
                //
                //Initialize the model with default params
                //
                if (model.StartDate == null)
                {
                    model.StartDate = DateTime.Now.AddDays(-60);
                }
                if (model.EndDate == null)
                {
                    model.EndDate = DateTime.Now;
                }
            }

            return Index<SecurityUserLoginHistoryReportParamsViewModel, SecurityUserLoginHistoryReportService, SecurityUserLoginHistoryReportGenerateReportRequest, SecurityUserLoginHistoryReportGenerateReportResponse>(model);
        }
        #endregion

    }
}
