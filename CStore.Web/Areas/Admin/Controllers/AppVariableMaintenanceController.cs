using AutoMapper;
using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Models.JQueryDataTables;
using Newtonsoft.Json;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Admin.Models.ServiceModels.AppVariableMaintenance;
using CStore.Domain.Domains.Admin.Models.ViewModels.AppVariableMaintenance;
using CStore.Domain.Domains.Admin.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace CStore.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller used for handling the app variable maintenance administration screens
    /// </summary>
    public partial class AppVariableMaintenanceController : DomainEntityController
    {
        #region Member Variables
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public AppVariableMaintenanceController()
            : base()
        {
        }
        #endregion

        #region Index/List Records Screen
        /// <summary>
        /// Display the Base View for the maintenance screen. This allows the user to search for and see a list of the records.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Displays the initial view of the page</returns>
        public virtual ActionResult Index(AppVariableMaintenanceListViewModel model)
        {
            return Index<AppVariableMaintenanceListViewModel>(model);
        }

        /// <summary>
        /// List Implementation for the new screen. This returns an AJAX response for the datatables object on the page.
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="dataTablesModel">Data Table Model</param>
        /// <returns></returns>
        public virtual ActionResult List(AppVariableMaintenanceListViewModel model = null, [ModelBinder(typeof(JQueryDataTablesModelBinder))]JQueryDataTablesParameterModel dataTablesModel = null)
        {
            return JQueryDataTablesList<AppVariableMaintenanceListViewModel, AppVariableMaintenanceService, AppVariableMaintenanceListRequest, AppVariableMaintenanceListResponse, AppVariable>(model, dataTablesModel);
        }
        #endregion

        #region Add or edit a record
        /// <summary>
        /// Add or Edit a Record
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Edit(int? id = null, AppVariableMaintenanceEditViewModel model = null)
        {
            int itemId = id.HasValue ? id.Value : 0;
            return this.Edit<AppVariableMaintenanceEditViewModel, int, AppVariableMaintenanceService, AppVariableMaintenanceGetRequest, AppVariableMaintenanceGetResponse, AppVariable>(itemId, model);
        }

        /// <summary>
        /// On Creation of a new record set the initial values
        /// </summary>
        /// <typeparam name="M">Model Type</typeparam>
        /// <param name="viewModel">View for new model</param>
        protected override void OnEditNewRecord<M>(M viewModel)
        {
            AppVariableMaintenanceEditViewModel model = viewModel as AppVariableMaintenanceEditViewModel;
            model.Active = true;
        }

        /// <summary>
        /// Add or Edit a Record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Edit(AppVariableMaintenanceEditViewModel model)
        {
            return this.Edit<AppVariableMaintenanceEditViewModel, int, AppVariableMaintenanceService, AppVariableMaintenanceSaveRequest, AppVariableMaintenanceSaveResponse, AppVariable>(model);
        }

        /// <summary>
        /// Allows you update the mapping of the model after the record has been saved and converted to the edit view model
        /// </summary>
        /// <typeparam name="M">Model</typeparam>
        /// <typeparam name="E">Entity</typeparam>
        /// <param name="model">Model</param>
        /// <param name="entity">Entity</param>
        protected override ActionResult OnEditSavedMapping<M, P, R, E>(M model, E Entity, P request, R response)
        {
            AppVariableMaintenanceEditViewModel editModel = model as AppVariableMaintenanceEditViewModel;

            // do not return an alternate result
            return null;
        }
        #endregion

        #region Delete a record
        /// <summary>
        /// Delete a Record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Delete(IEnumerable<int> ids)
        {
            return this.Delete<int, AppVariableMaintenanceService, AppVariableMaintenanceDeleteRequest, AppVariableMaintenanceDeleteResponse, AppVariable>(ids);
        }
        #endregion
    }
}
