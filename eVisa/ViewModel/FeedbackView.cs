using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class FeedbackView
    {
        public int Id { get; set; }
        [Display(Name="Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress(ErrorMessage="Invalid Email")]
        public string Email { get; set; }

        [Display(Name = "Your feedback")]
        [AllowHtml]
        public string Content { get; set; }
    }
}