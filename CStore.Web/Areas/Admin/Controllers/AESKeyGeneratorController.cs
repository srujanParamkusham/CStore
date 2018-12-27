using Catalyst.MVC.Infrastructure.Providers.Security;
using Catalyst.MVC.Infrastructure.Util.Encryption;
using Newtonsoft.Json;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Admin.Models;
using CStore.Domain.Domains.Admin.Models.ViewModels.AESKeyGenerator;
using CStore.Domain.Domains.Admin.Services;
using CStore.Domain.Domains.Authentication.Models;
using CStore.Domain.Services.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace CStore.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller used for a basic admin utility to generate an AES Key
    /// </summary>
    public partial class AESKeyGeneratorController : DomainController
    {
        #region Member Variables

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="securityProvider"></param>
        public AESKeyGeneratorController(ISecurityProvider securityProvider)
            : base(securityProvider)
        {
        }
        #endregion

        #region Actions
        /// <summary>
        /// The main index page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Index()
        {
            var model = new AESKeyGeneratorViewModel
            {

            };

            return View(model);
        }

        /// <summary>
        /// The main index page. This will generate the key and return it to the user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Index(AESKeyGeneratorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_AESKeyForm", model);
            }

            var key = AESEncryption.GenerateKey();
            model.AESKey = key;
            return View("_AESKeyForm", model);
        }

        #endregion

    }
}
