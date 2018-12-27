using AutoMapper;
using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Models.JQueryDataTables;
using Newtonsoft.Json;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Admin.Models.ServiceModels.AppMenuItemMaintenance;
using CStore.Domain.Domains.Admin.Models.ViewModels.AppMenuItemMaintenance;
using CStore.Domain.Domains.Admin.Services;
using CStore.Domain.HTMLHelpers.Extensions.Grid.DataTablesReadonly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace CStore.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller used for handling the app menu maintenance administration screens
    /// </summary>
    public partial class AppMenuItemMaintenanceController : DomainEntityController
    {
        #region Member Variables

        private IAppMenuItemMaintenanceService _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public AppMenuItemMaintenanceController(IAppMenuItemMaintenanceService service)
            : base()
        {
            _service = service;
        }
        #endregion

        #region Index/List Records Screen
        /// <summary>
        /// Display the Base View for the maintenance screen. This allows the user to search for and see a list of the records.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Displays the initial view of the page</returns>
        public virtual ActionResult Index(AppMenuItemMaintenanceListViewModel model)
        {
            return Index<AppMenuItemMaintenanceListViewModel>(model);
        }

        /// <summary>
        /// List Implementation for the new screen. This returns an AJAX response for the datatables object on the page.
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="dataTablesModel">Data Table Model</param>
        /// <returns></returns>
        public virtual ActionResult List(AppMenuItemMaintenanceListViewModel model = null,
            [ModelBinder(typeof(JQueryDataTablesModelBinder))]JQueryDataTablesParameterModel dataTablesModel = null)
        {
            return JQueryDataTablesList<AppMenuItemMaintenanceListViewModel, AppMenuItemMaintenanceService, AppMenuItemMaintenanceListRequest, AppMenuItemMaintenanceListResponse, AppMenuItem>(model, dataTablesModel);
        }
        #endregion

        #region Add or edit a record
        /// <summary>
        /// Add or Edit a Record
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Edit(int? id = null, AppMenuItemMaintenanceEditViewModel model = null)
        {
            int itemId = id.HasValue ? id.Value : 0;
            return this.Edit<AppMenuItemMaintenanceEditViewModel, int, AppMenuItemMaintenanceService, AppMenuItemMaintenanceGetRequest, AppMenuItemMaintenanceGetResponse, AppMenuItem>(itemId, model);
        }

        /// <summary>
        /// Populate the drop down lists for editing the record
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        protected override void PopulateAdditionalEditViewModelAttributes<TViewModel>(TViewModel viewModel)
        {
            var model = viewModel as AppMenuItemMaintenanceEditViewModel;

            //
            //Get the list of all app menus
            //
            var getAllAppMenusRequest = new AppMenuItemMaintenanceGetAllAppMenusRequest()
            {
            };

            //Set the list of app menus that the app meny drop down list will be based off of.
            var getAllAppMenusResponse = _service.GetAllAppMenus(getAllAppMenusRequest);
            model.AppMenuList = getAllAppMenusResponse.AppMenus;

            //
            //Get the list of all app menu items except for the one we are currently editing.
            //
            var getAllAppMenuItemsRequest = new AppMenuItemMaintenanceGetAllAppMenuItemsRequest()
            {
                AppMenuItemIdExcludeList = new List<int>()
            };
            if (model != null && model.AppMenuItemId != null)
            {
                getAllAppMenuItemsRequest.AppMenuItemIdExcludeList.Add(model.AppMenuItemId);
            }

            //Set the list of app menu items that the parent menu item drop down list will be based off of.
            var getAllAppMenuItemsResponse = _service.GetAllAppMenuItems(getAllAppMenuItemsRequest);
            model.ParentAppMenuItemList = getAllAppMenuItemsResponse.AppMenuItems;
        }

        /// <summary>
        /// On Creation of a new record set the initial values
        /// </summary>
        /// <typeparam name="M">Model Type</typeparam>
        /// <param name="viewModel">View for new model</param>
        protected override void OnEditNewRecord<M>(M viewModel)
        {
            AppMenuItemMaintenanceEditViewModel model = viewModel as AppMenuItemMaintenanceEditViewModel;
            model.Active = true;
        }

        /// <summary>
        /// Add or Edit a Record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Edit(AppMenuItemMaintenanceEditViewModel model)
        {
            return this.Edit<AppMenuItemMaintenanceEditViewModel, int, AppMenuItemMaintenanceService, AppMenuItemMaintenanceSaveRequest, AppMenuItemMaintenanceSaveResponse, AppMenuItem>(model);
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
            AppMenuItemMaintenanceEditViewModel editModel = model as AppMenuItemMaintenanceEditViewModel;

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
            return this.Delete<int, AppMenuItemMaintenanceService, AppMenuItemMaintenanceDeleteRequest, AppMenuItemMaintenanceDeleteResponse, AppMenuItem>(ids);
        }
        #endregion

        #region Export Grid
        public virtual ActionResult Export(string exportConfig, AppMenuItemMaintenanceListViewModel model, [ModelBinder(typeof(JQueryDataTablesModelBinder))]JQueryDataTablesParameterModel dataTablesModel = null)
        {
            return ExportJQueryDataTable<AppMenuItemMaintenanceListViewModel, DomainExportSettings, AppMenuItemMaintenanceService, AppMenuItemMaintenanceListRequest, AppMenuItemMaintenanceListResponse, AppMenuItem>(exportConfig, model, dataTablesModel);
        }
        #endregion
    }
}
