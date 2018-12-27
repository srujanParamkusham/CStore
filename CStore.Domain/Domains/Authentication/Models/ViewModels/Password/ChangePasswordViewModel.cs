using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Authentication.Models.ViewModels.Password
{
    /// <summary>
    /// A model used for changing the users password.
    /// </summary>
    public class ChangePasswordViewModel : DomainViewModel
    {
        [Display(Name = "User Name")]
        [StringLength(50)]
        public String UserName { get; set; }

        [Required]
        [Display(Name = "Current Password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public String CurrentPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public String NewPassword { get; set; }

        [Required]
        [Display(Name = "Retype Your New Password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public String NewPasswordConfirm { get; set; }


    }
}