using System;
using System.Collections.Generic;
using Catalyst.MVC.Infrastructure.Entities;
using Catalyst.MVC.Infrastructure.Attributes.Entity;
using Catalyst.MVC.Domain.Entities;

namespace CStore.Domain.Entities
{
    public partial class VWSecurityUser : DomainEntity
    {
        public int SecurityUserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AuthenticationMethod { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime? PasswordLastChangedDate { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public bool PasswordNeverExpires { get; set; }
        public bool UserCannotChangePassword { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool AccountActivated { get; set; }
        public bool AccountLocked { get; set; }
        public DateTime? AccountLockedDate { get; set; }
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
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyUser { get; set; }

        public string AssignedRoles { get; set; }

        public virtual ICollection<SecurityUserActivation> Activations { get; set; }
        public virtual ICollection<SecurityUserRoleMembership> RoleMemberships { get; set; }
    }
}
