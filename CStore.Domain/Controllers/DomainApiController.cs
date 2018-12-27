using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using Catalyst.MVC.Infrastructure.Controllers;
using Catalyst.MVC.Infrastructure.Providers.Security;

namespace CStore.Domain.Controllers
{
    /// <summary>
    /// A base Web API controller implementation that all of your Web API application controllers should extend. This allows for common
    /// controller functionality to be specified here for your application.
    /// </summary>
    [Secured]
    public class DomainApiController : BaseApiController
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
        public DomainApiController() : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="securityProvider"></param>
        public DomainApiController(ISecurityProvider securityProvider)
            : base(securityProvider)
        {
        }

        #endregion

        #region Overrides
        #endregion

        #region Public Methods
        #endregion

    }
}
