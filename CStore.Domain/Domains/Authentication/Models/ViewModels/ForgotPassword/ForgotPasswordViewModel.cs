using CStore.Domain.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Authentication.Models.ViewModels.ForgotPassword
{
    /// <summary>
    /// A model used for processing forgotten password requests where the user 
    /// needs to submit their username or email address to have a link emailed to them
    /// to reset their password.
    /// </summary>
    public class ForgotPasswordViewModel : DomainViewModel
    {
        /// <summary>
        /// The user name or email address to process the forgotten password request for
        /// </summary>
        [Required]
        [Display(Name = "User Name or Email Address")]
        [StringLength(100)]
        public string UserNameOrEmail { get; set; }
    }
}