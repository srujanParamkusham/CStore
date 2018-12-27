using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Util.ActiveDirectory;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.Password
{
    /// <summary>
    /// Service request object for the method to send a user an email notifying them that their password has been changed
    /// </summary>
    public class SendPasswordChangedEmailRequest : DomainServiceRequest
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
    }
}
