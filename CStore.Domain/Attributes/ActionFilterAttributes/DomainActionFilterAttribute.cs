using Catalyst.MVC.Infrastructure.Attributes.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CStore.Domain.Attributes.ActionFilterAttributes
{
    /// <summary>
    /// A base filter attribute you can extend, and is called on all controller actions.
    /// </summary>
    public class DomainActionFilterAttribute : BaseActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}
