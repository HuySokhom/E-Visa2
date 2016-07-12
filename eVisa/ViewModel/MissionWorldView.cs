using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class MissionWorldView
    {
        public int Id { get; set; }
        [Display(Name="Language")]
        [Required]
        public string Language_Code { get; set; }
        [Display(Name="Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name="Country")]
        [Required]
        public string Country { get; set; }
        [Display(Name="Chief of Mission")]
        public string Mission { get; set; }
        [Display(Name="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string Tel { get; set; }
        [Display(Name="Fax")]
        public string Fax { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Display(Name="Website")]
        public string Website { get; set; }

        
        [Display(Name="Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Header")]
        public bool Header { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
    }
}