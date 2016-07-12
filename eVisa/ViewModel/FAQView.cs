using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class FAQView
    {
        public int Id { get; set; }

        [Display(Name = "Language")]
        public string Language_Code { get; set; }

        [Display(Name="Question")]
        [Required]
        [AllowHtml]
        public string question { get; set; }

        [Required]
        [Display(Name="Answer")]
        [AllowHtml]
        public string answer { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }

    }
}