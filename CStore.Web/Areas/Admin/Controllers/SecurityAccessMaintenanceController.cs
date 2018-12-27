using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Domain.Enums;
using Catalyst.MVC.Infrastructure.Enums;
using Catalyst.MVC.Infrastructure.Models;
using Catalyst.MVC.Infrastructure.Models.JQueryDataTables;
using Catalyst.MVC.Infrastructure.Providers.Security;
using Newtonsoft.Json;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance;
using CStore.Domain.Domains.Admin.Models.ViewModels.SecurityAccessMaintenance;
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
    /// Controller used for handling the security access maintenance administration screens
    /// </summary>
    public partial class SecurityAccessMaintenanceController : DomainController
    {
        #region Internals

        private ISecurityAccessMaintenanceService _service;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="securityProvider"></param>
        public SecurityAccessMaintenanceController(ISecurityProvider securityProvider, ISecurityAccessMaintenanceService service)
            : base()
        {
            _service = service;
        }
        #endregion

        #region Index Screen
        /// <summary>
        /// Display the Base View for the maintenance screen.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Displays the initial view of the page</returns>
        public virtual ActionResult Index(SecurityAccessMaintenanceIndexViewModel model)
        {
            return View();
        }
        #endregion


        #region Get the nodes for the tree
        /// <summary>
        /// Get a level of the securable tree. If parent securable id is not passed in,
        /// the root of the tree is obtained. Otherwise, the children of the parent id will be 
        /// returned.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Displays the initial view of the page</returns>
        public virtual ActionResult GetSecurableTree(int? parentSecuritySecurableId)
        {
            var request = new SecurityAccessMaintenanceGetSecurableTreeRequest()
            {
                ParentSecuritySecurableId = parentSecuritySecurableId
            };
            var response = _service.GetSecurableTree(request);
            //Use Newtonsoft to to avoid error: "A circular reference was detected while serializing an object of type..."
            var json = JsonConvert.SerializeObject(response.SecuritySecurableTreeModel,
                                                   Formatting.None,
                                                   new JsonSerializerSettings()
                                                   {
                                                       ReferenceLoopHandling =
                                                           Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                                   });
            return Content(json, "application/json");
        }
        #endregion

        #region Get the effective permissions for a node
        /// <summary>
        /// Get the effective permissions for a securable node
        /// </summary>
        /// <param name="securitySecurableId">The security securable id to get the data for</param>
        /// <returns>Displays the initial view of the page</returns>
        [HttpGet]
        public virtual ActionResult GetEffectivePermissionsForSecurable(int? securitySecurableId, List<int> selectedRoleIds = null)
        {
            //
            //Create empty view model
            //
            var model = new GetEffectivePermissionsForSecurableViewModel()
            {
                SecuritySecurableId = securitySecurableId,
                SelectedRoleIds = selectedRoleIds
            };

            //
            //Populate the role selection lists
            //
            PopulateDropDownLists(model);

            //
            //Call the service to get the effective permissions for the selected roles
            //
            var request = new SecurityAccessMaintenanceGetEffectivePermissionsForSecurableRequest()
            {
                SecuritySecurableId = securitySecurableId,
                SelectedRoleIds = model.SelectedRoleIds
            };

            var response = _service.GetEffectivePermissionsForSecurable(request);
            if (response.IsSuccessful)
            {
                model.EffectivePermissionsForSecurable = response;
                model.IsSuccessful = true;
            }
            else
            {
                model.Message = response.Message;
                model.IsSuccessful = false;
            }
            return PartialView("_EffectivePermissionsPartial", model);
        }
        #endregion

        #region Get the effective permissions for a node

        /// <summary>
        /// Get the effective permissions for a securable node
        /// </summary>
        /// <param name="securitySecurableId">The security securable id to get the data for</param>
        /// <returns>Displays the initial view of the page</returns>
        [HttpPost]
        public virtual ActionResult GetEffectivePermissionsForSecurable(GetEffectivePermissionsForSecurableViewModel model)
        {
            //
            //Populate the role selection lists
            //
            PopulateDropDownLists(model);

            //
            //Save any permission changes that came in
            //
            if (model.SubmitAction == SubmitActions.Save.ToString()
                && model.SecurityAccessSettings != null
                && model.SecurityAccessSettings.Count > 0)
            {
                var permissionsToSave = new List<PermissionToSave>();
                //
                //The permissions get submitted in a dictionary.
                //The key is security securable action ID + "~" + security role Id
                //The value is Allowed, NotAllowed, or Inherited
                //
                foreach (var key in model.SecurityAccessSettings.Keys)
                {
                    //
                    //Break the key into its components: securitySecurableActionId and roleId
                    //Create the record of the permission to save for the service layer
                    //
                    var value = model.SecurityAccessSettings[key];
                    var keyElements = key.Split('~');
                    if (keyElements != null && keyElements.Count() == 2)
                    {
                        var securitySecurableActionId = int.Parse(keyElements[0]);
                        var roleId = int.Parse(keyElements[1]);

                        var permissionToSave = new PermissionToSave()
                        {
                            SecurityRoleId = roleId,
                            SecuritySecurableActionId = securitySecurableActionId,
                            PermissionValue = value
                        };
                        permissionsToSave.Add(permissionToSave);
                    }
                }

                //
                //Call the service layer to persist the permissions
                //
                var saveRequest = new SecurityAccessMaintenanceSavePermissionsRequest()
                {
                    SecuritySecurableId = model.SecuritySecurableId,
                    PermissionsToSave = permissionsToSave
                };

                var saveResponse = _service.SavePermissions(saveRequest);
                if (!saveResponse.IsSuccessful)
                {
                    ModelState.AddModelError("", saveResponse.Message);
                }
                else if (saveResponse.NumPermissionsUpdated > 0)
                {
                    AddToastMessage(new ToastMessage()
                    {
                        Message = saveResponse.Message,
                        Type = ToastMessage.ToastType.Success,
                        AutoHide = true,
                        Position = ToastMessage.ToastPosition.TopCenter
                    });
                }
            }

            //
            //Call the service to get the effective permissions for the selected roles
            //
            var request = new SecurityAccessMaintenanceGetEffectivePermissionsForSecurableRequest()
            {
                SecuritySecurableId = model.SecuritySecurableId,
                SelectedRoleIds = model.SelectedRoleIds
            };

            var response = _service.GetEffectivePermissionsForSecurable(request);
            if (response.IsSuccessful)
            {
                model.EffectivePermissionsForSecurable = response;
                model.IsSuccessful = true;
            }
            else
            {
                model.Message = response.Message;
                model.IsSuccessful = false;
            }
            return PartialView("_EffectivePermissionsPartial", model);
        }
        #endregion

        #region Populate Role Drop Down Lists
        /// <summary>
        /// Populate the role selection drop down lists for picking the roles to view permissions for
        /// </summary>
        /// <param name="model"></param>
        protected void PopulateDropDownLists(GetEffectivePermissionsForSecurableViewModel model)
        {
            var request = new SecurityAccessMaintenanceGetRolesRequest()
            {
                SelectedRoleIds = model.SelectedRoleIds
            };
            var getRolesResponse = _service.GetRoles(request);
            model.AvailableRoles = getRolesResponse.AvailableRoles;
            model.SelectedRoles = getRolesResponse.SelectedRoles;
        }
        #endregion

    }
}
