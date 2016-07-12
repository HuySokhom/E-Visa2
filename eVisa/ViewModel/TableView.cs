using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class TouristVisaView
    {

        public int Id { get; set; }

        [Display(Name="Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Language")]
        [Required]
        public string Language_code { get; set; }

        [Display(Name="Text")]
        
        public string Text { get; set; }

        [Display(Name = "Header")]
        public bool Header { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }

    }

    public class NameofPortView {

        public int Id { get; set; }

        [Display(Name = "Language")]
        [Required]
        public string Language_Code { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Border")]
        public string Border { get; set; }

        [Display(Name = "Entry")]
        public string Entry { get; set; }

        [Display(Name = "Exit")]
        public string Exit { get; set; }

        [Display(Name = "Header")]
        public bool Header { get; set; }
        public int IsActive { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
    
    }

    public class PageViewTemplate{

        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name="Content")]
        public string Content { get; set; }

        [Display(Name = "Language")]
        public string Language_Code { get; set; }
    
     }
}