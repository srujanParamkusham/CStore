using Catalyst.MVC.Domain.Domains.Authentication.Services;
using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using Catalyst.MVC.Infrastructure.Models;
using Catalyst.MVC.Infrastructure.Providers.Security;
using Catalyst.MVC.Infrastructure.Services.Log;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Authentication.Models;
using CStore.Domain.Domains.Authentication.Models.ServiceModels;
using CStore.Domain.Domains.Authentication.Models.ServiceModels.Password;
using CStore.Domain.Domains.Authentication.Models.ViewModels;
using CStore.Domain.Domains.Authentication.Models.ViewModels.Password;
using CStore.Domain.Domains.Authentication.Services;
using CStore.Domain.Services.State;
using CStore.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CStore.Web.Areas.Authentication.Controllers
{
    /// <summary>
    /// Controller used for handling the user password change and expired password screens.
    /// </summary>
    public partial class PasswordController : DomainController
    {
        #region Member Variables
        /// <summary>
        /// Service to handle the controller actions
        /// </summary>
        private readonly IPasswordService _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="securityProvider"></param>
        public PasswordController(IPasswordService service, ISecurityProvider securityProvider)
            : base(securityProvider)
        {
            _service = service;
        }
        #endregion

        #region Change Password Actions
        /// <summary>
        /// The main page for when a users password is to be changed
        /// </summary>
        /// <returns></returns>
        [IgnoreUserStateAttribute]
        public virtual ActionResult Change(String ReturnUrl = null)
        {
            //Store the return url that was sent to this page
            var u = new ChangePasswordViewModel()
            {
                ReturnUrl = ReturnUrl,
                UserName = DomainSessionService.Instance.CurrentUser.UserName
            };
            return View(u);
        }

        /// <summary>
        /// Process a password change for the specified user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreUserStateAttribute]
        public virtual ActionResult Change(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Change");
            }

            model.UserName = DomainSessionService.Instance.CurrentUser.UserName;

            var request = new ChangePasswordRequest()
            {
                UserName = DomainSessionService.Instance.CurrentUser.UserName,
                AuthenticationMethod = DomainSessionService.Instance.CurrentUser.AuthenticationMethod,
                SecurityUserId = DomainSessionService.Instance.CurrentUser.UserIdAsNullableInt,
                CurrentPassword = model.CurrentPassword,
                NewPassword = model.NewPassword,
                NewPasswordConfirm = model.NewPasswordConfirm,
                CheckPasswordComplexity = true,
                CheckIfUserPasswordCanBeChanged = true,
                EnforcePasswordHistory = true,
                SendPasswordSuccessfullyChangedEmail = true,
                CheckCurrentPassword = true
            };

            var response = _service.ChangePassword(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View("Change", model);
            }
            else
            {
                //Redirect to the home page with an error message if there is an issue
                AddToastMessage(new ToastMessage
                {
                    AutoHide = true,
                    Position = ToastMessage.ToastPosition.TopCenter,
                    Type = ToastMessage.ToastType.Success,
                    Title = "Password Changed",
                    Message = "Your password has been changed."
                });

                //
                //On successful password change, redirect to where the user is trying to go to.
                //
                if (model != null && !string.IsNullOrWhiteSpace(model.ReturnUrl))
                {
                    //LogService.Instance.Log.Debug("Redirecting to " + model.ReturnUrl);
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    //LogService.Instance.Log.Debug("Redirecting to " + MVC.Home.Index());
                    return RedirectToAction(MVC.Home.Index());
                }
            }
        }

        #endregion

        #region Expired Password Change Actions
        /// <summary>
        /// The main page for when a users password is expired
        /// </summary>
        /// <returns></returns>
        [IgnoreUserStateAttribute]
        public virtual ActionResult Expired(String ReturnUrl = null)
        {
            //Store the return url that was sent to this page
            var u = new ExpiredPasswordViewModel()
            {
                ReturnUrl = ReturnUrl
            };
            return View(u);
        }

        /// <summary>
        /// Process an expired password change for the specified user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreUserStateAttribute]
        public virtual ActionResult Expired(ExpiredPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Expired");
            }

            var request = new ChangePasswordRequest()
            {
                UserName = DomainSessionService.Instance.CurrentUser.UserName,
                AuthenticationMethod = DomainSessionService.Instance.CurrentUser.AuthenticationMethod,
                SecurityUserId = DomainSessionService.Instance.CurrentUser.UserIdAsNullableInt,
                NewPassword = model.NewPassword,
                NewPasswordConfirm = model.NewPasswordConfirm,
                CheckPasswordComplexity = true,
                CheckIfUserPasswordCanBeChanged = true,
                EnforcePasswordHistory = true,
                SendPasswordSuccessfullyChangedEmail = true,
                CheckCurrentPassword = false
            };

            var response = _service.ChangePassword(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View("Expired", model);
            }
            else
            {
                //Redirect to the home page with an error message if there is an issue
                AddToastMessage(new ToastMessage
                {
                    AutoHide = true,
                    Position = ToastMessage.ToastPosition.TopCenter,
                    Type = ToastMessage.ToastType.Success,
                    Title = "Password Changed",
                    Message = "Your password has been changed."
                });

                //
                //On successful password change, redirect to where the user is trying to go to.
                //
                if (model != null && !string.IsNullOrWhiteSpace(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction(MVC.Home.Index());
                }
            }
        }

        #endregion

    }
}
