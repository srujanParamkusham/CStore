using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Newtonsoft.Json;
using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Models.JQueryDataTables;
using CStore.Domain.Domains.ScrumBan.Models.ServiceModels.WebsiteDataEntity;
using CStore.Domain.Domains.ScrumBan.Models.ViewModels.WebsiteData;
using CStore.Domain.Domains.ScrumBan.Services;
using CStore.Domain.Entities;


namespace CStore.Web.Areas.ScrumBan.Controllers
{
    /// <summary>
    /// Controller used for handling the application for WebsiteData
    /// </summary>
    public partial class WebsiteDataController : CStore.Domain.Controllers.DomainEntityController
    {
        #region Member Variables
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteDataController()
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
        public virtual ActionResult Index(WebsiteDataListViewModel model)
        {
            return Index<WebsiteDataListViewModel>(model);
        }

        /// <summary>
        /// List Implementation for the new screen. This returns an AJAX response for the datatables object on the page.
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="dataTablesModel">Data Table Model</param>
        /// <returns></returns>
        public virtual ActionResult List(WebsiteDataListViewModel model = null, [ModelBinder(typeof(JQueryDataTablesModelBinder))]JQueryDataTablesParameterModel dataTablesModel = null)
        {
            return JQueryDataTablesList<WebsiteDataListViewModel, WebsiteDataEntityService, WebsiteDataEntityPagedListRequest, WebsiteDataEntityPagedListResponse, WebsiteData>(model, dataTablesModel);
        }
        #endregion
     
       
        public virtual ActionResult GetWebsiteData(string id = null, WebsiteDataEditViewModel model = null)
        {
            //string itemId = id.HasValue ? id.Value : 0;
            string itemId = id;
            this.Edit<WebsiteDataEditViewModel, string, WebsiteDataEntityService, WebsiteDataEntityGetRequest, WebsiteDataEntityGetResponse, WebsiteData>(itemId, model);

            //
            // Return response as JSON
            //
            var json = JsonConvert.SerializeObject(model,
                                       Formatting.None,
                                       new JsonSerializerSettings()
                                       {
                                           ReferenceLoopHandling =
                                                   Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                       });

            return Content(json, "application/json");

           // return View("GetWebSiteData", model);
        }



        #region Add or edit a record
        /// <summary>
        /// Add or Edit a Record
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Edit(string id = null, WebsiteDataEditViewModel model = null)
        {
            //string itemId = id.HasValue ? id.Value : 0;
            string itemId = id;
            return this.Edit<WebsiteDataEditViewModel, string, WebsiteDataEntityService, WebsiteDataEntityGetRequest, WebsiteDataEntityGetResponse, WebsiteData>(itemId, model);
        }

        /// <summary>
        /// On Edit New Record, supports the ability to define the defaults for the a new record
        /// </summary>
        /// <typeparam name="TModel">Model</typeparam>
        /// <param name="model">View Model Instance</param>
        protected override void OnEditNewRecord<TViewModel>(TViewModel model)
        {
            WebsiteDataEditViewModel editModel = model as WebsiteDataEditViewModel;
        }

        /// <summary>
        /// Add or Edit a Record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Edit(WebsiteDataEditViewModel model)
        {
            return this.Edit<WebsiteDataEditViewModel, string, WebsiteDataEntityService, WebsiteDataEntitySaveRequest, WebsiteDataEntitySaveResponse, WebsiteData>(model);
        }

        /// <summary>
        /// Allows you update the mapping of the model after the record has been saved and converted to the edit view model
        /// </summary>
        /// <typeparam name="M">Model</typeparam>
        /// <typeparam name="E">Entity</typeparam>
        /// <param name="model">Model</param>
        /// <param name="entity">Entity</param>
        protected override ActionResult OnEditSavedMapping<TViewModel, TRequest, TResponse, TEntity>(TViewModel model, TEntity entity, TRequest request, TResponse response)
        {
            WebsiteDataEditViewModel editModel = model as WebsiteDataEditViewModel;

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
        public virtual ActionResult Delete(IEnumerable<string> ids)
        {
            return this.Delete<string, WebsiteDataEntityService, WebsiteDataEntityDeleteRequest, WebsiteDataEntityDeleteResponse, WebsiteData>(ids);
        }
        #endregion

        #region Populate Model Attributes

        /// <summary>
        /// Allows the list view model to be populated with drop down lists and other data to support the
        /// search criteria
        /// </summary>
        /// <typeparam name="TViewModel">Model</typeparam>
        /// <param name="model">Model</param>
        protected override void PopulateAdditionalListViewModelAttributes<TViewModel>(TViewModel model)
        {
            base.PopulateAdditionalListViewModelAttributes<TViewModel>(model);
            WebsiteDataListViewModel listModel = model as WebsiteDataListViewModel;
        }

        /// <summary>
        /// Allows the view model to be populated with drop down lists and other data to support the
        /// editing of the entity.
        /// </summary>
        /// <typeparam name="TViewModel">Model</typeparam>
        /// <param name="model">View</param>
        protected override void PopulateAdditionalEditViewModelAttributes<TViewModel>(TViewModel model)
        {
            base.PopulateAdditionalEditViewModelAttributes<TViewModel>(model);
            WebsiteDataEditViewModel editModel = model as WebsiteDataEditViewModel;
        }

        #endregion

    }
}