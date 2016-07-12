using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.ViewModel
{
    public class HomeView
    {

        public PageView Pageview { get; set; }
        public IEnumerable<TouristVisaView> TouristTable { get; set; }
        public IEnumerable<ImageView> ImageView { get; set; }
        public IEnumerable<NameofPortView> NameofPort { get; set; }
        //public Dictionary<int,string> Menus { get; set; }
        public IEnumerable<MenuView> NavMenu { get; set; }
        public IList<SelectListItem> MenuList { get; set; }
        public IEnumerable<LanguageView> LanguageList { get; set; }
        public PageView PageInformation { get; set; }
        public PageView PageAnnounment { get; set; }

       
    }
}