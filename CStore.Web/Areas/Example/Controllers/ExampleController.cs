using Catalyst.MVC.Infrastructure.Providers.Security;
using Newtonsoft.Json;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.Example.Models.ServiceModels.Example;
using CStore.Domain.Domains.Example.Models.ViewModels.Example;
using CStore.Domain.Domains.Example.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace CStore.Web.Areas.Example.Controllers
{
    /// <summary>
    /// A very basic example controller
    /// </summary>
    public partial class ExampleController : DomainController
    {
        //TODO Delete this whole example controller area in your implementation
        #region Member Variables
        /// <summary>
        /// Service for the controller
        /// </summary>
        private IExampleService _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="securityProvider"></param>
        /// <param name="service"></param>
        public ExampleController(ISecurityProvider securityProvider, IExampleService service)
            : base(securityProvider)
        {
            _service = service;
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
            var model = new ExampleIndexViewModel
            {

            };
            return View(model);
        }
        #endregion

    }
}
