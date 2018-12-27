using Catalyst.MVC.Domain.Entities;
using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.ForgotPassword
{
    /// <summary>
    /// Service response object for the method to validate a users password reset request
    /// </summary>
    public class ValidateSecurityPasswordResetTokenResponse : DomainServiceResponse
    {
        /// <summary>
        /// If this is a valid password reset request, then the record in the table containing
        /// the SecurityPasswordResetRequest details are contained in this property
        /// </summary>
        public SecurityPasswordResetRequest SecurityPasswordResetRequest { get; set; }
    }
}
