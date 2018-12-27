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
using CStore.Domain.Domains.Authentication.Models.ServiceModels.ForgotPassword;
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
    /// Service for managing user forgot password requests and resetting the passwords. Relies on the PasswordService 
    /// itself.
    /// </summary>
    public class ForgotPasswordService : DomainService, IForgotPasswordService
    {
        #region Internals
        private readonly IRepository _repository;
        private readonly ISendMailProvider _sendMail;
        private readonly IAuthenticationContentProvider _authenticationContentProvider;
        private readonly ITokenResolver _tokenResolver;
        private readonly IPasswordService _passwordService;
        private readonly HttpContext _httpContext;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="membershipProvider"></param>
        public ForgotPasswordService(IRepository repository, ISendMailProvider sendMail, IAuthenticationContentProvider authenticationContentProvider, ITokenResolver tokenResolver, IPasswordService passwordService)
        {
            _repository = repository;
            _sendMail = sendMail;
            _authenticationContentProvider = authenticationContentProvider;
            _tokenResolver = tokenResolver;
            _passwordService = passwordService;
            _httpContext = HttpContext.Current;
        }
        #endregion

        #region Send Instructions To Reset Password
        /// <summary>
        /// Process the request for a forgotten password from the main forgot password screen. This will lookup the 
        /// user and if found email them a link to reset their password.
        /// </summary>
        /// <param name="model"></param>
        public SendInstructionsToResetPasswordResponse SendInstructionsToResetPassword(SendInstructionsToResetPasswordRequest request)
        {
            //
            //Validate the parameters
            //
            if (request == null || String.IsNullOrWhiteSpace(request.UserNameOrEmail))
            {
                return new SendInstructionsToResetPasswordResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid user name or email address was specified."
                };
            }

            SecurityUser securityUser = null;
            ADUser adUser = null;

            //
            //See if we can reset this users active directory account. We only need to do this
            //if active directory auth is enabled, if AD users are allowed to reset their password,
            //and we are not storing the users in the SecurityUser table.
            //
            if (ApplicationService.Instance.FormsAuthenticationADEnabled
                && ApplicationService.Instance.ForgotPasswordResetAllowedForADUser
                && adUser == null && securityUser == null
                )
            {
                //
                //Try to find by username first
                //
                adUser = new ADUser(request.UserNameOrEmail);

                //
                //If not found, try to find by email address
                //
                if (!adUser.ValidUser)
                {
                    var searchFilter = adUser.GetUserSearchFilterByEmailAddress(request.UserNameOrEmail);
                    adUser.PopulateUserInformation(searchFilter);
                }

                //Ensure we clear the record if we dont have a valid AD user
                if (!adUser.ValidUser)
                {
                    adUser = null;
                }
                //
                //Get the security user record if we have security user records for AD users
                //
                else
                {
                    if (ApplicationService.Instance.FormsAuthenticationCreateSecurityUserRecordOnADAuth)
                    {
                        securityUser =
                            _repository.GetAll<SecurityUser>()
                                       .FirstOrDefault(p => p.ActiveDirectoryGuid == adUser.ObjectGuid);
                    }
                }
            }

            //
            //Then try to find the user by their SecurityUser table record
            //
            if (ApplicationService.Instance.FormsAuthenticationSecurityUserTableEnabled && adUser == null && securityUser == null)
            {
                //
                //First try lookup only by userid
                //Check the security user table first
                //
                var securityUsers =
                    _repository.GetAll<SecurityUser>().Where(p => p.UserName.Equals(request.UserNameOrEmail));

                //If no match found, try lookup by email address
                if (!securityUsers.Any())
                {
                    securityUsers = _repository.GetAll<SecurityUser>()
                                               .Where(p => p.EmailAddress.Equals(request.UserNameOrEmail));
                }

                //
                //If there is more than one result, we cannot reset the password
                //
                if (securityUsers.Count() > 1)
                {
                    return new SendInstructionsToResetPasswordResponse()
                    {
                        IsSuccessful = false,
                        Message =
                                "Multiple records were found with the supplied username or email, your password cannot be reset. Please contact support to have your password reset."
                    };
                }

                securityUser = securityUsers.FirstOrDefault();
            }

            //
            //Now ensure we can continue with the password reset
            //
            //
            var canUserPasswordBeChangedRequest = new CanUserPasswordBeChangedRequest()
            {
                SecurityUser = securityUser,
                ADUser = adUser
            };
            var canUserPasswordBeChangedResponse = _passwordService.CanUserPasswordBeChanged(canUserPasswordBeChangedRequest);
            if (!canUserPasswordBeChangedResponse.IsSuccessful)
            {
                return new SendInstructionsToResetPasswordResponse()
                {
                    IsSuccessful = false,
                    Message = "That username or email address cannot have it's password reset. Please contact support to have your password reset."
                };
            }

            //
            //Ensure the user has an email address
            //
            if ((securityUser == null || String.IsNullOrWhiteSpace(securityUser.EmailAddress))
                && (adUser == null || String.IsNullOrWhiteSpace(adUser.EmailAddress)))
            {
                return new SendInstructionsToResetPasswordResponse()
                {
                    IsSuccessful = false,
                    Message = "That username or email address cannot have it's password reset as it has no email on file. Please contact support to have your password reset."
                };
            }

            //
            //Create the password reset entry
            //
            var passwordResetRequest = CreateSecurityPasswordResetRequestEntry(request, securityUser, adUser);

            //
            //Send the password reset email
            //
            var sendForgotPasswordEmailRequest = new SendForgotPasswordEmailRequest()
            {
                SecurityUser = securityUser,
                ADUser = adUser,
                SecurityPasswordResetRequest = passwordResetRequest
            };
            var sendForgotPasswordEmailResponse = SendForgotPasswordEmail(sendForgotPasswordEmailRequest);
            if (!sendForgotPasswordEmailResponse.IsSuccessful)
            {
                return new SendInstructionsToResetPasswordResponse()
                {
                    IsSuccessful = false,
                    Message = sendForgotPasswordEmailResponse.Message
                };
            }

            //
            //Request was successful
            //
            return new SendInstructionsToResetPasswordResponse()
            {
                IsSuccessful = true,
                Message = null
            };
        }
        #endregion

        #region Create the Password Reset Entry
        /// <summary>
        /// Creates a Password Reset History object recording this reset attempt
        /// </summary>
        /// <returns></returns>
        private SecurityPasswordResetRequest CreateSecurityPasswordResetRequestEntry(SendInstructionsToResetPasswordRequest request, SecurityUser securityUser, ADUser adUser)
        {
            string token = Guid.NewGuid().ToString().ToUpper();

            var passwordResetRequest = new SecurityPasswordResetRequest()
            {
                SecurityUserId = (securityUser == null ? (int?)null : securityUser.SecurityUserId),
                UserName = (securityUser == null ? adUser.UserName : securityUser.UserName),
                Token = token,
                EmailAddress = ((securityUser == null || String.IsNullOrWhiteSpace(securityUser.EmailAddress))
                                    ? adUser.EmailAddress
                                    : securityUser.EmailAddress),
                IPAddress = request.IPAddress,
                RequestDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddHours(24),
                Processed = false,
                ProcessDate = null
            };

            _repository.Add<SecurityPasswordResetRequest>(passwordResetRequest);
            _repository.Commit();

            return passwordResetRequest;
        }
        #endregion


        #region Validate a SecurityPasswordResetRequest

        /// <summary>
        /// Validate a security password reset request, ensuring the ID and token can be decrypted and 
        /// has not expired
        /// </summary>
        /// <param name="model"></param>
        public ValidateSecurityPasswordResetTokenResponse ValidateSecurityPasswordResetToken(
            ValidateSecurityPasswordResetTokenRequest request)
        {
            //
            //Validate the parameters
            //
            if (request == null || String.IsNullOrWhiteSpace(request.Id) || String.IsNullOrWhiteSpace(request.Token))
            {
                return new ValidateSecurityPasswordResetTokenResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid password reset request was made."
                };
            }

            //
            //Decrypt the ID and token to verify they are valid
            //
            int passwordResetId;
            String passwordResetToken = null;
            try
            {
                String passwordResetIdString = AESEncryption.Decrypt(request.Id);
                passwordResetToken = AESEncryption.Decrypt(request.Token);
                passwordResetId = Convert.ToInt32(passwordResetIdString);
            }
            catch (Exception e)
            {
                LogService.Instance.Log.Debug("Exception logged for debug purposes only. This was meant to be caught and handled.", e);
                return new ValidateSecurityPasswordResetTokenResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid password reset request was made."
                };
            }

            //
            //If valid, get the SecurityPasswordResetRequest object from the table
            //
            var securityPasswordResetRequest = _repository.GetAll<SecurityPasswordResetRequest>().Where(p => p.SecurityPasswordResetRequestId == passwordResetId && p.Token == passwordResetToken).FirstOrDefault();
            if (securityPasswordResetRequest == null)
            {
                return new ValidateSecurityPasswordResetTokenResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid password reset request was made."
                };
            }

            //
            //Ensure the password reset request hasnt expired and hasnt been processed already
            //
            if (securityPasswordResetRequest.ExpirationDate == null || securityPasswordResetRequest.ExpirationDate < DateTime.Now || securityPasswordResetRequest.Processed)
            {
                return new ValidateSecurityPasswordResetTokenResponse()
                {
                    IsSuccessful = false,
                    Message = "The password reset request has expired."
                };
            }

            //
            //If we got here then all is good
            //
            return new ValidateSecurityPasswordResetTokenResponse()
            {
                IsSuccessful = true,
                Message = null,
                SecurityPasswordResetRequest = securityPasswordResetRequest
            };

        }

        #endregion

        #region Reset Forgotten Password
        /// <summary>
        /// For a forgotten password request that has been emailed to the user, allow them to
        /// reset their password.
        /// </summary>
        /// <param name="model"></param>
        public ResetForgottenPasswordResponse ResetForgottenPassword(
            ResetForgottenPasswordRequest request)
        {
            //
            //Validate the parameters
            //
            if (request == null || String.IsNullOrWhiteSpace(request.Id) || String.IsNullOrWhiteSpace(request.Token))
            {
                return new ResetForgottenPasswordResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid password reset request was made."
                };
            }

            //
            //Ensure the id and token is valid
            //
            var validationRequest = new ValidateSecurityPasswordResetTokenRequest()
            {
                Id = request.Id,
                Token = request.Token
            };
            var validationResponse = this.ValidateSecurityPasswordResetToken(validationRequest);
            if (!validationResponse.IsSuccessful)
            {
                return new ResetForgottenPasswordResponse()
                {
                    IsSuccessful = false,
                    Message = validationResponse.Message
                };
            }
            var securityPasswordResetRequest = validationResponse.SecurityPasswordResetRequest;

            //
            //Get the SecurityUser record if it exists
            //
            SecurityUser securityUser = null;
            if (securityPasswordResetRequest.SecurityUserId != null)
            {
                securityUser =
                    _repository.GetAll<SecurityUser>()
                               .FirstOrDefault(p => p.SecurityUserId == securityPasswordResetRequest.SecurityUserId);
            }

            //
            //Change the password
            //
            var changePasswordRequest = new ChangePasswordRequest()
            {
                UserName = securityPasswordResetRequest.UserName,
                AuthenticationMethod = (securityUser != null ? securityUser.AuthenticationMethod : null),
                SecurityUserId = (securityUser != null ? securityUser.SecurityUserId : (int?)null),
                NewPassword = request.NewPassword,
                NewPasswordConfirm = request.NewPasswordConfirm,
                CheckPasswordComplexity = true,
                CheckIfUserPasswordCanBeChanged = true,
                EnforcePasswordHistory = true,
                SendPasswordSuccessfullyChangedEmail = true
            };
            var changePasswordResponse = _passwordService.ChangePassword(changePasswordRequest);
            if (!changePasswordResponse.IsSuccessful)
            {
                return new ResetForgottenPasswordResponse()
                {
                    IsSuccessful = false,
                    Message = changePasswordResponse.Message
                };
            }

            //
            //Update the password reset request entity to store that the request has been processed
            //
            securityPasswordResetRequest.Processed = true;
            securityPasswordResetRequest.ProcessDate = DateTime.Now;
            _repository.Commit();

            //
            //Successful reset of forgotten password if we got to here
            //
            return new ResetForgottenPasswordResponse()
            {
                IsSuccessful = true,
                Message = null
            };
        }
        #endregion

        #region Send the Forgot Password Email to the User
        /// <summary>
        /// Send the user the email telling them instructions on how to reset their password
        /// </summary>
        /// <param name="request"></param>
        public SendForgotPasswordEmailResponse SendForgotPasswordEmail(SendForgotPasswordEmailRequest request)
        {
            //
            //Validate parameters
            //
            if (request == null || request.SecurityPasswordResetRequest == null || (request.ADUser == null && request.SecurityUser == null))
            {
                return new SendForgotPasswordEmailResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid request was specified to the SendForgotPasswordEmail service."
                };
            }

            if (String.IsNullOrWhiteSpace(request.SecurityPasswordResetRequest.EmailAddress))
            {
                return new SendForgotPasswordEmailResponse()
                {
                    IsSuccessful = false,
                    Message = "An invalid request was specified to the SendForgotPasswordEmail service. The email address to send to is empty."
                };
            }

            //
            //Send the email
            //
            var adUser = request.ADUser;
            var securityUser = request.SecurityUser;
            var passwordResetRequest = request.SecurityPasswordResetRequest;
            try
            {
                _tokenResolver.GetTokenResolutionProvider().ClearTokens();
                var tokenValues = new Dictionary<String, String>();

                //
                //Create the URL for the user to reset their password with
                //
                String passwordResetId = AESEncryption.Encrypt(passwordResetRequest.SecurityPasswordResetRequestId.ToString());
                String passwordResetToken = AESEncryption.Encrypt(passwordResetRequest.Token);

                //
                //Setup the values for the Token Resolver so the email parses properly
                //Override the email address and other attributes since it may come from an AD User, 
                //we cant bank on this always being security user.
                //
                var firstName = (adUser != null ? adUser.FirstName : securityUser.FirstName);
                tokenValues.Add(TokenNames.FirstName.ToString(), firstName);
                var lastName = (adUser != null ? adUser.LastName : securityUser.LastName);
                tokenValues.Add(TokenNames.LastName.ToString(), lastName);
                tokenValues.Add(TokenNames.EmailAddress.ToString(), passwordResetRequest.EmailAddress);
                //We send both the encrypted ID and token to the user, making it harder for them to hack a reset entry
                String forgotPasswordUrl = String.Format("{0}Authentication/ForgotPassword/Reset?id={1}&t={2}",
                                                         DomainApplicationService.Instance.SiteBaseURL,
                                                         HttpUtility.UrlEncode(passwordResetId),
                                                         HttpUtility.UrlEncode(passwordResetToken));
                tokenValues.Add(TokenNames.ForgotPasswordURL.ToString(), forgotPasswordUrl);


                //
                //Process the email template and send it
                //
                var emailTemplate = _authenticationContentProvider.GetForgotPasswordEmail();
                _sendMail.MessageHtml = emailTemplate.Html;
                _sendMail.Subject = _tokenResolver.TokenizeString(emailTemplate.EmailSubject,
                                                                  TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                  passwordResetRequest.SecurityUserId.ToString(),
                                                                  tokenValues);
                _sendMail.Body = _tokenResolver.TokenizeString(emailTemplate.EmailBody,
                                                               TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                               passwordResetRequest.SecurityUserId.ToString(),
                                                               tokenValues);
                _sendMail.AddMessageTo(_tokenResolver.TokenizeString(emailTemplate.EmailTo,
                                                                     TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                     passwordResetRequest.SecurityUserId.ToString(),
                                                                     tokenValues));
                _sendMail.AddMessageCC(_tokenResolver.TokenizeString(emailTemplate.EmailCC,
                                                                     TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                     passwordResetRequest.SecurityUserId.ToString(),
                                                                     tokenValues));
                _sendMail.AddMessageBCC(_tokenResolver.TokenizeString(emailTemplate.EmailBCC,
                                                                      TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                      passwordResetRequest.SecurityUserId.ToString(),
                                                                      tokenValues));
                var fromAddress = _tokenResolver.TokenizeString(emailTemplate.EmailFrom,
                                                                TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                passwordResetRequest.SecurityUserId.ToString(),
                                                                tokenValues);
                var fromDisplayName = _tokenResolver.TokenizeString(emailTemplate.EmailFromDisplayName,
                                                                    TokenResolutionObjectTypes.SecurityUser.ToString(),
                                                                    passwordResetRequest.SecurityUserId.ToString(),
                                                                    tokenValues);
                var from = new MailAddress(fromAddress, fromDisplayName);
                _sendMail.SetMessageFrom(from);
                _sendMail.Send();
            }
            catch (Exception e)
            {
                String errorMessage = String.Format("An error occurred sending the forgot password email to user {0}",
                                                    (adUser != null ? adUser.UserName : securityUser.UserName));
                LogService.Instance.Log.Error(errorMessage, e);
                return new SendForgotPasswordEmailResponse()
                {
                    IsSuccessful = false,
                    Message = errorMessage
                };
            }

            //
            //Everything is good if we got hee
            //
            return new SendForgotPasswordEmailResponse()
            {
                IsSuccessful = true,
                Message = null
            };
        }
        #endregion

    }
}
