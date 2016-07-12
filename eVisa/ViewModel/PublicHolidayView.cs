using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Models
{
    public class PublicHolidayView
    {
        public int Id { get; set; }
        [Display(Name="Language")]
        [Required]
        public string Language_Code { get; set; }

        [Display(Name = "Date")]
        [Required]
        public string Date { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Num Of Day")]
        [Required]
        public int NumOfDay { get; set; }

        [Display(Name = "Day")]
        [Required]
        public string Day { get; set; }

        [Display(Name = "Event")]
        [Required]
        public string Event { get; set; }
        public string Note { get; set; }

        public IList<SelectListItem> LanguageList { get; set; }
    }
}