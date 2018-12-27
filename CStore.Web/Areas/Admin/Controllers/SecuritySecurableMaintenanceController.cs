using AutoMapper;
using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Models.JQueryDataTables;
using Newtonsoft.Json;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Admin.Models.ServiceModels.SecuritySecurableMaintenance;
using CStore.Domain.Domains.Admin.Models.ViewModels.SecuritySecurableMaintenance;
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
    /// Controller used for handling the security securable maintenance administration screens
    /// </summary>
    public partial class SecuritySecurableMaintenanceController : DomainEntityController
    {
        #region Member Variables
        private ISecuritySecurableMaintenanceService _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public SecuritySecurableMaintenanceController(ISecuritySecurableMaintenanceService service)
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
        public virtual ActionResult Index(SecuritySecurableMaintenanceListViewModel model)
        {
            return Index<SecuritySecurableMaintenanceListViewModel>(model);
        }

        /// <summary>
        /// List Implementation for the new screen. This returns an AJAX response for the datatables object on the page.
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="dataTablesModel">Data Table Model</param>
        /// <returns></returns>
        public virtual ActionResult List(SecuritySecurableMaintenanceListViewModel model = null, [ModelBinder(typeof(JQueryDataTablesModelBinder))]JQueryDataTablesParameterModel dataTablesModel = null)
        {
            var result = JQueryDataTablesList<SecuritySecurableMaintenanceListViewModel, SecuritySecurableMaintenanceService, SecuritySecurableMaintenanceListRequest, SecuritySecurableMaintenanceListResponse, SecuritySecurable>(model, dataTablesModel);
            return result;
        }

        /// <summary>
        /// Customize the datatables list response
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TEntityService"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        /// <param name="dataTablesModel"></param>
        /// <param name="service"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected override ActionResult OnJQueryDataTablesListResponse<TViewModel, TEntityService, TRequest, TResponse, TEntity>(TViewModel model,
            JQueryDataTablesParameterModel dataTablesModel, TEntityService service, TRequest request, TResponse response)
        {
            var serviceResponse = response as SecuritySecurableMaintenanceListResponse;

            //
            //Return a stripped down version of only the data needed for the table.
            //If we dont do this, the json object serializes the parent securable navigation property, which adds
            //a lot of additional json data to be returned.
            //
            var json = JsonConvert.SerializeObject(new
            {
                draw = (dataTablesModel != null ? dataTablesModel.Draw : 1),
                iTotalRecords = response.TotalRows,
                iTotalDisplayRecords = response.TotalRows,
                aaData = serviceResponse.Items.Select(p => new
                {
                    p.SecuritySecurableId,
                    p.Name,
                    ParentSecuritySecurable = new { Name = (p.ParentSecuritySecurable != null ? p.ParentSecuritySecurable.Name : String.Empty) }
                }).ToList()
            },
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            return Content(json, "application/json");
        }

        #endregion

        #region Add or edit a record
        /// <summary>
        /// Add or Edit a Record
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Edit(int? id = null, SecuritySecurableMaintenanceEditViewModel model = null)
        {
            int itemId = id.HasValue ? id.Value : 0;
            return this.Edit<SecuritySecurableMaintenanceEditViewModel, int, SecuritySecurableMaintenanceService, SecuritySecurableMaintenanceGetRequest, SecuritySecurableMaintenanceGetResponse, SecuritySecurable>(itemId, model);
        }

        /// <summary>
        /// Populate the drop down lists for editing the record
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        protected override void PopulateAdditionalEditViewModelAttributes<TViewModel>(TViewModel viewModel)
        {
            var model = viewModel as SecuritySecurableMaintenanceEditViewModel;

            //Get the list of all securables except for the one we are currently editing.
            var getAllSecurablesRequest = new SecuritySecurableMaintenanceGetAllSecurablesRequest()
            {
                SecuritySecurableIdExcludeList = new List<int>()
            };
            if (model != null && model.SecuritySecurableId != null)
            {
                getAllSecurablesRequest.SecuritySecurableIdExcludeList.Add(model.SecuritySecurableId);
            }

            //Set the list of securables that the parent securable drop down list will be based off of.
            var getAllSecurablesResponse = _service.GetAllSecurables(getAllSecurablesRequest);
            model.PossibleParentSecuritySecurables = getAllSecurablesResponse.SecuritySecurables;
        }

        /// <summary>
        /// On Creation of a new record set the initial values
        /// </summary>
        /// <typeparam name="M">Model Type</typeparam>
        /// <param name="viewModel">View for new model</param>
        protected override void OnEditNewRecord<M>(M viewModel)
        {
            SecuritySecurableMaintenanceEditViewModel model = viewModel as SecuritySecurableMaintenanceEditViewModel;
        }

        /// <summary>
        /// Add or Edit a Record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Edit(SecuritySecurableMaintenanceEditViewModel model)
        {
            return this.Edit<SecuritySecurableMaintenanceEditViewModel, int, SecuritySecurableMaintenanceService, SecuritySecurableMaintenanceSaveRequest, SecuritySecurableMaintenanceSaveResponse, SecuritySecurable>(model);
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
            SecuritySecurableMaintenanceEditViewModel editModel = model as SecuritySecurableMaintenanceEditViewModel;

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
            return this.Delete<int, SecuritySecurableMaintenanceService, SecuritySecurableMaintenanceDeleteRequest, SecuritySecurableMaintenanceDeleteResponse, SecuritySecurable>(ids);
        }
        #endregion
    }
}
