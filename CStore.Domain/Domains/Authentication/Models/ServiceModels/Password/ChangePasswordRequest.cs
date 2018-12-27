using CStore.Domain.Models.ServiceModels;

namespace CStore.Domain.Domains.Authentication.Models.ServiceModels.Password
{
    /// <summary>
    /// Service request object for the method to change a users password
    /// </summary>
    public class ChangePasswordRequest : DomainServiceRequest
    {
        /// <summary>
        /// The users name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The users authentication method (either AD or SecurityUser).
        /// This can be null. If null, then the system will try to figure it out.
        /// </summary>
        public string AuthenticationMethod { get; set; }

        /// <summary>
        /// The security user ID record of the user. You only
        /// need to specify this if the Authentication Method is SecurityUser.
        /// </summary>
        public int? SecurityUserId { get; set; }

        /// <summary>
        /// The current password to set for the user
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// If true, check if the users password matches the current password on file
        /// </summary>
        public bool CheckCurrentPassword { get; set; }

        /// <summary>
        /// The new password to set for the user
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// The new password to set for the user. Must match the value in NewPassword
        /// </summary>
        public string NewPasswordConfirm { get; set; }

        /// <summary>
        /// If true, then the password must meet complexity requirements
        /// </summary>
        public bool CheckPasswordComplexity { get; set; }

        /// <summary>
        /// If true, then the validation rules will be run to determine if this users password is allowed to be changed
        /// </summary>
        public bool CheckIfUserPasswordCanBeChanged { get; set; }

        /// <summary>
        /// If true, and the password is being changed for a SecurityUser record,
        /// then the password will be checked to ensure its not the current password for the user
        /// nor is it in the last X password history records for the user. X is defined
        /// as an application parameter in web.config or app.config. Access it
        /// through the Application Service: ApplicationService.Instance.PasswordHistoryRecordsToCheckOnPasswordChange
        /// </summary>        
        public bool EnforcePasswordHistory { get; set; }

        /// <summary>
        /// If true, when the password is successfully changed the user will be sent an email
        /// </summary>
        public bool SendPasswordSuccessfullyChangedEmail { get; set; }
    }
}
