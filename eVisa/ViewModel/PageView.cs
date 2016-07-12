using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace eVisa.ViewModel
{
    public class PageView
    {

        public PageView() { 
        
        }

        public int Id { get; set; }

        [Display(Name="Page Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Language Code")]
        [Required]
        public string Language_Code { get; set; }

        public int Code { get; set; }

        [Display(Name = "Index Content")]
        [Required]
        public int Link_Code { get; set; }

        [Display(Name = "Content Title")]
        [Required]
        public string Title { get; set; }


        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Menu")]
        public int MenuId { get; set; }

        public string Links { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
        public IList<SelectListItem> MenuList { get; set; }
        public IList<SelectListItem> TitleIndexList { get; set; }
        
    }

    public class PageViewHome {

        [Display(Name = "Content Title")]
        [Required]
        public string Title { get; set; }


        [Display(Name = "Content")]
        public string Content { get; set; }

    }
}