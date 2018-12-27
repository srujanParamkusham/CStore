using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using Catalyst.MVC.Infrastructure.Providers.WebService;
using CStore.Domain.Services.WebService;
using System.Web;

namespace CStore.Domain.Services.Controller
{
    /// <summary>
    /// Service for the home page
    /// </summary>
    public class HomeControllerService : DomainService, IHomeControllerService
    {
        #region Member Variables
        private readonly IRepository _repository;
        private readonly ISendMailProvider _sendMail;
        private readonly HttpContext _httpContext;
        private readonly IWebServiceProvider _webService;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="membershipProvider"></param>
        public HomeControllerService(IRepository repository, ISendMailProvider sendMail, IWebServiceProvider webService)
        {
            _repository = repository;
            this._sendMail = sendMail;
            _httpContext = HttpContext.Current;
            _webService = webService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// An example of using the Web Service Provider to call a sample web service
        /// </summary>
        //TODO Remove this method in your project implementation, this is just an example of calling a WCF service
        public void CallSampleWebServiceMethod()
        {
            using (var channel = _webService.CreateChannel<IExampleWCFService>("BasicHttpBinding_IExampleWCFService"))
            {
                var result = channel.GetData(1);
                var y = result;
            }
        }
        #endregion

    }
}