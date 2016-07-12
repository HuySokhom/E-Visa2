using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class Menu
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int TypeMenu { get; set; }
        public string Language_Code { get; set; }
        public int Link_Code { get; set; }
        public bool SubMenu { get; set; }
        public string Url { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int IsActive { get; set; }

    }
}