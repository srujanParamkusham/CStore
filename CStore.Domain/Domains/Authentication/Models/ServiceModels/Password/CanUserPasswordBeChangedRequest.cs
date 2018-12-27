using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Util.ActiveDirectory;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.Password
{
    /// <summary>
    /// Service request object for the method to check if a user can change their password or not
    /// </summary>
    public class CanUserPasswordBeChangedRequest : DomainServiceRequest
    {
        /// <summary>
        /// The security user record for the user. This is optional, but at least one of the
        /// Security user or AD User needs to be specified.
        /// </summary>
        public SecurityUser SecurityUser { get; set; }

        /// <summary>
        /// The AD user record for the user. This is optional, but at least one of the
        /// Security user or AD User needs to be specified.
        /// </summary>
        public ADUser ADUser { get; set; }

        /// <summary>
        /// The users authentication method (either AD or SecurityUser).
        /// This can be null. If null, then the system will try to figure it out.
        /// </summary>
        public string AuthenticationMethod { get; set; }
    }
}
