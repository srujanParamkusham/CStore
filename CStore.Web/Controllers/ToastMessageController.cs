using Catalyst.MVC.Infrastructure.Controllers;
using Catalyst.MVC.Infrastructure.Providers.Security;
using Newtonsoft.Json;
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
    /// Controller for getting the toast messages as a JSON array to be displayed on a page
    /// </summary>
    public partial class ToastMessageController : DomainController
    {
        #region Member Variables

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="securityProvider"></param>
        public ToastMessageController(ISecurityProvider securityProvider)
            : base(securityProvider)
        {
        }

        #endregion

        #region Method used to get all the toast messages as a JSON array
        /// <summary>
        /// Get the toast messages queued up to be displayed as a JSON object
        /// To get the toast messages, make an AJAX call to http://localhost:60406/ToastMessage/Messages
        /// It will return a JSON object like this:
        /// [{"Title":"Record Saved","Message":"The record has been successfully saved.","AutoHide":true,"CloseButton":false,"Type":"Success","Position":"TopCenter"}]
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Messages()
        {
            var toastMessages = this.ToastMessages;
            if (this.ToastMessages != null)
            {
                //Use Newtonsoft to to avoid error: "A circular reference was detected while serializing an object of type..."
                var json = JsonConvert.SerializeObject(this.ToastMessages,
                                                       Formatting.None,
                                                       new JsonSerializerSettings()
                                                       {
                                                           ReferenceLoopHandling =
                                                                   Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                                       });
                return Content(json, "application/json");
            }
            else
            {
                return null;
            }
        }
        #endregion

    }
}