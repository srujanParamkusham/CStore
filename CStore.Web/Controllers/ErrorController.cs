using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using Catalyst.MVC.Infrastructure.Providers.Security;
using CStore.Domain.Controllers;
using CStore.Domain.Services.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CStore.Web.Controllers
{
    /// <summary>
    /// Controller for the application errorss.
    /// </summary>
    public partial class ErrorController : DomainController
    {
        #region Member Variables
        /// <summary>
        /// Service to handle the controller actions
        /// </summary>
        private readonly IErrorControllerService _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="securityProvider"></param>
        public ErrorController(IErrorControllerService service, ISecurityProvider securityProvider)
            : base(securityProvider)
        {
            _service = service;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Index Action
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [IgnoreUserState]
        public virtual ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// 404 Action
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [IgnoreUserStateAttribute]
        public virtual ActionResult Http404()
        {
            return View("Http404");
        }

        /// <summary>
        /// 403 Action
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [IgnoreUserStateAttribute]
        public virtual ActionResult Http403()
        {
            return View("Http403");
        }
        #endregion
    }
}