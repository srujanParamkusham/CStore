using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Catalyst.MVC.Domain.Attributes.Controllers;
using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using CStore.Domain.IOC;

namespace CStore.Web.App_Start
{
    public static class WebApiConfig
    {
        public const string UrlPrefix = "api";
        public static string UrlPrefixRelative { get { return "~/api"; } }

        public static void Register(HttpConfiguration config)
        {

            // Web API routes
            //
            //Add attribute based routes. With attribute routing, it’s trivial to define a route for this URI. You simply add an attribute to the controller action:
            //[Route("customers/{customerId}/orders")]
            //public IEnumerable<Order> GetOrdersByCustomer(int customerId) { ... }
            //See http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#constraints for more details
            //
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: WebApiConfig.UrlPrefix + "/{controller}/{action}",
                defaults: new { action = RouteParameter.Optional },
                constraints: null
            );

            //
            //Add Catalyst Security and error handling
            //
            config.Filters.Add(new ApiBasicAuthenticationFilterAttribute());
            config.Filters.Add(new SecuredApiAttribute());
            config.Filters.Add(new ApiExceptionFilterAttribute());

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            //
            //Register custom ServiceActivator to support StructureMap and Dependency Injection in WebApi Controllers
            //
            config.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator(config));

            //
            //If an exception occurs, dont send the stack trace back to the user
            //
            //config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;
        }
    }
}

