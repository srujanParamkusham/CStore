using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using CStore.Domain.Services;
using System.Web;


namespace CStore.Domain.Domains.Authentication.Services
{
    /// <summary>
    /// Service for managing the user registration process
    /// </summary>
    public class RegistrationService : DomainService, IRegistrationService
    {
        #region Member Variables
        private readonly IRepository _repository;
        private readonly ISendMailProvider _sendMail;
        private readonly HttpContext _httpContext;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="membershipProvider"></param>
        public RegistrationService(IRepository repository, ISendMailProvider sendMail)
        {
            _repository = repository;
            this._sendMail = sendMail;
            _httpContext = HttpContext.Current;
        }
        #endregion

    }
}
