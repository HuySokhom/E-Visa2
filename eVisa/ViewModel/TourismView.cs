using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class TourismView
    {

        public int Id { get; set; }

        [Display(Name = "Language")]
        public string Language_Code { get; set; }

        [Display(Name="Name")]
        [Required]
        public string Name { get; set; }

        public string Url { get; set; }
        public string Content { get; set; }
        [Display(Name="Telephone")]
        [Phone]
        public string Tel { get; set; }

        [Display(Name="Fax")]
        public string Fax { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "Image")]
        [Required]
        public HttpPostedFileBase Photo { get; set; }
        
        public IList<SelectListItem> LanguageList { get; set; }
    }

    public class TourismEditView
    {

        public int Id { get; set; }

        [Display(Name = "Language")]
        public string Language_Code { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        public string Url { get; set; }
        public string Content { get; set; }
        [Display(Name = "Telephone")]
        public string Tel { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase Photo { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
    }
}