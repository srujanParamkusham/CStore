using CStore.Domain.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityUserMaintenance
{
    /// <summary>
    /// Service request object to save a user
    /// </summary>
    public class SecurityUserMaintenanceSaveRequest : DomainServiceSaveRequest
    {
        [Key]
        public int SecurityUserId { get; set; }

        [Required]
        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string ExternalId { get; set; }

        //[Required]
        public string FirstName { get; set; }

        //[Required]
        public string LastName { get; set; }

        public string AuthenticationMethod { get; set; }

        public DateTime? PasswordLastChangedDate { get; set; }

        [Range(typeof(DateTime), "01/01/1900", "01/01/2099")]
        public DateTime? PasswordExpirationDate { get; set; }

        public bool PasswordNeverExpires { get; set; }

        public bool UserCannotChangePassword { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool AccountActivated { get; set; }

        public bool AccountLocked { get; set; }

        public DateTime? AccountLockedDate { get; set; }

        [Range(typeof(DateTime), "01/01/1900", "01/01/2099")]
        public DateTime? AccountExpirationDate { get; set; }

        public int NumConsecutiveFailedLogins { get; set; }

        public DateTime? ActivationRequestDate { get; set; }

        public DateTime? ActivationConfirmedDate { get; set; }

        public string ActivationCookie { get; set; }

        public string TermsAndConditionsVersion { get; set; }

        public Guid? ActiveDirectoryGuid { get; set; }

        public string ActiveDirectoryDn { get; set; }

        public string UserType { get; set; }

        public string TimeZone { get; set; }

        public string Locale { get; set; }

        public bool SystemAdmin { get; set; }

        public bool Active { get; set; }

        public String NewPassword { get; set; }

        public String NewPasswordConfirm { get; set; }

        public List<int> AssignedRoleIds { get; set; }

    }
}
