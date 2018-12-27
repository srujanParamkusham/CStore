using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Domain.Enums;
using Catalyst.MVC.Domain.Providers.Authentication;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using Catalyst.MVC.Infrastructure.Services.Log;
using Catalyst.MVC.Infrastructure.Services.State;
using Catalyst.MVC.Infrastructure.Util.ActiveDirectory;
using Catalyst.MVC.Infrastructure.Util.Encryption;
using Catalyst.MVC.Infrastructure.Util.Token;
using CStore.Domain.Domains.Authentication.Models.ServiceModels.Password;
using CStore.Domain.Services;
using CStore.Domain.Services.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CStore.Domain.Domains.Authentication.Services
{
    /// <summary>
    /// Service for managing user password related functionality such as changing passwords, expired passwords, etc
    /// </summary>
    public class PasswordService : DomainService, IPasswordService
    {
        #region Constants

        private int PASSWORD_MIN_LENGTH = 8;

        #endregion

        #region Internals
        private readonly IRepository _repository;
        private readonly ISendMailProvider _sendMail;
        private readonly IAuthenticationContentProvider _authenticationContentProvider;
        private readonly ITokenResolver _tokenResolver;
        private readonly HttpContext _httpContext;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="membershipProvider"></param>
        public PasswordService(IRepository repository, ISendMailProvider sendMail, IAuthenticationContentProvider authenticationContentProvider, ITokenResolver tokenResolver)
        {
            _repository = repository;
            this._sendMail = sendMail;
            _authenticationContentProvider = authenticationContentProvider;
            _tokenResolver = tokenResolver;
            _httpContext = HttpContext.Current;
        }
        #endregion

        #region Determine If a Users Password Can Be Changed
        /// <summary>
        /// Determine if a user can change their password or not.
        /// </summary>
        /// <param name="request">The request object containing the ADUser or SecurityUser to check, </param>
        /// <returns>Response object where IsSuccessful is True if the password can be changed, false if it doesnt.</returns>
        public CanUserPasswordBeChangedResponse CanUserPasswordBeChanged(CanUserPasswordBeChangedRequest request)
        {
            //
            //If there are no users found, we cannot reset the password
            //
            if (request.ADUser == null && request.SecurityUser == null)
            {
                return new CanUserPasswordBeChangedResponse()
                {
                    IsSuccessful = false,
                    Message =
                        "Neither the SecurityUser or ADUser record was specified to determine if the password could be changed for it."
                };
            }

            //
            //We have only one user now, validate it can have a password reset
            //
            //Ensure AD allows it to be reset
            if (request.ADUser != null
                && (request.SecurityUser == null || request.AuthenticationMethod == AuthenticationMethods.ActiveDirectory.ToString()))
            {
                if (request.ADUser.UserCannotChangePassword
                    || request.ADUser.AccountDisabled
                    || request.ADUser.AccountLockedOut
                    || !ApplicationService.Instance.PasswordChangeAllowedForADUser)
                {
                    return new CanUserPasswordBeChangedResponse()
                    {
                        IsSuccessful = false,
                        Message = String.Format("Username {0} cannot have it's password reset.", request.ADUser.UserName)
                    };
                }
            }

            //And ensure SecurityUser allows it to be reset
            if (request.SecurityUser != null)
            {
                if (request.SecurityUser.AccountLocked
                    || !request.SecurityUser.Active
                    || request.SecurityUser.UserCannotChangePassword
                    || (request.SecurityUser.AccountExpirationDate != null && request.SecurityUser.AccountExpirationDate <= DateTime.Now)
                    || request.SecurityUser.UserName.Equals("Anonymous"))
                {
                    var errorDetails = "";
                    if (!request.SecurityUser.Active)
                    {
                        errorDetails = "The user is not active.";
                    }
                    else if (request.SecurityUser.UserCannotChangePassword)
                    {
                        errorDetails = "The user cannot change their password.";
                    }
                    else if (request.SecurityUser.AccountExpirationDate != null && request.SecurityUser.AccountExpirationDate <= DateTime.Now)
                    {
                        errorDetails = "The user's account is expired.";
                    }
                    else if (request.SecurityUser.UserName.Equals("Anonymous"))
                    {
                        errorDetails = "Passwords may not be set for the anonymous user.";
                    }

                    return new CanUserPasswordBeChangedResponse()
                    {
                        IsSuccessful = false,
                        Message = String.Format("Username {0} cannot have it's password changed. {1}", request.SecurityUser.UserName, errorDetails)
                    };
                }
            }

            //
            //If we get here then the users password can be changed
            //
            return new CanUserPasswordBeChangedResponse()
            {
                IsSuccessful = true,
                Message = null
            };
        }

        #endregion

        #region Change a users password
        /// <summary>
        /// Change a users password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ChangePasswordResponse ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                //
                //Validate the parameters
                //
                if (request == null || String.IsNullOrWhiteSpace(request.UserName))
                {
                    return new ChangePasswordResponse()
                    {
                        IsSuccessful = false,
                        Message = "An invalid password change request was made."
                    };
                }

                //
                //Validate the new passwords are equal
                //
                if (!request.NewPassword.Equals(request.NewPasswordConfirm))
                {
                    return new ChangePasswordResponse()
                    {
                        IsSuccessful = false,
                        Message = "The passwords you entered must match."
                    };
                }

                //
                //Validate the new passwords meets complexity requirements
                //
                if (request.CheckPasswordComplexity)
                {
                    var passwordComplexityRequest = new CheckPasswordComplexityRequest()
                    {
                        Password = request.NewPassword,
                        UserName = request.UserName
                    };
                    var passwordComplexityResponse = CheckPasswordComplexity(passwordComplexityRequest);
                    if (!passwordComplexityResponse.IsSuccessful)
                    {
                        return new ChangePasswordResponse()
                        {
                            IsSuccessful = false,
                            Message = passwordComplexityResponse.Message
                        };
                    }
                }

                //
                //Determine if this is for an AD user or a security user record
                //Get the SecurityUser Record if we have it
                //
                SecurityUser securityUser = null;
                ADUser adUser = null;
                if (request.SecurityUserId != null)
                {
                    securityUser =
                        _repository.GetAll<SecurityUser>()
                                   .FirstOrDefault(p => p.SecurityUserId == request.SecurityUserId);
                }

                //
                //Get the AD user if AD authentication is enabled, and we dont have a security user
                //or we dont have a security user active directory guid
                //
                if (DomainApplicationService.Instance.FormsAuthenticationADEnabled &&
                    (securityUser == null || securityUser.ActiveDirectoryGuid == null))
                {
                    adUser = new ADUser(request.UserName);
                    if (adUser == null || !adUser.ValidUser)
                    {
                        adUser = null;
                    }
                }
                else if (DomainApplicationService.Instance.FormsAuthenticationADEnabled &&
                         securityUser.ActiveDirectoryGuid != null)
                {
                    adUser = new ADUser(securityUser.ActiveDirectoryGuid.Value);
                    if (adUser == null || !adUser.ValidUser)
                    {
                        adUser = null;
                    }
                }

                //
                //Ensure we found a user account to reset a password for
                //
                if (securityUser == null && adUser == null)
                {
                    return new ChangePasswordResponse()
                    {
                        IsSuccessful = false,
                        Message = "A valid user could not be found to reset the password for."
                    };
                }

                //
                //Ensure the user account is active and not locked and its password can be reset
                //
                //
                if (request.CheckIfUserPasswordCanBeChanged)
                {
                    var canUserPasswordBeChangedRequest = new CanUserPasswordBeChangedRequest()
                    {
                        SecurityUser = securityUser,
                        ADUser = adUser,
                        AuthenticationMethod = request.AuthenticationMethod
                    };
                    var canUserPasswordBeChangedResponse = CanUserPasswordBeChanged(canUserPasswordBeChangedRequest);
                    if (!canUserPasswordBeChangedResponse.IsSuccessful)
                    {
                        return new ChangePasswordResponse()
                        {
                            IsSuccessful = false,
                            Message =
                                    String.Format(
                                        "{0} Please contact support to have the password changed.",
                                        canUserPasswordBeChangedResponse.Message)
                        };
                    }
                }

                DateTime? newPasswordExpirationDate = null;
                var passwordChanged = false;

                //
                //If AD user, connect to AD and reset the users password
                //Ensure the user is allowed to reset their password
                //
                if (adUser != null
                    &&
                    (securityUser == null ||
                     request.AuthenticationMethod == AuthenticationMethods.ActiveDirectory.ToString()))
                {
                    adUser.ChangePassword(request.NewPassword);
                    newPasswordExpirationDate = adUser.PasswordExpiryDate;
                    passwordChanged = true;
                }

                //
                //If Security User, then reset the users password there
                //Generate a new passwordhash, salt, and update the password last changed date as well as password expiration date
                //Store the users current password in the password history table if its a SecurityUser record.
                //
                else if (securityUser != null
                         &&
                         (request.AuthenticationMethod == null ||
                          request.AuthenticationMethod == AuthenticationMethods.SecurityUser.ToString()))
                {
                    //
                    //Ensure the users current password matches what they typed into the screen
                    //
                    if (request.CheckCurrentPassword)
                    {
                        if (securityUser.PasswordHash !=
                            SHA256Hash.HashValue(request.CurrentPassword, securityUser.PasswordSalt))
                        {
                            return new ChangePasswordResponse()
                            {
                                IsSuccessful = false,
                                Message = "An invalid current password was entered."
                            };
                        }
                    }

                    //
                    //Enforce password history, where a password cannot be one of the previously used passwords.
                    //
                    if (request.EnforcePasswordHistory)
                    {
                        //
                        //Ensure the new password is not the current password
                        //
                        if (securityUser.PasswordHash ==
                            SHA256Hash.HashValue(request.NewPassword, securityUser.PasswordSalt))
                        {
                            return new ChangePasswordResponse()
                            {
                                IsSuccessful = false,
                                Message = "The new password cannot be the same as the current password."
                            };
                        }

                        //
                        //Validate that the new password is not in the password history
                        //as a previously used password
                        //
                        var passwordHistoryRecordsToCheck =
                            ApplicationService.Instance.PasswordHistoryRecordsToCheckOnPasswordChange;
                        if (passwordHistoryRecordsToCheck > 0)
                        {
                            var passwordHistory =
                                _repository.GetAll<SecurityUserPasswordHistory>()
                                           .Where(p => p.SecurityUserId == securityUser.SecurityUserId)
                                           .OrderBy(p => p.CreateDate)
                                           .Take(passwordHistoryRecordsToCheck)
                                           .ToList();
                            foreach (var p in passwordHistory)
                            {
                                if (p.PasswordHash == SHA256Hash.HashValue(request.NewPassword, p.PasswordSalt))
                                {
                                    return new ChangePasswordResponse()
                                    {
                                        IsSuccessful = false,
                                        Message = "The new password cannot be one of the previously used passwords."
                                    };
                                }
                            }
                        }
                    }

                    //
                    //Create the password history entry for the users current password
                    //
                    var securityUserPasswordHistory = new SecurityUserPasswordHistory()
                    {
                        SecurityUserId = securityUser.SecurityUserId,
                        PasswordHash = securityUser.PasswordHash,
                        PasswordSalt = securityUser.PasswordSalt
                    };
                    _repository.Add(securityUserPasswordHistory);

                    //
                    //Update the security user record
                    //
                    securityUser.PasswordLastChangedDate = DateTime.Now;
                    securityUser.PasswordSalt = SHA256Hash.CreateSalt(6);
                    securityUser.PasswordHash = SHA256Hash.HashValue(request.NewPassword, securityUser.PasswordSalt);

                    if (securityUser.PasswordNeverExpires)
                    {
                        securityUser.PasswordExpirationDate = null;
                    }
                    else
                    {
                        var passwordExpirationDateResponse =
                            CalculatePasswordExpirationDate(new CalculatePasswordExpirationDateRequest());
                        securityUser.PasswordExpirationDate = passwordExpirationDateResponse.PasswordExpirationDate;
                    }
                    newPasswordExpirationDate = securityUser.PasswordExpirationDate;
                    passwordChanged = true;
                }

                //
                //If for some reason the password couldnt be changed
                //
                if (!passwordChanged)
                {
                    return new ChangePasswordResponse()
                    {
                        IsSuccessful = false,
                        Message =
                                "The password could not be changed. Please report this error. No AD User or SecurityUser record could be obtained to change the password for."
                    };
                }

                _repository.Commit();

                //
                //Repopulate the current users session values if needed
                //
                if (DomainSessionService.Instance != null && DomainSessionService.Instance.CurrentUser != null &&
                    request.UserName.Equals(DomainSessionService.Instance.CurrentUser.UserName))
                {
                    DomainSessionService.Instance.CurrentUser.PasswordExpirationDate = newPasswordExpirationDate;
                    DomainSessionService.Instance.CurrentUser.PasswordLastChangedDate = DateTime.Now;
                }

                //
                //Send the user an email to confirm that their password has been reset
                //
                if (request.SendPasswordSuccessfullyChangedEmail)
                {
                    var sendPasswordChangedEmailRequest = new SendPasswordChangedEmailRequest()
                    {
                        SecurityUser = securityUser,
                        ADUser = adUser
                    };
                    var sendPasswordChangedEmailResponse = SendPasswordChangedEmail(sendPasswordChangedEmailRequest);
                }

                //
                //Password change was successful if we got to here
                //
                return new ChangePasswordResponse()
                {
                    IsSuccessful = true,
                    Message = null,
                    PasswordExpirationDate = newPasswordExpirationDate
                };
            }
            catch (Exception e)
            {
                LogService.Instance.Log.Error("An unhandled exception occurred changing the password. ", e);
                return new ChangePasswordResponse()
                {
                    IsSuccessful = false,
                    Message = String.Format("An unhandled exception occurred changing the password. {0}", e.Message)
                };
            }
        }
        #endregion

        #region Check if a password is valid or not and meets Complexity Requirements
        /// <summary>
        /// Checks that a user submitted password conforms to any password rules including length,
        /// special characters, alphanumeric, etc as well as matching their confirmation entry.
        /// </summary>
        /// <param name="request">The request object containing the password to check, and optionally the username to ensure the password doesnt contain the username</param>
        /// <returns>Response object where IsSuccessful is True if the password meets complexity requirements, false if it doesnt.</returns>
        public CheckPasswordComplexityResponse CheckPasswordComplexity(CheckPasswordComplexityRequest request)
        {
            //
            //Validate the parameters
            //
            if (request == null)
            {
                return new CheckPasswordComplexityResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid request was made to check password complexity."
                };
            }

            //
            //Reject if the password has fewer than the minimum number of characters
            //
            if (String.IsNullOrEmpty(request.Password) || request.Password.Length < PASSWORD_MIN_LENGTH)
            {
                return new CheckPasswordComplexityResponse()
                {
                    IsSuccessful = false,
                    Message = String.Format("Password must be at least {0} characters long.", PASSWORD_MIN_LENGTH)
                };
            }

            //
            //Reject if the password contains the username
            //
            if (!String.IsNullOrEmpty(request.UserName) && request.Password.Contains(request.UserName))
            {
                return new CheckPasswordComplexityResponse()
                {
                    IsSuccessful = false,
                    Message = String.Format("Password cannot contain your username.")
                };
            }

            //
            //Password must contain a digit
            //
            if (!System.Text.RegularExpressions.Regex.IsMatch(request.Password, @"\d"))
            {
                return new CheckPasswordComplexityResponse()
                {
                    IsSuccessful = false,
                    Message = String.Format("Password must contain at least one number.")
                };
            }

            //
            //Password must contain a capital letter
            //
            if (!System.Text.RegularExpressions.Regex.IsMatch(request.Password, @"[A-Z]"))
            {
                return new CheckPasswordComplexityResponse()
                {
                    IsSuccessful = false,
                    Message = String.Format("Password must contain at least one capital letter.")
                };
            }

            //
            //Password is valid if we get here
            //
            return new CheckPasswordComplexityResponse()
            {
                IsSuccessful = true,
                Message = null
            };
        }
        #endregion

        #region Send Password Changed Email
        /// <summary>
        /// Send the user an email that their password has been changed.
        /// <param name="request"></param>
        /// <returns></returns>
        public SendPasswordChangedEmailResponse SendPasswordChangedEmail(SendPasswordChangedEmailRequest request)
        {
            //
            //Validate parameters
            //
            if (request == null || (request.ADUser == null && request.SecurityUser == null))
            {
                return new SendPasswordChangedEmailResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid request was specified to the SendPasswordChangedEmail service."
                };
            }

            //
            //Send the email
            //
            var adUser = request.ADUser;
            var securityUser = request.SecurityUser;

            try
            {
                _tokenResolver.GetTokenResolutionProvider().ClearTokens();
                var tokenValues = new Dictionary<String, String>();

                //
                //Setup the values for the Token Resolver so the email parses properly
                //Override the email address and other attributes since it may come from an AD User, 
                //we cant bank on this always being security user.
                //
                var firstName = (adUser != null ? adUser.FirstName : securityUser.FirstName);
                tokenValues.Add(TokenNames.FirstName.ToString(), firstName);
                var lastName = (adUser != null ? adUser.LastName : securityUser.LastName);
                tokenValues.Add(TokenNames.LastName.ToString(), lastName);
                var emailAddress = (adUser != null ? adUser.EmailAddress : securityUser.EmailAddress);
                tokenValues.Add(TokenNames.EmailAddress.ToString(), emailAddress);

                //
                //Process the email template and send it
                //
                var emailTemplate = _authenticationContentProvider.GetPasswordChangedEmail();
                _sendMail.MessageHtml = emailTemplate.Html;
                _sendMail.Subject = _tokenResolver.TokenizeString(emailTemplate.EmailSubject,
                                                                  TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                  (securityUser != null ? securityUser.SecurityUserId.ToString() : null),
                                                                  tokenValues);
                _sendMail.Body = _tokenResolver.TokenizeString(emailTemplate.EmailBody,
                                                               TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                               (securityUser != null ? securityUser.SecurityUserId.ToString() : null),
                                                               tokenValues);
                _sendMail.AddMessageTo(_tokenResolver.TokenizeString(emailTemplate.EmailTo,
                                                                     TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                     (securityUser != null ? securityUser.SecurityUserId.ToString() : null),
                                                                     tokenValues));
                _sendMail.AddMessageCC(_tokenResolver.TokenizeString(emailTemplate.EmailCC,
                                                                     TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                     (securityUser != null ? securityUser.SecurityUserId.ToString() : null),
                                                                     tokenValues));
                _sendMail.AddMessageBCC(_tokenResolver.TokenizeString(emailTemplate.EmailBCC,
                                                                      TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                      (securityUser != null ? securityUser.SecurityUserId.ToString() : null),
                                                                      tokenValues));
                var fromAddress = _tokenResolver.TokenizeString(emailTemplate.EmailFrom,
                                                                TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                (securityUser != null ? securityUser.SecurityUserId.ToString() : null),
                                                                tokenValues);
                var fromDisplayName = _tokenResolver.TokenizeString(emailTemplate.EmailFromDisplayName,
                                                                    TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                    (securityUser != null ? securityUser.SecurityUserId.ToString() : null),
                                                                    tokenValues);
                var from = new MailAddress(fromAddress, fromDisplayName);
                _sendMail.SetMessageFrom(from);
                _sendMail.Send();
            }
            catch (Exception e)
            {
                String errorMessage = String.Format("An error occurred sending the password changed email to user {0}",
                                                    (adUser != null ? adUser.UserName : securityUser.UserName));
                LogService.Instance.Log.Error(errorMessage, e);
                return new SendPasswordChangedEmailResponse()
                {
                    IsSuccessful = false,
                    Message = errorMessage
                };
            }

            //
            //Everything is good if we got hee
            //
            return new SendPasswordChangedEmailResponse()
            {
                IsSuccessful = true,
                Message = null
            };
        }
        #endregion

        #region Compute a password expiration date
        /// <summary>
        /// Calculates a password expiration date based on the system settings.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CalculatePasswordExpirationDateResponse CalculatePasswordExpirationDate(
            CalculatePasswordExpirationDateRequest request)
        {
            DateTime? passwordExpirationDate = null;
            if (ApplicationService.Instance.DaysBeforePasswordExpires > 0)
            {
                passwordExpirationDate =
                    DateTime.Now.AddDays(ApplicationService.Instance.DaysBeforePasswordExpires);
            }
            else
            {
                passwordExpirationDate = null;
            }

            return new CalculatePasswordExpirationDateResponse()
            {
                IsSuccessful = true,
                Message = null,
                PasswordExpirationDate = passwordExpirationDate
            };
        }
        #endregion

    }
}
