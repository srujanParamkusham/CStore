using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CStore.Domain.Domains.Example.Models.ViewModels.Example
{
    /// <summary>
    /// This example view model is intended to demonstrate many of the attributes you may use
    /// on properties.
    /// </summary>
    public class ExampleIndexViewModel
    {
        [Display(Name = "Effective Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        //For range attributes, it appears you need to use a TextBoxFor and not EditorFor.
        //Otherwise the validation doesnt seem to actually fire when using the jquery unobtrusive validation.
        [Range(typeof(DateTime), "01/01/1900", "01/01/2099")]
        public DateTime? EffectiveDate { get; set; }

        [Display(Name = "Number of Hours")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Decimal? NumberOfHours { get; set; }

        [Display(Name = "Subject")]
        [StringLength(2000)]
        [Required]
        public String Subject { get; set; }

        [Display(Name = "Announcement Text")]
        [StringLength(16000)]
        [AllowHtml]
        public String HtmlText { get; set; }

        [Display(Name = "Sort")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32? Sort { get; set; }

        [Display(Name = "Password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public String NewPassword { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(255)]
        public string EmailAddress { get; set; }

    }
}
