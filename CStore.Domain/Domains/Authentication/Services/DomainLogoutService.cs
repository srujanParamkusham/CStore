using Catalyst.MVC.Domain.Domains.Authentication.Services;
using Catalyst.MVC.Domain.Providers.Authentication;

namespace CStore.Domain.Domains.Authentication.Services
{
    /// <summary>
    /// Handle actions for the standard logout controller
    /// </summary>
    public class DomainLogoutService : LogoutService, IDomainLogoutService
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="membershipProvider"></param>
        public DomainLogoutService(IApplicationMembershipProvider membershipProvider)
            : base(membershipProvider)
        {
        }
        #endregion
    }
}
