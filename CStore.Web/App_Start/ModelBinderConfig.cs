using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.JQueryDataTables;
using CStore.Domain.Domains.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CStore.Web.App_Start
{
    /// <summary>
    /// Configure the custom model bindings
    /// </summary>
    public class ModelBinderConfig
    {
        /// <summary>
        /// Register
        /// </summary>
        public static void Register()
        {
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(JQueryDataTablesParameterModel),
                                                    new JQueryDataTablesModelBinder());
        }
    }
}