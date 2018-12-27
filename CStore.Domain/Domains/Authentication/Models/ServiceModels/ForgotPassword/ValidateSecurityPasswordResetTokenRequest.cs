using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.ForgotPassword
{
    /// <summary>
    /// Service request object for the method to validate a users password reset request
    /// </summary>
    public class ValidateSecurityPasswordResetTokenRequest : DomainServiceRequest
    {
        /// <summary>
        /// The encrypted SecurityPasswordResetRequestId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The encrypted token in the SecurityPasswordResetRequest token
        /// </summary>
        public string Token { get; set; }
    }
}
