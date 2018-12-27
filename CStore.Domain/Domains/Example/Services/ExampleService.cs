using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Services.Log;
using Catalyst.Reporting.Export;
using StructureMap;
using CStore.Domain.Domains.Example.Models.ServiceModels.Example;
using CStore.Domain.Services;
using System.Linq;

namespace CStore.Domain.Domains.Example.Services
{
    /// <summary>
    /// An example of an application service.
    /// </summary>
    public class ExampleService : DomainService, IExampleService
    {
        #region Internals
        protected readonly IRepository _repository;
        protected readonly IContainer _container;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container"></param>
        /// <param name="repository"></param>
        [DefaultConstructor]
        public ExampleService(IContainer container, IRepository repository)
        {
            this._container = container;
            this._repository = repository;
        }
        #endregion

        #region ExampleServiceMethod 
        /// <summary>
        /// Example service method, demonstrates taking a simple request and response object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ExampleServiceMethodResponse ExampleServiceMethod(ExampleServiceMethodRequest request)
        {
            LogService.Instance.Log.Info("Example Service Method Called");

            var users = _repository.GetAll<SecurityUser>().ToList();
            foreach (var user in users)
            {
                LogService.Instance.Log.InfoFormat("Found user {0}", user.UserName);
            }

            return new ExampleServiceMethodResponse()
            {
                IsSuccessful = true,
                Message = "Service successfully completed",
                Users = users
            };
        }
        #endregion
    }
}
