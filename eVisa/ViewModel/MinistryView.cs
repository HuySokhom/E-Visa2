
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class MinistryView
    {
        public int Id { get; set; }
        [Display(Name="Ministry Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Language")]
        public string Language_Code { get; set; }

        [Display(Name = "Telephone")]
        [Required]
        [Phone(ErrorMessage="Invalid Phone")]
        public string Tel { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage="Invalid Email")]
        public string Email { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
    }

    public class ForeignMissionView:MinistryView
    {

    }
}