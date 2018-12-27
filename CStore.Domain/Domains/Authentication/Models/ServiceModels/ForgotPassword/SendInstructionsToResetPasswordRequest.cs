using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.ForgotPassword
{
    /// <summary>
    /// Service request object for the method to send instructions to the user to reset their password
    /// </summary>
    public class SendInstructionsToResetPasswordRequest : DomainServiceRequest
    {
        public string UserNameOrEmail { get; set; }
        public string IPAddress { get; set; }
    }
}
