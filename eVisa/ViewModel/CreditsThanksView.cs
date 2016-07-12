using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class CreditsThanksView
    {

        public int Id { get; set; }

        [Display(Name = "Language")]
        public string Language_Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name="National")]
        public string National { get; set; }

        public string Url { get; set; }

        [Display(Name = "Date")]
        public DateTime Dates { get; set; }

        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Image")]
        [Required]
        public HttpPostedFileBase Photo { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
    }

    public class CreditsThanksEditView
    {

        public int Id { get; set; }

        [Display(Name = "Language")]
        public string Language_Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "National")]
        public string National { get; set; }

        public string Url { get; set; }

        [Display(Name = "Date")]
        public DateTime Dates { get; set; }

        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase Photo { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
    }
}