using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class ContactView
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Language")]
        public string Language_Code { get; set; }

        [Display(Name = "Fax")]
        public string GeneralFax { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage="Invalid Email")]
        public string GeneralEmail { get; set; }

        [Display(Name = "Tel")]
        public string GeneralTel { get; set; }

        [Display(Name = "Fax")]
        public string PaymentFax { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage="Invalid Email")]
        public string Paymentemail { get; set; }

        [Display(Name = "Tel")]
        [Phone(ErrorMessage="Invalid Phone")]
        public string PaymentTel { get; set; }

        [Display(Name = "Photograph helps")]
        public string Photohelp { get; set; }

        [Display(Name = "Photoguide")]
        public string Photoguide { get; set; }

        [Display(Name = "link")]
        public string guidelink { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
    }
}