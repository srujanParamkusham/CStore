using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using Catalyst.MVC.Infrastructure.Services.Report;

namespace CStore.Domain.Services.Report
{
    /// <summary>
    /// A base service implementation that all of application reporting services should extend. This allows for common
    /// service functionality to be specified here for your application.
    /// </summary>
    [Secured]
    public class DomainReportService : BaseReportService
    {
        #region Constants
        #endregion

        #region Member Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public DomainReportService(IRepository repository, ISendMailProvider sendMail) : base(repository, sendMail)
        {
        }
        #endregion

        #region Overrides
        #endregion

        #region Public Methods
        #endregion

    }
}
