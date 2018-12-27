using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Domain.Enums;
using Catalyst.MVC.Infrastructure.Models.JQueryDataTables;
using Catalyst.MVC.Infrastructure.Providers.Security;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityUserMaintenance;
using CStore.Domain.Domains.Admin.Models.ViewModels.SecurityUserMaintenance;
using CStore.Domain.Domains.Admin.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using CStore.Domain.Domains.Admin.Models.ViewModels.SecuritySecurableMaintenance;
using CStore.Domain.Entities;

namespace CStore.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller used for handling the user maintenance administration screens
    /// </summary>
    public partial class SecurityUserMaintenanceController : DomainEntityController
    {
        #region Internals

        private ISecurityUserMaintenanceService _service;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="securityProvider"></param>
        public SecurityUserMaintenanceController(ISecurityProvider securityProvider, ISecurityUserMaintenanceService service)
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
        public virtual ActionResult Index(SecurityUserMaintenanceListViewModel model)
        {
            if (model.SecurityRoleIds == null)
            {
                model.SecurityRoleIds = new List<int>();
            }
            return Index<SecurityUserMaintenanceListViewModel>(model);
        }

        /// <summary>
        /// List Implementation for the screen. This returns an AJAX response for the datatables object on the page.
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="dataTablesModel">Data Table Model</param>
        /// <returns></returns>
        public virtual ActionResult List(SecurityUserMaintenanceListViewModel model = null, [ModelBinder(typeof(JQueryDataTablesModelBinder))]JQueryDataTablesParameterModel dataTablesModel = null)
        {
            return JQueryDataTablesList<SecurityUserMaintenanceListViewModel, SecurityUserMaintenanceService, SecurityUserMaintenanceListRequest, SecurityUserMaintenanceListResponse, VWSecurityUser>(model, dataTablesModel);
        }
        #endregion

        #region Add or edit a record
        /// <summary>
        /// Add or Edit a Record
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Edit(int? id = null, SecurityUserMaintenanceEditViewModel model = null)
        {
            int itemId = id.HasValue ? id.Value : 0;
            return this.Edit<SecurityUserMaintenanceEditViewModel, int, SecurityUserMaintenanceService, SecurityUserMaintenanceGetRequest, SecurityUserMaintenanceGetResponse, SecurityUser>(itemId, model);
        }

        /// <summary>
        /// Populate the drop down lists for editing the user
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        protected override void PopulateAdditionalEditViewModelAttributes<TViewModel>(TViewModel viewModel)
        {
            var model = viewModel as SecurityUserMaintenanceEditViewModel;

            //Get the list of available roles and the ones assigned by default to a new user.
            var userRolesRequest = new SecurityUserMaintenanceGetRolesForUserRequest()
            {
                SecurityUserId = model.SecurityUserId
            };
            var userRolesResponse = _service.GetRolesForUser(userRolesRequest);
            model.AvailableRoles = userRolesResponse.AvailableRoles;
            model.AssignedRoles = userRolesResponse.AssignedRoles;

            //
            //And in the case where we submitted the form, but a validation error occurred causing 
            //the assigned roles to not be saved, we add those back from the available to assigned list.
            //
            if (model.AssignedRoleIds != null && model.AssignedRoleIds.Count > 0)
            {
                foreach (var availableRole in model.AvailableRoles.Where(p => model.AssignedRoleIds.Contains(p.SecurityRoleId)))
                {
                    model.AssignedRoles.Add(availableRole);
                }
                model.AvailableRoles = model.AvailableRoles.Except(model.AssignedRoles).ToList();
                model.AssignedRoles = model.AssignedRoles.OrderBy(p => p.Name).ToList();
            }
        }

        /// <summary>
        /// On Creation of a new record set the initial values
        /// </summary>
        /// <typeparam name="M">Model Type</typeparam>
        /// <param name="viewModel">View for new model</param>
        protected override void OnEditNewRecord<M>(M viewModel)
        {
            SecurityUserMaintenanceEditViewModel model = viewModel as SecurityUserMaintenanceEditViewModel;
            model.AuthenticationMethod = AuthenticationMethods.SecurityUser.ToString();
            model.AccountActivated = true;
            model.Active = true;


            var userRolesResponse = _service.GetRolesForUser(new SecurityUserMaintenanceGetRolesForUserRequest());
            model.AvailableRoles = userRolesResponse.AvailableRoles;
            model.AssignedRoles = userRolesResponse.AssignedRoles;

            //
            //Assign the default system roles to the new record.
            //
            if (model.AvailableRoles != null)
            {
                model.AvailableRoles = model.AvailableRoles.Except(model.AssignedRoles).ToList();
            }

            if (model.AssignedRoles != null)
            {
                model.AssignedRoles = model.AssignedRoles.OrderBy(p => p.Name).ToList();
                model.AssignedRoleIds = model.AssignedRoles.Select(p => p.SecurityRoleId).ToList();
            }
        }

        /// <summary>
        /// Add or Edit a Record (Save event, on HTTP Post)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Edit(SecurityUserMaintenanceEditViewModel model)
        {
            return this.Edit<SecurityUserMaintenanceEditViewModel, int, SecurityUserMaintenanceService, SecurityUserMaintenanceSaveRequest, SecurityUserMaintenanceSaveResponse, SecurityUser>(model);
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
            SecurityUserMaintenanceEditViewModel editModel = model as SecurityUserMaintenanceEditViewModel;
            editModel.NewPassword = null;
            editModel.NewPasswordConfirm = null;

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
            return this.Delete<int, SecurityUserMaintenanceService, SecurityUserMaintenanceDeleteRequest, SecurityUserMaintenanceDeleteResponse, SecurityUser>(ids);
        }
        #endregion

    }
}
