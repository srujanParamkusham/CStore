using Catalyst.MVC.Domain.Domains.Authentication.Models.ServiceModels.Logout;
using Catalyst.MVC.Infrastructure.Attributes.ActionFilters;
using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using Catalyst.MVC.Infrastructure.Controllers;
using Catalyst.MVC.Infrastructure.Providers.Security;
using Catalyst.MVC.Infrastructure.Services.Log;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Authentication.Services;
using CStore.Domain.Services.State;
using System.Web.Mvc;
using System.Web.Routing;

namespace CStore.Web.Areas.Authentication.Controllers
{
    /// <summary>
    /// Controller used for handling the user logout screens
    /// </summary>   
    public partial class LogoutController : DomainController
    {
        #region Member Variables
        /// <summary>
        /// Service to handle the controller actions
        /// </summary>
        private readonly IDomainLogoutService _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="securityProvider"></param>
        public LogoutController(IDomainLogoutService service, ISecurityProvider securityProvider)
            : base(securityProvider)
        {
            _service = service;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Logout the current user
        /// </summary>
        /// <returns></returns>
        [IgnoreUserStateAttribute]
        [AllowAnonymous]
        public virtual ActionResult Index()
        {
            _service.LogoutUser(new LogoutUserRequest());
            return View();
        }
        #endregion
    }
}
