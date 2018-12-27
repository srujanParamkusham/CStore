using Catalyst.MVC.Domain.Domains.Authentication.Services;
using Catalyst.MVC.Domain.Providers.Authentication;

namespace CStore.Domain.Domains.Authentication.Services
{
    /// <summary>
    /// Handle actions for the standard login controller
    /// </summary>
    public class DomainLoginService : LoginService, IDomainLoginService
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="membershipProvider"></param>
        public DomainLoginService(IApplicationMembershipProvider membershipProvider)
            : base(membershipProvider)
        {
        }
        #endregion
    }
}
