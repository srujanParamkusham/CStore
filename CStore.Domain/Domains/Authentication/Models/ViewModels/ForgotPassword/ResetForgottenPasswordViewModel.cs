using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Authentication.Models.ViewModels.ForgotPassword
{
    /// <summary>
    /// A model used for resetting the forgotten password.
    /// </summary>
    public class ResetForgottenPasswordViewModel : DomainViewModel
    {
        public String Id { get; set; }
        public String Token { get; set; }

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