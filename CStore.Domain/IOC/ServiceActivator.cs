using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using CStore.Domain.Services.State;

namespace CStore.Domain.IOC
{
    /// <summary>
    /// An implementation of the ServiceActivator used to implement WebAPI with StructureMap.
    /// This allows dependency injection to be done into WebApi controllers.
    /// Taken from the URL below, and tweaked replacing Factory with the 4.0 StructureMap call 
    /// to just use the container.
    /// http://stackoverflow.com/questions/18896758/webapi-apicontroller-with-structuremap
    /// </summary>
    public class ServiceActivator : IHttpControllerActivator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ServiceActivator(HttpConfiguration configuration) { }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="request"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = DomainApplicationService.Instance.IOCContainer.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}