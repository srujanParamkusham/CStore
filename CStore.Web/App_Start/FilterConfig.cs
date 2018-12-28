using Catalyst.MVC.Infrastructure.Attributes;
using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CStore.Web.App_Start
{
    public class FilterConfig
    {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HttpsAttribute());
          //  filters.Add(new SecuredAttribute());
        }

    }
}