using CStore.Domain.Domains.Authentication.Models.ServiceModels.Password;

namespace CStore.Domain.Domains.Authentication.Services
{
    /// <summary>
    /// Interface for all password related services including changing passwords, determining password complexity, etc.
    /// </summary>
    public interface IPasswordService
    {
        CanUserPasswordBeChangedResponse CanUserPasswordBeChanged(CanUserPasswordBeChangedRequest request);

        ChangePasswordResponse ChangePassword(ChangePasswordRequest request);

        CheckPasswordComplexityResponse CheckPasswordComplexity(CheckPasswordComplexityRequest request);

        SendPasswordChangedEmailResponse SendPasswordChangedEmail(SendPasswordChangedEmailRequest request);

        CalculatePasswordExpirationDateResponse CalculatePasswordExpirationDate(
            CalculatePasswordExpirationDateRequest request);
    }
}
