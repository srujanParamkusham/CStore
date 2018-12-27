using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Util.Extensions;
using CStore.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.SecurityUserMaintenance
{

    public class SecurityUserMaintenanceEditViewModel : DomainViewModel
    {
        public int SecurityUserId { get; set; }

        [Display(Name = "User Name")]
        [StringLength(100)]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(255)]
        public string EmailAddress { get; set; }

        [Display(Name = "External Id")]
        [StringLength(255)]
        public string ExternalId { get; set; }

        [Display(Name = "First Name")]
        [StringLength(500)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(500)]
        public string LastName { get; set; }

        [Display(Name = "Authentication Method")]
        public string AuthenticationMethod { get; set; }

        [Display(Name = "Password Last Changed On")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PasswordLastChangedDate { get; set; }

        [Display(Name = "Password Expires On")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PasswordExpirationDate { get; set; }

        [Display(Name = "Password Never Expires")]
        public bool PasswordNeverExpires { get; set; }

        [Display(Name = "User Cannot Change Password")]
        public bool UserCannotChangePassword { get; set; }

        [Display(Name = "Last Login Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "Account Activated")]
        public bool AccountActivated { get; set; }

        [Display(Name = "Account Locked")]
        public bool AccountLocked { get; set; }

        [Display(Name = "Account Locked On")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? AccountLockedDate { get; set; }

        [Display(Name = "Account Expires On")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? AccountExpirationDate { get; set; }

        [Display(Name = "Number of Consecutive Failed Logins")]
        public int NumConsecutiveFailedLogins { get; set; }

        [Display(Name = "Activation Requested On")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? ActivationRequestDate { get; set; }

        [Display(Name = "Activation Confirmed On")]
        public DateTime? ActivationConfirmedDate { get; set; }

        [Display(Name = "Activation Cookie")]
        public string ActivationCookie { get; set; }

        [Display(Name = "Terms and Conditions Version")]
        public string TermsAndConditionsVersion { get; set; }

        [Display(Name = "Active Directory Guid")]
        public Guid? ActiveDirectoryGuid { get; set; }

        [Display(Name = "Active Directory DN")]
        public string ActiveDirectoryDn { get; set; }

        [Display(Name = "User Type")]
        [StringLength(100)]
        public string UserType { get; set; }

        [Display(Name = "Time Zone")]
        public string TimeZone { get; set; }

        [Display(Name = "Locale")]
        public string Locale { get; set; }

        [Display(Name = "System Administrator (User will have access to everything)")]
        public bool SystemAdmin { get; set; }

        [Display(Name = "Active User")]
        public bool Active { get; set; }

        [Display(Name = "Created On")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Created By")]
        public string CreateUser { get; set; }

        [Display(Name = "Modified On")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "Modified By")]
        public string ModifyUser { get; set; }

        //Used to assign the user a new password
        [Display(Name = "User's Password (Only enter to change the user's password)")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public String NewPassword { get; set; }

        [Display(Name = "Retype User's Password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public String NewPasswordConfirm { get; set; }

        [Display(Name = "Available Roles")]
        public List<SecurityRole> AvailableRoles { get; set; }

        public List<SelectListItem> AvailableRolesSelectList
        {
            get
            {
                if (AvailableRoles == null)
                {
                    return new List<SelectListItem>();
                }
                else
                {
                    return AvailableRoles.ToSelectList(p => p.Name, p => p.SecurityRoleId.ToString());
                }
            }
        }

        [Display(Name = "Assigned Roles")]
        public List<SecurityRole> AssignedRoles { get; set; }

        public List<SelectListItem> AssignedRolesSelectList
        {
            get
            {
                if (AssignedRoles == null)
                {
                    return new List<SelectListItem>();
                }
                else
                {
                    return AssignedRoles.ToSelectList(p => p.Name, p => p.SecurityRoleId.ToString());
                }
            }
        }

        public List<int> AssignedRoleIds { get; set; }

    }
}