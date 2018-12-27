using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using Catalyst.MVC.Infrastructure.Util.Extensions;
using CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityUserMaintenance;
using CStore.Domain.Domains.Authentication.Models.ServiceModels.Password;
using CStore.Domain.Domains.Authentication.Services;
using CStore.Domain.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using CStore.Domain.Entities;

namespace CStore.Domain.Domains.Admin.Services
{
    /// <summary>
    /// Service for the user maintenance screens
    /// </summary>
    public class SecurityUserMaintenanceService : DomainEntityService, ISecurityUserMaintenanceService
    {
        #region Internals
        /// <summary>
        /// The password service used to manipulate passwords on users
        /// </summary>
        private IPasswordService _passwordService;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public SecurityUserMaintenanceService(IRepository repository, ISendMailProvider sendMail, IPasswordService passwordService)
            : base(repository, sendMail)
        {
            _passwordService = passwordService;
        }
        #endregion

        #region List
        /// <summary>
        /// Apply List Filter - Applies the Filters from the Request to the Query
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="request">Request Type</param>
        /// <param name="query">Query</param>
        /// <returns>Updated Query</returns>
        public override IQueryable<T> ApplyListFilter<T>(BaseServiceListRequest request, IQueryable<T> query)
        {
            SecurityUserMaintenanceListRequest listRequest = request as SecurityUserMaintenanceListRequest;
            var listQuery = (IQueryable<VWSecurityUser>)query;

            // apply filters to the IQueryable
            if (!String.IsNullOrEmpty(listRequest.UserName))
            {
                listQuery = listQuery.Where(p => p.UserName.Contains(listRequest.UserName));
            }
            if (!String.IsNullOrEmpty(listRequest.LastName))
            {
                listQuery = listQuery.Where(p => p.LastName.Contains(listRequest.LastName));
            }
            if (!String.IsNullOrEmpty(listRequest.FirstName))
            {
                listQuery = listQuery.Where(p => p.FirstName.Contains(listRequest.FirstName));
            }
            if (listRequest.SecurityRoleIds != null && listRequest.SecurityRoleIds.Any())
            {
                listQuery = listQuery.Where(p => p.RoleMemberships.Any(s => listRequest.SecurityRoleIds.Contains(s.SecurityRoleId)));
            }
            if (listRequest.SystemAdmin != null && listRequest.SystemAdmin.Value)
            {
                listQuery = listQuery.Where(p => p.SystemAdmin == true);
            }
            if (listRequest.Active != null && listRequest.Active.Value)
            {
                listQuery = listQuery.Where(p => p.Active == true);
            }
            if (listRequest.LastLoginDateStart != null)
            {
                listQuery = listQuery.Where(p => p.LastLoginDate >= listRequest.LastLoginDateStart);
            }
            if (listRequest.LastLoginDateEnd != null)
            {
                //Add 1 day to the end date and subtract a millisecond so we get all users who logged in on the end date
                var endDate = listRequest.LastLoginDateEnd.Value.AddDays(1).AddMilliseconds(-1);
                listQuery = listQuery.Where(p => p.LastLoginDate <= endDate);
            }

            return (IQueryable<T>)listQuery;
        }

        /// <summary>
        /// On Validate List Request / Used for Custom Validation or Tweaking of Parameters
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <typeparam name="R">Request Type</typeparam>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>
        public override R OnValidateListRequest<T, R>(BaseServiceListRequest request)
        {
            SecurityUserMaintenanceListRequest listRequest = request as SecurityUserMaintenanceListRequest;

            if (string.IsNullOrEmpty(listRequest.OrderBy) == true)
            {
                listRequest.OrderBy = "UserName";
            }

            return null;
        }

        #endregion

        #region Post get, after the record is retrieved from a get request
        /// <summary>
        /// Post get, after the record is retrieved from a get request
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void PostGet<TEntity, TResponse, TId>(BaseServiceGetRequest<TId> request, TResponse response)
        {
            var getRequest = request as SecurityUserMaintenanceGetRequest;
            var getResponse = response as SecurityUserMaintenanceGetResponse;

            //
            //Get the roles available for and assigned to the user.
            //
            var rolesForUserRequest = new SecurityUserMaintenanceGetRolesForUserRequest()
            {
                SecurityUserId = getRequest.Id
            };
            var rolesForUserResponse = GetRolesForUser(rolesForUserRequest);
            getResponse.AvailableRoles = rolesForUserResponse.AvailableRoles;
            getResponse.AssignedRoles = rolesForUserResponse.AssignedRoles;

        }
        #endregion

        #region Save Validation
        /// <summary>
        /// Custom Save Validation Logic. If successful, then return null.
        /// </summary>
        /// <typeparam name="T">Type of Entity</typeparam>
        /// <typeparam name="R">Type of Response</typeparam>
        /// <typeparam name="I">Type of ID Column</typeparam>
        /// <param name="request">Request</param>
        /// <returns>Validation Response</returns>
        public override R OnValidateRecordToSave<T, R, I>(BaseServiceSaveRequest request)
        {
            var saveRequest = request as SecurityUserMaintenanceSaveRequest;

            //
            //Ensure the username is unique
            //
            var duplicateRecords = _repository.GetAll<SecurityUser>()
                       .Where(p => p.UserName == saveRequest.UserName && p.SecurityUserId != saveRequest.SecurityUserId)
                       .ToList();
            if (duplicateRecords.Any())
            {
                var response = Activator.CreateInstance<R>();
                response.IsSuccessful = false;
                response.Message = String.Format("Unable to save the record. The username of {0} already exists on another user.", saveRequest.UserName);
                return response;
            }

            return null;
        }
        #endregion

        #region On Save
        /// <summary>
        /// On Save - Happens before the record is saved
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <typeparam name="S">Request Type</typeparam>
        /// <typeparam name="R">Response Type</typeparam>
        /// <param name="request">Request</param>
        /// <param name="record">That that has been Mapped</param>
        public override R OnSave<T, R, S>(S request, T originalRecord, T record)
        {
            var saveRequest = request as SecurityUserMaintenanceSaveRequest;
            var securityUser = record as SecurityUser;

            //
            //Set the password expiration date accordingly
            //
            if (securityUser.PasswordNeverExpires || securityUser.UserCannotChangePassword)
            {
                securityUser.PasswordExpirationDate = null;
            }
            else if (!securityUser.PasswordNeverExpires && securityUser.PasswordExpirationDate == null && !String.IsNullOrWhiteSpace(saveRequest.NewPassword))
            {
                var passwordExpirationDateResponse =
                    _passwordService.CalculatePasswordExpirationDate(new CalculatePasswordExpirationDateRequest());
                //securityUser.PasswordExpirationDate = passwordExpirationDateResponse.PasswordExpirationDate;
            }

            //
            //Set other account flags accordingly
            //            
            if (!securityUser.AccountLocked)
            {
                securityUser.AccountLockedDate = null;
            }
            else if (securityUser.AccountLocked && securityUser.AccountLockedDate == null)
            {
                securityUser.AccountLockedDate = DateTime.Now;
            }

            //Simply return null to indicate success
            return null;
        }
        #endregion

        #region Post Save
        /// <summary>
        /// After the Record has been saved
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="request"></param>
        /// <param name="originalRecord"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public override bool PostSave<T, R, S>(S request, T originalRecord, T record, R response)
        {
            var saveRequest = request as SecurityUserMaintenanceSaveRequest;
            var saveResponse = response as SecurityUserMaintenanceSaveResponse;
            var securityUser = record as SecurityUser;

            //
            //Change the users password if a new password was submitted
            //
            if (!String.IsNullOrWhiteSpace(saveRequest.NewPassword) ||
                !String.IsNullOrWhiteSpace(saveRequest.NewPasswordConfirm))
            {
                var changePasswordRequest = new ChangePasswordRequest()
                {
                    UserName = securityUser.UserName,
                    AuthenticationMethod = securityUser.AuthenticationMethod,
                    SecurityUserId = securityUser.SecurityUserId,
                    NewPassword = saveRequest.NewPassword,
                    NewPasswordConfirm = saveRequest.NewPasswordConfirm,
                    CheckPasswordComplexity = false,
                    CheckIfUserPasswordCanBeChanged = false,
                    EnforcePasswordHistory = false,
                    SendPasswordSuccessfullyChangedEmail = false
                };

                var changePasswordResponse = _passwordService.ChangePassword(changePasswordRequest);

                if (!changePasswordResponse.IsSuccessful)
                {
                    response.IsSuccessful = false;
                    response.Message = "An error occurred setting the users password. " + changePasswordResponse.Message;
                }
            }

            //
            //Assign the roles to the user submitted in the save request
            //
            if (saveRequest.AssignedRoleIds != null)
            {
                var assignedRoles = _repository.GetAll<SecurityUserRoleMembership>(p => p.User, p => p.Role)
                                               .Where(p => p.SecurityUserId == securityUser.SecurityUserId && p.Role.Active == true)
                                               .ToList();
                //
                //Remove any roles currently assigned to the user that were not submitted in the list
                //of AssignedRoleIds (the list of roles the user should have)
                //
                foreach (var assignedRole in assignedRoles)
                {
                    if (!saveRequest.AssignedRoleIds.Contains(assignedRole.SecurityRoleId))
                    {
                        _repository.Delete(assignedRole);
                    }
                }

                //
                //Add any new roles to the user not yet assigned. The roles submitted in the list
                //of AssignedRoleIds are the current roles the user should have.
                //
                foreach (var assignedRoleId in saveRequest.AssignedRoleIds)
                {
                    if (assignedRoles.FirstOrDefault(p => p.SecurityRoleId == assignedRoleId) == null)
                    {
                        var securityUserRoleMembership = new SecurityUserRoleMembership()
                        {
                            SecurityRoleId = assignedRoleId,
                            SecurityUserId = securityUser.SecurityUserId,
                            Active = true
                        };
                        _repository.Add(securityUserRoleMembership);
                    }
                }
            }

            _repository.Commit();

            //
            //Get the roles available for and assigned to the user.
            //
            var rolesForUserRequest = new SecurityUserMaintenanceGetRolesForUserRequest()
            {
                SecurityUserId = securityUser.SecurityUserId
            };
            var rolesForUserResponse = GetRolesForUser(rolesForUserRequest);
            saveResponse.AvailableRoles = rolesForUserResponse.AvailableRoles;
            saveResponse.AssignedRoles = rolesForUserResponse.AssignedRoles;

            //Dont need the caller to commit after this processing is done
            return false;
        }

        #endregion

        #region Logic to get the available and assigned roles for the user

        /// <summary>
        /// Helper method used to get the list of available roles and list of assigned roles for a user.
        /// </summary>
        /// <param name="securityUserId"></param>
        /// <param name="availableRoles"></param>
        /// <param name="assignedRoles"></param>
        public SecurityUserMaintenanceGetRolesForUserResponse GetRolesForUser(SecurityUserMaintenanceGetRolesForUserRequest request)
        {
            var allRoles = _repository.GetAll<SecurityRole>()
                                      .Where(p => p.Active == true)
                                      .OrderBy(p => p.Name)
                                      .ToList();


            var assignedRoles = new List<SecurityRole>();
            if (request.SecurityUserId != null)
            {
                assignedRoles = _repository.GetAll<SecurityUserRoleMembership>(p => p.User, p => p.Role)
                                           .Where(
                                               p => p.SecurityUserId == request.SecurityUserId && p.Role.Active == true)
                                           .OrderBy(p => p.Role.Name)
                                           .Select(p => p.Role)
                                           .ToList();
            }
            //
            //Add any default roles to the user if it is a new user being created.
            //
            else if (request.SecurityUserId == null)
            {
                assignedRoles = _repository.GetAll<SecurityRole>()
                    .Where(p => p.Active == true && p.Default == true)
                    .OrderBy(p => p.Name)
                    .ToList();
            }

            //
            //Get all the roles not yet assigned to the user
            //
            var availableRoles = allRoles.Except(assignedRoles).ToList();

            return new SecurityUserMaintenanceGetRolesForUserResponse()
            {
                SecurityUserId = request.SecurityUserId,
                AvailableRoles = availableRoles,
                AssignedRoles = assignedRoles,
                IsSuccessful = true
            };
        }
        #endregion

        #region Validate if the record can be deleted
        /// <summary>
        /// Method to check if the record being deleted can be
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="request"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public override R OnValidateRecordToDelete<T, R, I>(BaseServiceDeleteRequest<I> request, T record)
        {
            var deleteRequest = request as SecurityUserMaintenanceDeleteRequest;
            var securityUser = record as SecurityUser;

            //Returning null means it is OK to be deleted.
            return null;
        }
        #endregion

        #region On Delete (Happens pre-delete)
        /// <summary>
        /// Before the record is deleted, do this functionality.
        /// Mainly used to delete other child objects to the one being deleted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="request"></param>
        /// <param name="record"></param>
        public override void OnRecordDelete<T, R, I>(BaseServiceDeleteRequest<I> request, T record)
        {
            var deleteRequest = request as SecurityUserMaintenanceDeleteRequest;
            var securityUser = record as SecurityUser;

            //Role Memberships
            var roleMemberships =
                _repository.GetAll<SecurityUserRoleMembership>()
                           .Where(p => p.SecurityUserId == securityUser.SecurityUserId).ToList();
            _repository.DeleteAll(roleMemberships);

            //SecurityUserQuestionAnswer
            var securityUserQuestionAnswers =
                _repository.GetAll<SecurityUserQuestionAnswer>()
                           .Where(p => p.SecurityUserId == securityUser.SecurityUserId).ToList();
            _repository.DeleteAll(securityUserQuestionAnswers);

            //Login History
            var loginHistory =
                _repository.GetAll<SecurityUserLoginHistory>()
                           .Where(p => p.SecurityUserId == securityUser.SecurityUserId).ToList();
            _repository.DeleteAll(loginHistory);

            //SecurityUserPasswordHistory
            var securityUserPasswordHistory =
                _repository.GetAll<SecurityUserPasswordHistory>()
                           .Where(p => p.SecurityUserId == securityUser.SecurityUserId).ToList();
            _repository.DeleteAll(securityUserPasswordHistory);

            //SecurityPasswordResetRequest
            var securityPasswordResetRequests =
                _repository.GetAll<SecurityPasswordResetRequest>()
                           .Where(p => p.SecurityUserId == securityUser.SecurityUserId).ToList();
            _repository.DeleteAll(securityPasswordResetRequests);

            //SecuritySingleSignOnToken
            var securitySingleSignOnTokens =
                _repository.GetAll<SecuritySingleSignOnToken>()
                           .Where(p => p.SecurityUserId == securityUser.SecurityUserId).ToList();
            _repository.DeleteAll(securitySingleSignOnTokens);

            //SecurityUserActivation
            var securityUserActivations =
                _repository.GetAll<SecurityUserActivation>()
                           .Where(p => p.SecurityUserId == securityUser.SecurityUserId).ToList();
            _repository.DeleteAll(securityUserActivations);

            _repository.Commit();
        }
        #endregion


    }
}
