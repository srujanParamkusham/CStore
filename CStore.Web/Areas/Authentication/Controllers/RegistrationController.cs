using Catalyst.MVC.Domain.Domains.Authentication.Services;
using Catalyst.MVC.Infrastructure.Providers.Security;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Authentication.Services;
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
    /// Controller used for handling the user registration screens such as terms and conditions acceptance.
    /// </summary>
    public partial class RegistrationController : DomainController
    {
        #region Member Variables
        /// <summary>
        /// Service to handle the controller actions
        /// </summary>
        private readonly IRegistrationService _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="securityProvider"></param>
        public RegistrationController(IRegistrationService service, ISecurityProvider securityProvider)
            : base(securityProvider)
        {
            _service = service;
        }
        #endregion

        #region Actions
        /// <summary>
        /// The main controller for user registration
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            return View();
        }
        #endregion

    }
}
