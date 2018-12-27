using Catalyst.MVC.Infrastructure.Controllers;
using Catalyst.MVC.Infrastructure.Providers.Security;
using Newtonsoft.Json;
using CStore.Domain.Controllers;
using CStore.Domain.Domains.General.Models.ServiceModels.AppAnnouncements;
using CStore.Domain.Domains.General.Services;
using CStore.Domain.Models.ViewModels.Home;
using CStore.Domain.Services.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CStore.Web.Controllers
{
    /// <summary>
    /// Controller for the main home page for the application
    /// </summary>
    public partial class HomeController : DomainController
    {
        #region Member Variables

        /// <summary>
        /// Service to handle the controller actions
        /// </summary>
        private readonly IHomeControllerService _service;

        /// <summary>
        /// Service to handle the announcements display
        /// </summary>
        private readonly IAppAnnouncementsService _appAnnouncementsService;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="securityProvider"></param>
        public HomeController(IHomeControllerService service, ISecurityProvider securityProvider, IAppAnnouncementsService appAnnouncementsService)
            : base(securityProvider)
        {
            _service = service;
            _appAnnouncementsService = appAnnouncementsService;
        }

        #endregion

        #region Main Home Page Index Action

        /// <summary>
        /// The main index page
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            //TODO, remove these lines in an actual implementation. This just causes a divide by zero
            //error so we can test the custom error page.
            //var t = 0;
            //var i = 10/t;

            //TODO This is just an example of calling a WCF service
            //Remove me in the implementation of the project
            //_service.CallSampleWebServiceMethod();      

            //
            //Get the announcements to display on the home page
            //
            var appAnnouncementsRequest = new GetActiveAnnouncementsRequest()
            {

            };
            var appAnnouncementsResponse = _appAnnouncementsService.GetActiveAnnouncements(appAnnouncementsRequest);

            var model = new HomeViewModel();
            model.AppAnnouncements = appAnnouncementsResponse.AppAnnouncements;
            return View(model);
        }

        #endregion

    }
}