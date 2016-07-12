using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eVisa.ViewModel
{
    public class LanguageView
    {
        public int Id { get; set; }
        [Display(Name="Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Code")]
        [Required]
        public string Code { get; set; }

        
        [Display(Name = "Path")]
        public string Url { get; set; }

        [Display(Name = "Image")]
        [Required]
        public HttpPostedFileBase Photo { get; set; }

        public int Index { get; set; }
    }

    public class LanguageEditView {

        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Code")]
        [Required]
        public string Code { get; set; }


        [Display(Name = "Path")]
        public string Url { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase Photo { get; set; }

        public int Index { get; set; }
    }
}