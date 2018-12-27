using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using Catalyst.MVC.Infrastructure.Controllers;

namespace CStore.Domain.Controllers
{
    /// <summary>
    /// A base controller implementation that all of application reporting controllers should extend. This allows for common
    /// controller functionality to be specified here for your application.
    /// </summary>
    [Secured]
    public class DomainReportController : BaseReportController
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
        public DomainReportController() : base()
        {

        }
        #endregion

        #region Overrides
        #endregion

        #region Public Methods
        #endregion

    }
}
