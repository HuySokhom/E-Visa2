using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class TestimonialView
    {
        public int Id { get; set; }

        [Display(Name="Title")]
        public string Title { get; set; }

        [Display(Name = "Language")]
        public string Language_Code { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Country")]
        [Required]
        public string Country { get; set; }

        [Display(Name="Subject")]
        public string Subject { get; set; }
        [Display(Name="Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name="Telephone")]
        [Phone]
        public string Tel { get; set; }

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
        public IList<SelectListItem> TitleList { get; set; }

      
    }

    public class TestimonialEditView
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Language")]
        public string Language_Code { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        
        [Display(Name = "Country")]
        [Required]
        public string Country { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telephone")]
        [Phone]
        public string Tel { get; set; }

        public string Url { get; set; }

        [Display(Name = "Date")]
        public DateTime Dates { get; set; }

        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase Photo { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
        public IList<SelectListItem> TitleList { get; set; }


    }
}