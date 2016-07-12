using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eVisa.ViewModel
{
    public class MenuCombo
    {
        [Display(Name="Menu")]
        [Required]
        public int Link_Code { get; set; }
    }
}