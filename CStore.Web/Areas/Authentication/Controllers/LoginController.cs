using Catalyst.MVC.Domain.Domains.Authentication.Models.ServiceModels.Login;
using Catalyst.MVC.Domain.Domains.Authentication.Models.ServiceModels.Logout;
using Catalyst.MVC.Infrastructure.Attributes.ActionFilters;
using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using Catalyst.MVC.Infrastructure.Models;
using Catalyst.MVC.Infrastructure.Providers.Security;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Authentication.Models.ViewModels.Login;
using CStore.Domain.Domains.Authentication.Services;
using CStore.Domain.Services.State;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace CStore.Web.Areas.Authentication.Controllers
{
    /// <summary>
    /// Controller used for handling the user login screens
    /// </summary>
    [IgnoreUserState]
    public partial class LoginController : DomainController
    {
        #region Member Variables
        /// <summary>
        /// Service to handle the controller actions
        /// </summary>
        private readonly IDomainLoginService _service;
        private readonly IDomainLogoutService _logoutService;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logoutService"></param>
        /// <param name="securityProvider"></param>
        public LoginController(IDomainLoginService service, IDomainLogoutService logoutService, ISecurityProvider securityProvider)
            : base(securityProvider)
        {
            _service = service;
            _logoutService = logoutService;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Main login page
        /// </summary>
        /// <returns></returns>
        [ImportModelStateFromTempData]
        [AllowAnonymous]
        public virtual ActionResult Index(String ReturnUrl = null)
        {
            //Store the return url that was sent to this page
            var u = new LoginViewModel()
            {
                ReturnUrl = ReturnUrl
            };
            return View(u);
        }

        /// <summary>
        /// User entered valid credentials, but is in an invalid state and still can't access the site.
        /// </summary>
        /// <returns></returns>
        [IgnoreUserState]
        [AllowAnonymous]
        public virtual ActionResult InvalidUserState()
        {
            _logoutService.LogoutUser(new LogoutUserRequest());
            return View("Index");
        }

        /// <summary>
        /// Main login page post handler
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ExportModelStateToTempData]
        [AllowAnonymous]
        public virtual ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var authenticateUserRequest = new AuthenticateUserRequest()
            {
                UserName = model.UserName,
                Password = model.Password,
                RememberMe = model.RememberMe,
                ScreenHeight = model.ScreenHeight,
                ScreenWidth = model.ScreenWidth,
                ActivationToken = model.ActivationToken,
                ReturnUrl = model.ReturnUrl,
                HttpResponse = Response
            };
            var authenticationResult = _service.AuthenticateUser(authenticateUserRequest);
            if (!authenticationResult.IsSuccessful)
            {
                ModelState.AddModelError("", authenticationResult.Message);

                //Add a toast message
                AddToastMessage(new ToastMessage
                {
                    AutoHide = true,
                    Position = ToastMessage.ToastPosition.TopCenter,
                    Type = ToastMessage.ToastType.Error,
                    Title = "Login Failed",
                    Message = (!String.IsNullOrWhiteSpace(authenticationResult.Message) ? authenticationResult.Message : "Invalid UserName or Password")
                });

                return View(model);
            }
            else
            {
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

        /// <summary>
        /// Activate the user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual ActionResult Activate(string userName, string token)
        {
            var activateUserRequest = new ActivateUserRequest()
            {
                UserName = userName,
                Token = token,
                CurrentUser = DomainSessionService.Instance.CurrentUser,
                HttpResponse = Response
            };
            var activateUserResponse = _service.ActivateUser(activateUserRequest);

            return RedirectToAction(activateUserResponse.IsSuccessful ? MVC.Home.Index() : MVC.Authentication.Login.Index());
        }
        #endregion
    }
}
