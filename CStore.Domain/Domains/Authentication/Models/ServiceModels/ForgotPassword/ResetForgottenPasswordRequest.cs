using CStore.Domain.Models.ServiceModels;


namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.ForgotPassword
{
    /// <summary>
    /// Service request object for the method to reset a users forgotten password
    /// </summary>
    public class ResetForgottenPasswordRequest : DomainServiceRequest
    {
        /// <summary>
        /// The encrypted SecurityPasswordResetRequestId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The encrypted token in the SecurityPasswordResetRequest token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The new password to set for the user
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// The new password to set for the user. Must match the value in NewPassword
        /// </summary>
        public string NewPasswordConfirm { get; set; }
    }
}
