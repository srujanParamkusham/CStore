using System;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Authentication.Models.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        public String ActivationToken { get; set; }

        /// <summary>
        /// Where to redirect back to after successful login
        /// </summary>
        public String ReturnUrl { get; set; }

        /// <summary>
        /// The width of the users screen so that it can be logged in login history
        /// </summary>
        public int? ScreenWidth { get; set; }

        /// <summary>
        /// The height of the users screen so that it can be logged in login history
        /// </summary>
        public int? ScreenHeight { get; set; }
    }
}