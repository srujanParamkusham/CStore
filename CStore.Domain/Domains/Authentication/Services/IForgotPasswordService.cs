using CStore.Domain.Domains.Authentication.Models.ServiceModels.ForgotPassword;

namespace CStore.Domain.Domains.Authentication.Services
{
    /// <summary>
    /// Interface for all forgotten password reset and retrieval related services
    /// </summary>
    public interface IForgotPasswordService
    {
        SendInstructionsToResetPasswordResponse SendInstructionsToResetPassword(SendInstructionsToResetPasswordRequest request);

        ValidateSecurityPasswordResetTokenResponse ValidateSecurityPasswordResetToken(
            ValidateSecurityPasswordResetTokenRequest request);

        ResetForgottenPasswordResponse ResetForgottenPassword(ResetForgottenPasswordRequest request);

        SendForgotPasswordEmailResponse SendForgotPasswordEmail(SendForgotPasswordEmailRequest request);

    }
}
