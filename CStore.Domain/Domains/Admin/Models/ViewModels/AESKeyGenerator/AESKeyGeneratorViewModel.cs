using CStore.Domain.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace CStore.Domain.Domains.Admin.Models.ViewModels.AESKeyGenerator
{

    public class AESKeyGeneratorViewModel : DomainViewModel
    {
        [Display(Name = "AES Key")]
        [StringLength(255)]
        public string AESKey { get; set; }

    }
}