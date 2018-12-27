using CStore.Domain.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Authentication.Models.ViewModels.Password
{
    /// <summary>
    /// A model used for changing the users password when it has expired.
    /// </summary>
    public class ExpiredPasswordViewModel : DomainViewModel
    {
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