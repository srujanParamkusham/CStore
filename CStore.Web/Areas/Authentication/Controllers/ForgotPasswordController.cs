using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using Catalyst.MVC.Infrastructure.Models;
using Catalyst.MVC.Infrastructure.Providers.Security;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Authentication.Models.ServiceModels.ForgotPassword;
using CStore.Domain.Domains.Authentication.Models.ViewModels.ForgotPassword;
using CStore.Domain.Domains.Authentication.Services;
using System;
using System.Web.Mvc;

namespace CStore.Web.Areas.Authentication.Controllers
{
    /// <summary>
    /// Controller used for handling the user forgot password screens so that the user can request a password be emailed to them.
    /// </summary>
    public partial class ForgotPasswordController : DomainController
    {
        #region Member Variables
        /// <summary>
        /// Service to handle the controller actions
        /// </summary>
        private readonly IForgotPasswordService _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="securityProvider"></param>
        public ForgotPasswordController(IForgotPasswordService service, ISecurityProvider securityProvider)
            : base(securityProvider)
        {
            _service = service;
        }
        #endregion

        #region Actions
        /// <summary>
        /// The main page to allow the user to reset their password
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [IgnoreUserStateAttribute]
        public virtual ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Process a forgot password request for the specified user.
        /// This will lookup their username and email them a link to reset their password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [IgnoreUserStateAttribute]
        public virtual ActionResult Index(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var request = new SendInstructionsToResetPasswordRequest()
            {
                UserNameOrEmail = model.UserNameOrEmail,
                IPAddress = Request.UserHostAddress
            };
            var response = _service.SendInstructionsToResetPassword(request);
            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View("Index", model);
            }
            else
            {
                return View("ForgotPasswordSuccessfullyRequested", model);
            }
        }

        /// <summary>
        /// Verify a users password reset request id and token, and prompt them for their new password.
        /// </summary>
        /// <param name="id">The encrypted ID of the SecurityUserPasswordResetHistory Entry</param>
        /// <param name="t">The encrypted token in the SecurityUserPasswordResetHistory Entry</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [IgnoreUserStateAttribute]
        public virtual ActionResult Reset(String id, String t)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //
            //Validate the id and token
            //This will decrypt the values, find the record, and ensure its not expired
            //If not valid then throw error, and redirect to ForgotPassword page to have them enter userid and email
            //
            var request = new ValidateSecurityPasswordResetTokenRequest()
            {
                Id = id,
                Token = t
            };
            var response = _service.ValidateSecurityPasswordResetToken(request);
            if (!response.IsSuccessful)
            {
                //Redirect to the home page with an error message if there is an issue
                AddToastMessage(new ToastMessage
                {
                    AutoHide = false,
                    Position = ToastMessage.ToastPosition.TopCenter,
                    Type = ToastMessage.ToastType.Error,
                    Title = "Invalid Request",
                    Message = response.Message
                });
                return RedirectToAction(MVC.Home.Index());
            }

            //
            //If valid, render the view allowing the password to be reset
            //
            var model = new ResetForgottenPasswordViewModel()
            {
                Id = id,
                Token = t
            };

            return View(model);
        }

        /// <summary>
        /// Process a forgot password request for the specified user.
        /// This will lookup their username and email them a link to reset their password
        /// </summary>
        /// <param name="id">The encrypted ID of the SecurityUserPasswordResetHistory Entry</param>
        /// <param name="t">The encrypted token in the SecurityUserPasswordResetHistory Entry</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [IgnoreUserStateAttribute]
        public virtual ActionResult Reset(ResetForgottenPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //
            //Validate the id and token
            //This will decrypt the values, find the record, and ensure its not expired
            //If not valid then throw error, and redirect to ForgotPassword page to have them enter userid and email
            //
            var request = new ResetForgottenPasswordRequest()
            {
                Id = model.Id,
                Token = model.Token,
                NewPassword = model.NewPassword,
                NewPasswordConfirm = model.NewPasswordConfirm
            };
            var response = _service.ResetForgottenPassword(request);
            if (!response.IsSuccessful)
            {
                //Redirect to the forgot password page with an error message if there is an issue
                ModelState.AddModelError("", response.Message);
                return View("Reset", model);
            }
            else
            {
                return View("PasswordSuccessfullyReset", model);
            }
        }


        #endregion

    }
}
