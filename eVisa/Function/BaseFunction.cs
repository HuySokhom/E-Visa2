using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace eVisa.Function
{
    public class BaseFunction
    {
        public BaseFunction()
        { 
        
        }

        private eVisaContext db = new eVisaContext();

        public IList<SelectListItem> getMenuList(int type) {

            var listmenu = new Dictionary<int, string>();
            if (type == 1) {
                listmenu.Add(1, "Home");
                listmenu.Add(2, "Service");
                listmenu.Add(3, "Service drectory");
                listmenu.Add(4, "Government ministries");
                listmenu.Add(5, "Missions");
                listmenu.Add(6, "Cambodia missions worldwide");
                listmenu.Add(7, "Foreign missions to cambodia");
                listmenu.Add(8, "Information");
                listmenu.Add(9, "Touching stories");
                listmenu.Add(10, "We need your help");
                listmenu.Add(11, "Credits & thanks");
                listmenu.Add(12, "Testimonials");
                listmenu.Add(13, "FAQ");
                listmenu.Add(14, "Tell us what you think");
                listmenu.Add(15, "Frequintly asked questions");
                listmenu.Add(16, "Public holiday in cambodia");
                listmenu.Add(17, "About");
                listmenu.Add(18, "Contact");
                listmenu.Add(19, "Guidelines");
                listmenu.Add(20, "Check");
                listmenu.Add(21, "Apply Now");
                listmenu.Add(22, "Log In");
                listmenu.Add(23, "Visa Exemption");
                listmenu.Add(24, "Search Reference Number");
                listmenu.Add(25, "How to apply");
                listmenu.Add(26, "Map");
                
            }
            if (type == 2) {
                listmenu.Add(1, "Embassy");
                listmenu.Add(2, "Frequenty e-Visa User");
                listmenu.Add(3, "Search Reference Number");
                listmenu.Add(4, "Check & Change");
                listmenu.Add(5, "Check Applcation");
                listmenu.Add(6, "Download e-Visa Certificate");
                listmenu.Add(7, "Change travel information");
                listmenu.Add(8, "Resubmite payment");
                listmenu.Add(9, "Print receipt");

                //listmenu.Add(10, "Apply Now");
                //listmenu.Add(11, "Log In");
                //listmenu.Add(12, "TESTIMONIALS");
               
            }
            
            return
                listmenu.Select(l => new SelectListItem
                {
                    Value = l.Key.ToString(),
                    Text = l.Value
                }).ToList();
        
        }

        

        public IList<SelectListItem> getLanguage(int flag)
        {
            var listlang = new Dictionary<string, string>();


            return (flag == 1 ? db.Languages.Where(l => l.IsActive == 1).Select(l => new SelectListItem
                {
                    Value = l.Code,
                    Text = l.Name
                }).ToList() :
                db.Languages.Where(l => l.IsActive == 1 && l.Code != "en").Select(l => new SelectListItem
                {
                    Value = l.Code,
                    Text = l.Name
                }).ToList()
                );
                
        }

        public IList<SelectListItem> getTitle()
        {
            var listlang = new Dictionary<string, string>();

            listlang.Add("Mr", "Mr.");
            listlang.Add("Miss", "Miss.");
            listlang.Add("Mrs", "Mrs.");
            listlang.Add("Dr", "Dr.");

            return
                listlang.Select(l => new SelectListItem
                {

                    Value = l.Key,
                    Text = l.Value
                }).ToList();
        }
        
        public IList<SelectListItem> getMenu()
        {

            return db.Menus.Where(e => e.Language_Code == "en" && e.IsActive == 1).AsEnumerable()
                .Select(l => new SelectListItem
             {
                 Value = l.Id.ToString(),
                 Text = l.Name
             }).ToList();

            //return
            //    listlang.Select(l => new SelectListItem
            //    {
            //        Value = l.Key,
            //        Text = l.Value
            //    }).ToList();

        }

        public IList<SelectListItem> getPageMenu()
        {
            var listlang = new Dictionary<int, string>();

            listlang.Add(1, "Home");
            listlang.Add(2, "About");
            listlang.Add(3, "Contact");
            listlang.Add(4, "Guidelines");
            listlang.Add(5, "PublicHoliday");
            listlang.Add(6, "VisaExemption");
            listlang.Add(7, "Information");
            listlang.Add(8, "Announcement");

            //listlang.Add(5, "Index 5");

            return
                listlang.Select(l => new SelectListItem
                {
                    Value = l.Key.ToString(),
                    Text = l.Value
                }).ToList();
        }

        public IList<SelectListItem> getIndexTitle()
        {
            var listlang = new Dictionary<int, string>();

            listlang.Add(1, "Index 1");
            listlang.Add(2, "Index 2");
            //listlang.Add(3, "Index 3");
            //listlang.Add(4, "Index 4");
            //listlang.Add(5, "Index 5");

            return
                listlang.Select(l => new SelectListItem
                {
                    Value = l.Key.ToString(),
                    Text = l.Value
                }).ToList();
        }


        public Dictionary<int, string> getItemMenu(eVisaContext db,IEnumerable<MenuView> menu)
        {
            
            var listmenu = new Dictionary<int, string>();
       
            var dmenu = db.Menus.Where(e => e.Language_Code == "en" && e.IsActive == 1 && e.TypeMenu == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  });
            
            foreach (var m in dmenu)
            {
                listmenu.Add(m.Link_Code, m.Name);
            }
      
            foreach (var d in menu) {
                listmenu[d.Link_Code] = d.Name;
            }

            

            return listmenu;
        }

        public PageView getPageContent(eVisaContext db,PageView page,int i) {

            PageView pageView = new PageView();

            var pageViewDefault = db.Pages.Where(e => e.Language_Code == "en" && e.IsActive == 1 && e.Link_Code == i)
                 .Select(e => new PageView
                 {
                     Content = e.Content,
                     Title = e.Title
                 }).FirstOrDefault();

                if (page != null)
                {
                    pageView.Code = page.Code;
                    pageView.Content = page.Content;
                    pageView.Description = page.Description;
                    pageView.Name = page.Name;
                    pageView.Title = page.Title;
                }
                else if (pageViewDefault != null)
                {
                    pageView.Code = pageViewDefault.Code;
                    pageView.Content = pageViewDefault.Content;
                    pageView.Description = pageViewDefault.Description;
                    pageView.Name = pageViewDefault.Name;
                    pageView.Title = pageViewDefault.Title;
                }
                else {
                    return null;
                }
            
            return pageView;
        
        }

        public IEnumerable<MinistryView> getMinistryContent(eVisaContext db, IEnumerable<MinistryView> page)
        {

            IEnumerable<MinistryView> pageViewDefault = null;

            if (page.ToList().Count > 0)
            {
                return page;
            }
            else {

                pageViewDefault = db.Ministries.Where(e => e.Language_Code == "en" && e.IsActive == 1)
                 .Select(e => new MinistryView
                 {
                     Id = e.Id,
                     Name = e.Name,
                     Language_Code = e.Language_Code,
                     Address = e.Address,
                     Tel = e.Tel,
                     Fax = e.Fax,
                     Email = e.Email,
                     Website = e.Website

                 }).AsEnumerable();
            

                return pageViewDefault;
            }
           
        }

        public IEnumerable<TourismView> getTourismContent(eVisaContext db, IEnumerable<TourismView> page)
        {

            IEnumerable<TourismView> pageViewDefault = null;

            if (page.ToList().Count > 0)
            {

                return page;
            }
            else
            {

                pageViewDefault = db.Tourisms.Where(e => e.Language_Code == "en" && e.IsActive == 1)
                 .Select(e => new TourismView
                 {
                     Id = e.Id,
                     Language_Code = e.Language_Code,
                     Content = e.Content,
                     Url = e.Url,
                     Name = e.Name,
                     Tel = e.Tel,
                     Website = e.Website,
                     Fax = e.Fax,
                     Email = e.Email
                 }).AsEnumerable();


                return pageViewDefault;
            }

        }



        public IEnumerable<MissionWorldView> getMissionWorldsContent(eVisaContext db, IEnumerable<MissionWorldView> page)
        {

            IEnumerable<MissionWorldView> pageViewDefault = null;

            if (page.ToList().Count > 0)
            {

                return page;
            }
            else
            {
                pageViewDefault = db.MissionWorlds.Where(e => e.Language_Code == "en" && e.IsActive == 1)
                 .Select(e => new MissionWorldView
                 {
                     Id = e.Id,
                     Language_Code = e.Language_Code,
                     Mission = e.Mission,
                     Name = e.Name,
                     Tel = e.Tel,
                     Website = e.Website,
                     Header = e.Header,
                     Email = e.Email,
                     Country = e.Country,
                     Address = e.Address,
                     Fax = e.Fax,
                 }).AsEnumerable();


                return pageViewDefault;
            }

        }

        public IEnumerable<ForeignMissionView> getForeignMissionContent(eVisaContext db, IEnumerable<ForeignMissionView> page)
        {

            IEnumerable<ForeignMissionView> pageViewDefault = null;

            if (page.ToList().Count > 0)
            {

                return page;
            }
            else
            {
                pageViewDefault = db.ForeignMissions.Where(e => e.Language_Code == "en" && e.IsActive == 1)
                 .Select(e => new ForeignMissionView
                 {
                     Id = e.Id,
                     Name = e.Name,
                     Tel = e.Tel,
                     Website = e.Website,
                     Language_Code = e.Language_Code,
                     Fax = e.Fax,
                     Email = e.Email,
                     Address = e.Address
                 }).AsEnumerable();


                return pageViewDefault;
            }

        }

        public IEnumerable<TouchingStoryView> getTouchingStoryContent(eVisaContext db, IEnumerable<TouchingStoryView> page)
        {

            IEnumerable<TouchingStoryView> pageViewDefault = null;

            if (page.ToList().Count > 0)
            {

                return page;
            }
            else
            {
                pageViewDefault = db.TouchingStorys.Where(e => e.Language_Code == "en" && e.IsActive == 1)
                 .Select(e => new TouchingStoryView
                 {
                     Id = e.Id,
                     Email = e.Email,
                     Dates = e.Dates,
                     Country = e.Country,
                     Content = e.Content,
                     Name = e.Name,
                     Url = e.Url,
                     Title = e.Title,
                     Tel = e.Tel,
                     Language_Code = e.Language_Code,
                 }).AsEnumerable();


                return pageViewDefault;
            }

        }

        public IEnumerable<TestimonialView> getTestimonialContent(eVisaContext db, IEnumerable<TestimonialView> page)
        {

            IEnumerable<TestimonialView> pageViewDefault = null;

            if (page.ToList().Count > 0)
            {

                return page;
            }
            else
            {
                pageViewDefault = db.Testimonials.Where(e => e.Language_Code == "en" && e.IsActive == 1)
                 .Select(e => new TestimonialView
                 {
                     Id = e.Id,
                     Language_Code = e.Language_Code,
                     Name = e.Name,
                     Title = e.Title,
                     Url = e.Url,
                     Subject = e.Subject,
                     Tel = e.Tel,
                     Country = e.Country,
                     Dates = e.Dates,
                     Email = e.Email,
                     Content = e.Content
                 }).AsEnumerable();


                return pageViewDefault;
            }

        }

        public IEnumerable<FAQView> getFAQContent(eVisaContext db, IEnumerable<FAQView> page)
        {

            IEnumerable<FAQView> pageViewDefault = null;

            if (page.ToList().Count > 0)
            {

                return page;
            }
            else
            {
                pageViewDefault = db.FAQs.Where(e => e.Language_Code == "en" && e.IsActive == 1)
                 .Select(e => new FAQView
                 {
                     Id = e.Id,
                     question = e.question,
                     answer = e.answer
                 }).AsEnumerable();


                return pageViewDefault;
            }

        }

        public string getLinked(int i) {
          
            string newUrl = "";
            string Url="";
          
            switch (i) {
                case 1 : Url = " "; break;
                case 2 : Url = "Home/About"; break;
                case 3 : Url = "Home/Contact"; break;
                case 4 : Url = "Information/Guidelines"; break;
                case 5 : Url = "Information/Public"; break;
                case 6 : Url = "Information/Exemption"; break;
                case 7 : Url = " "; break;
                case 8 : Url = " "; break;
            }

            Uri originalUri = System.Web.HttpContext.Current.Request.Url;
            //+ originalUri.LocalPath
            newUrl = originalUri.Scheme +
                "://" + originalUri.Authority +"/" + Url;


            return newUrl;
        }

        public string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;
            string newUrl = serverUrl;
            Uri originalUri = System.Web.HttpContext.Current.Request.Url;
           //+ originalUri.LocalPath
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority  + newUrl;
            
            return newUrl;
        }

        public string getString(Object o) {
            return (o == null ? "" : o.ToString());
        }

        /* generate reference with user id for application */
        public string GenerateReferenceNo(string UserId)
        {
            /* generate reference no with datetime */
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            var month = "";
            var day = "";
            // check month concat with string 0 if it is smaller than 9
            if (datevalue.Month <= 9)
            {
                month = Convert.ToString(datevalue.Month);
                month = "0" + month;
            }
            else
            {
                month = Convert.ToString(datevalue.Month);
            }

            // check day if smaller than 9 concat string 0
            if (datevalue.Day <= 9)
            {
                day = Convert.ToString(datevalue.Day);
                day = "0" + day;
            }
            else
            {
                day = Convert.ToString(datevalue.Day);
            }

            String year = datevalue.Year.ToString().Substring(2, 2);

            // check count user in application table if used to save 
            int count = db.Application.Count(co => co.Userid.Equals(UserId) && co.Status == 0);
            
            if(count > 0){
                var user = db.Application.Where( a => a.Userid.Equals(UserId) && a.Status == 0).Select(
                    app => new {
                        ReferenceNo = app.ReferenceNo,
                    }).FirstOrDefault();
                // return the same ReferenceNo from application
                return user.ReferenceNo;
            }
            else
            {
                // Generate new ReferenceNo
                int countRef = db.Application.Count( co => EntityFunctions.TruncateTime(co.CreateDate) == EntityFunctions.TruncateTime(DateTime.Now) );
                countRef = countRef + 1;

                string countNo = "";
                if (countRef <= 99999)
                {
                    countNo = Convert.ToString(countRef);
                    countNo = "0000" + countNo;
                    // get last string of count concate
                    countNo = countNo.Substring(countNo.Length - 5);
                }
                else
                {
                    countNo = Convert.ToString(countRef);
                }
                var referenceNo = 1 + year + month + day + countNo;

                // Check if duplication Reference NO
                int check = db.Application.Count(c => c.ReferenceNo == referenceNo);
                if(check > 0){
                    var result = db.Application.OrderByDescending(a => a.id).First();
                    var temp = Double.Parse(result.ReferenceNo) + countRef;
                    //var maxValue = db.Application.Max(a => a.id);
                    //var reff = Double.Parse(referenceNo) + check;
                    referenceNo = Convert.ToString(temp);

                }
                return referenceNo;

            }
            
        }

    }
}