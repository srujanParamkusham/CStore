using CStore.Domain.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.PasswordHashGenerator
{

    public class PasswordHashGeneratorViewModel : DomainViewModel
    {
        [Display(Name = "Password (PlainText)")]
        [StringLength(255)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Salt")]
        [StringLength(50)]
        public string Salt { get; set; }

        [Display(Name = "Password (Hashed)")]
        [StringLength(255)]
        public string PasswordHash { get; set; }

    }
}