using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class MenuView
    {

        public int Id { get; set; }

        public int TypeMenu { get; set; }
        
        [Display(Name="Menu Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Language")]
        [Required]
        public string Language_Code { get; set; }

        public int Link_Code { get; set; }
        [Display(Name="Name")]
        public int Menu_Id { get; set; }

        [Display(Name="SubMenu")]

        public bool SubMenu { get; set; }



        public IList<SelectListItem> LanguageList { get; set; }
        public IList<SelectListItem> MenuList { get; set; }
        public MenuView MenuDetails { get; set; }
    }
    public class ItemMenu { 

        public string HOME { get; set; }
        public string SERVICE { get; set; }
        public string SERVICE_DIRECTORY { get; set; }
        public string GOVERNMENT_MINISTRIES { get; set; }
        public string MISSIONS { get; set; }
        public string CAMBODIAMISSIONS_WORLDWIDE { get; set; }
        public string FOREIGN_MISSIONS_TO_CAMBODIA { get; set; }
        public string INFORMATION { get; set; }
        public string TOUCHING_STORIES { get; set; }
        public string WE_NEED_YOUR_HELP { get; set; }
        public string CREDIT_THANKS { get; set; }
        public string TESTIMONIALS { get; set; }
        public string FAQ { get; set; }
        public string TELLUS_WHATYOU_THINK { get; set; }
        public string FREQUINTLY_ASKED_QUESTIONS { get; set; }
        public string PUBLIC_HOLIDAYINCAMBODIA { get; set; }
        public string ABOUT { get; set; }
        public string CONTACT { get; set; }
    }
}