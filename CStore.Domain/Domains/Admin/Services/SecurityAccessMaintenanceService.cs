using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance;
using CStore.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CStore.Domain.Domains.Admin.Services
{
    /// <summary>
    /// Service for the security access maintenance screens
    /// </summary>
    public class SecurityAccessMaintenanceService : DomainService, ISecurityAccessMaintenanceService
    {
        #region Internals

        private IRepository _repository;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public SecurityAccessMaintenanceService(IRepository repository)
            : base()
        {
            _repository = repository;
        }
        #endregion

        #region Get the Securable Tree to display on the screen
        /// <summary>
        /// Get the Securable Tree to display on the screen
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SecurityAccessMaintenanceGetSecurableTreeResponse GetSecurableTree(
            SecurityAccessMaintenanceGetSecurableTreeRequest request)
        {
            //Get one level of securables
            var securableQueryable = _repository.GetAll<SecuritySecurable>(p => p.ChildSecuritySecurables);
            if (request != null && request.ParentSecuritySecurableId != null)
            {
                securableQueryable =
                    securableQueryable.Where(p => p.ParentSecuritySecurableId == request.ParentSecuritySecurableId);
            }
            else
            {
                securableQueryable =
                    securableQueryable.Where(p => p.ParentSecuritySecurableId == null);
            }

            //
            //Convert the linq query into the current tree model level
            //
            var securableTreeModel = securableQueryable.OrderBy(p => p.Name).Select(p => new SecuritySecurableTreeModel
            {
                id = p.SecuritySecurableId.ToString(),
                parent = (p.ParentSecuritySecurableId == null ? "#" : p.ParentSecuritySecurableId.ToString()),
                text = p.Name,
                state = new SecuritySecurableTreeStateModel()
                {
                    disabled = false,
                    opened = (p.ParentSecuritySecurableId == null ? true : false),
                    selected = false //(p.ParentSecuritySecurableId == null ? true : false)
                },
                children = p.ChildSecuritySecurables.Any()
            }).ToList();

            //
            //Select only the first parent node, in case we have multiple root securables
            //
            if (request != null && request.ParentSecuritySecurableId == null)
            {
                var firstNode = securableTreeModel.FirstOrDefault();
                if (firstNode != null)
                {
                    firstNode.state.selected = true;
                }
            }

            return new SecurityAccessMaintenanceGetSecurableTreeResponse()
            {
                IsSuccessful = true,
                SecuritySecurableTreeModel = securableTreeModel
            };
        }
        #endregion

        #region Get the effective permissions for all of the roles for a securable.
        /// <summary>
        /// Get the effective permissions for all of the roles for a securable.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SecurityAccessMaintenanceGetEffectivePermissionsForSecurableResponse GetEffectivePermissionsForSecurable(
            SecurityAccessMaintenanceGetEffectivePermissionsForSecurableRequest request)
        {
            //
            //Validation
            //
            if (request == null || request.SecuritySecurableId == null)
            {
                return new SecurityAccessMaintenanceGetEffectivePermissionsForSecurableResponse()
                {
                    IsSuccessful = true,
                    Message = "Unable to get the effective permissions for securable. The request object must contain a valid security securable ID."
                };
            }

            var securitySecurable =
                _repository.GetAll<SecuritySecurable>()
                           .FirstOrDefault(p => p.SecuritySecurableId == request.SecuritySecurableId);
            if (securitySecurable == null)
            {
                return new SecurityAccessMaintenanceGetEffectivePermissionsForSecurableResponse()
                {
                    IsSuccessful = true,
                    Message = String.Format("Unable to get the effective permissions for securable. The security securable record at ID {0} could not be found.", request.SecuritySecurableId)
                };
            }

            //
            //Get all of the actions available on this securable
            //
            var securitySecurableActions = _repository.GetAll<SecuritySecurableAction>(p => p.SecurityAction, p => p.SecuritySecurable).Where(p => p.SecuritySecurableId == request.SecuritySecurableId).OrderBy(p => p.SecurityAction.Name).ToList();

            //
            //Get all of the roles, and for each role determine its effective permissions on the securable
            //
            var effectivePermissionsForRoleList = new List<EffectivePermissionsForRoleModel>();
            var rolesQueryable = _repository.GetAll<SecurityRole>().Where(p => p.Active == true);
            //Limit the roles to only those selected by the form.
            if (request.SelectedRoleIds != null && request.SelectedRoleIds.Count > 0)
            {
                rolesQueryable = rolesQueryable.Where(p => request.SelectedRoleIds.Contains(p.SecurityRoleId));
            }
            var roles = rolesQueryable.OrderBy(p => p.Name).ToList();

            foreach (var role in roles)
            {
                //
                //For each action, determine the roles and their permission to do that action on the securable
                //
                var effectivePermissionsList = new List<EffectivePermissionModel>();
                foreach (var securitySecurableAction in securitySecurableActions)
                {
                    var securityResult = GetSecurityResult(role, securitySecurable, securitySecurableAction.SecurityAction);

                    //
                    //Determine if the security was inherited. If it came from a securable that is not
                    //equal to the securable we are checking permissions for, then it was inherited.
                    //
                    bool isInheritedSecurity = false;
                    SecuritySecurable securitySecurableInheritedFrom = null;
                    if (securityResult != null && securityResult.SecuritySecurable != null)
                    {
                        isInheritedSecurity = (securityResult.SecuritySecurable.SecuritySecurableId !=
                                               securitySecurable.SecuritySecurableId);
                        if (isInheritedSecurity)
                        {
                            securitySecurableInheritedFrom = securityResult.SecuritySecurable;
                        }
                    }

                    //
                    //Create the effective permission model for the securable describing the permissions the
                    //role has on the securable.
                    //
                    var effectivePermissionModel = new EffectivePermissionModel()
                    {
                        SecuritySecurableAction = securitySecurableAction,
                        Allowed = securityResult.Allowed,
                        Inherited = isInheritedSecurity,
                        SecuritySecurableInheritedFrom = securitySecurableInheritedFrom,
                        Message = securityResult.Message
                    };

                    effectivePermissionsList.Add(effectivePermissionModel);
                }

                //
                //Add the model to the list of effective permissions for the role/securable
                //
                var effectivePermissionsForRoleModel = new EffectivePermissionsForRoleModel()
                {
                    SecurityRole = role,
                    EffectivePermissions = effectivePermissionsList
                };
                effectivePermissionsForRoleList.Add(effectivePermissionsForRoleModel);
            }

            //
            //Return the effective permissions list
            //
            return new SecurityAccessMaintenanceGetEffectivePermissionsForSecurableResponse()
            {
                IsSuccessful = true,
                SecuritySecurable = securitySecurable,
                EffectivePermissionsForRoles = effectivePermissionsForRoleList
            };
        }
        #endregion


        #region Get Security Result
        /// <summary>
        /// Get Security Result base on Security Hierachy
        /// </summary>
        /// <param name="role">Role</param>
        /// <param name="securable">Securable</param>
        /// <param name="action">Action</param>
        /// <returns>Allowed</returns>
        private SecurityResultModel GetSecurityResult(SecurityRole role, SecuritySecurable securable, SecurityAction action)
        {
            if (securable == null || action == null || role == null)
            {
                return new SecurityResultModel()
                {
                    Allowed = false,
                    Message = "An invalid securable, action, or role was specified to check the security on."
                };
            }

            var accessItems = _repository.GetAll<SecurityAccess>().Where(a => a.SecurityActionId == action.SecurityActionId && a.SecuritySecurableId == securable.SecuritySecurableId && a.SecurityRoleId == role.SecurityRoleId).ToList();

            //
            //If there is not security setup directly on the current security access item, then check the parent securable
            //
            if (accessItems.Count == 0)
            {
                // get security for parent item
                if (securable.ParentSecuritySecurableId == null)
                {
                    // no security is setup. default to less accessible
                    return new SecurityResultModel()
                    {
                        SecuritySecurable = securable,
                        Allowed = false,
                        Message = String.Format("No security access record has been setup on securable {0} for action {1} and role {2}.", securable.Name, action.Name, role.Name)
                    };
                }

                var parent = _repository.GetAll<SecuritySecurable>().FirstOrDefault(s => s.SecuritySecurableId == securable.ParentSecuritySecurableId);
                var result = GetSecurityResult(role, parent, action);
                return result;
            }

            //
            //If the user has any access item that permits access, then return true
            //
            if (accessItems.Exists(a => a.Allowed == true))
            {

                return new SecurityResultModel()
                {
                    SecuritySecurable = securable,
                    Allowed = true,
                    Message = String.Format("Allow on securable {0} for action {1} and role {2}.", securable.Name, action.Name, role.Name)
                };
            }

            return new SecurityResultModel()
            {
                SecuritySecurable = securable,
                Allowed = false,
                Message = String.Format("Deny on securable {0} for action {1} and role {2}.", securable.Name, action.Name, role.Name)
            };
        }
        #endregion

        #region Method to get the roles that security permissions can be possibly assigned to
        /// <summary>
        /// Method to get the roles that security permissions can be possibly assigned to
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SecurityAccessMaintenanceGetRolesResponse GetRoles(SecurityAccessMaintenanceGetRolesRequest request)
        {
            var allRoles = _repository.GetAll<SecurityRole>()
                                      .Where(p => p.Active == true)
                                      .OrderBy(p => p.Name)
                                      .ToList();


            var selectedRoles = new List<SecurityRole>();
            if (request != null && request.SelectedRoleIds != null)
            {
                selectedRoles = allRoles.Where(p => request.SelectedRoleIds.Contains(p.SecurityRoleId))
                                      .OrderBy(p => p.Name)
                                      .ToList();
            }

            //
            //Get all the roles not yet selected
            //
            var availableRoles = allRoles.Except(selectedRoles).ToList();

            return new SecurityAccessMaintenanceGetRolesResponse()
            {
                AvailableRoles = availableRoles,
                SelectedRoles = selectedRoles,
                IsSuccessful = true
            };
        }
        #endregion

        #region Save the permissions
        /// <summary>
        /// Save the permissions submitted by the security access screen
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SecurityAccessMaintenanceSavePermissionsResponse SavePermissions(
            SecurityAccessMaintenanceSavePermissionsRequest request)
        {
            //
            //Validation
            //
            if (request == null || request.SecuritySecurableId == null)
            {
                return new SecurityAccessMaintenanceSavePermissionsResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid request or SecuritySecurableId was passed into SavePermissions."
                };
            }

            //
            //Get the security securable
            //
            var securitySecurable = _repository.GetAll<SecuritySecurable>()
                       .FirstOrDefault(p => p.SecuritySecurableId == request.SecuritySecurableId);

            //
            //Get the security action records for this securable
            //
            var securitySecurableActions = _repository.GetAll<SecuritySecurableAction>(p => p.SecurityAction).Where(p => p.SecuritySecurableId == request.SecuritySecurableId).ToList();

            //
            //Get the security access records for this securable
            //
            var securityAccesses = _repository.GetAll<SecurityAccess>().Where(p => p.SecuritySecurableId == request.SecuritySecurableId).ToList();

            //
            //Update the permissions
            //
            int numPermissionsUpdated = 0;
            foreach (var permissionToSave in request.PermissionsToSave)
            {
                //
                //Get the security securable action associated with the permission we are saving
                //
                var securitySecurableAction = securitySecurableActions.FirstOrDefault(p => p.SecuritySecurableActionId == permissionToSave.SecuritySecurableActionId);

                //
                //Get the security access record associated with this permission.
                //This may return null if the record doesnt exist.
                //
                var securityAccess =
                    securityAccesses.FirstOrDefault(
                        p => p.SecurityActionId == securitySecurableAction.SecurityAction.SecurityActionId
                             && p.SecurityRoleId == permissionToSave.SecurityRoleId
                             && p.SecuritySecurableId == request.SecuritySecurableId);

                //
                //Bad permission value specified, throw an error
                //
                if (String.IsNullOrWhiteSpace(permissionToSave.PermissionValue))
                {
                    return new SecurityAccessMaintenanceSavePermissionsResponse()
                    {
                        IsSuccessful = false,
                        Message = String.Format("An invalid permission value was specified to save. The value was empty.")
                    };
                }
                //
                //If this is an inherited permission, then delete the security access record if it exists.
                //
                else if (permissionToSave.PermissionValue.ToLower() == "inherited")
                {
                    if (securityAccess != null)
                    {
                        _repository.Delete(securityAccess);
                        numPermissionsUpdated++;
                    }
                }
                //
                //If the security access record exists and we are changing its value to allowed
                //
                else if (securityAccess != null && permissionToSave.PermissionValue.ToLower() == "allowed")
                {
                    //If it is currently NOT allowed we need to change the value
                    if (!securityAccess.Allowed)
                    {
                        securityAccess.Allowed = true;
                        numPermissionsUpdated++;
                    }
                }
                //
                //If the security access record exists and we are changing its value to not allowed
                //
                else if (securityAccess != null && permissionToSave.PermissionValue.ToLower() == "notallowed")
                {
                    //If it is currently allowed we need to change the value
                    if (securityAccess.Allowed)
                    {
                        securityAccess.Allowed = false;
                        numPermissionsUpdated++;
                    }
                }
                //
                //If the security access record doesnt exist then we create it
                //
                else if (securityAccess == null
                    && (permissionToSave.PermissionValue.ToLower() == "allowed" || permissionToSave.PermissionValue.ToLower() == "notallowed"))
                {
                    securityAccess = new SecurityAccess()
                    {
                        SecuritySecurableId = securitySecurable.SecuritySecurableId,
                        SecurityActionId = securitySecurableAction.SecurityActionId,
                        SecurityRoleId = permissionToSave.SecurityRoleId,
                        Allowed = (permissionToSave.PermissionValue.ToLower() == "allowed" ? true : false)
                    };
                    _repository.Add(securityAccess);
                    numPermissionsUpdated++;
                }
                //
                //If we got here then an error occurred
                //
                else
                {
                    return new SecurityAccessMaintenanceSavePermissionsResponse()
                    {
                        IsSuccessful = false,
                        Message = String.Format("An invalid permission value of {0} was specified to save.", permissionToSave.PermissionValue)
                    };
                }

            }

            _repository.Commit();

            //
            //Successful save
            //
            return new SecurityAccessMaintenanceSavePermissionsResponse()
            {
                IsSuccessful = true,
                NumPermissionsUpdated = numPermissionsUpdated,
                Message = String.Format("{0} permission(s) were updated.", numPermissionsUpdated)
            };
        }
        #endregion
    }
}
