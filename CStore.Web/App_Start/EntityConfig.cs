using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CStore.Web.App_Start
{
    /// <summary>
    /// Configure Catalyst entity framework extensions. Associate your connection string names with the namespaces for where those entities
    /// are located.
    /// </summary>
    public class EntityConfig
    {
        /// <summary>
        /// Register
        /// </summary>
        public static void Register()
        {
            DataContextConfig.Contexts.Add("DefaultConnection", new DataContextConfig("DefaultConnection", new string[] { "CStore.Domain", "Catalyst.MVC.Domain" }));
            DataContextConfig.Contexts.Add("SecurityConnection", new DataContextConfig("SecurityConnection", new string[] { "Catalyst.MVC.Domain" }));
        }
    }
}