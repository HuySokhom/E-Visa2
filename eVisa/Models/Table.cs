using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    
    public class NameofPort {

        public int Id { get; set; }
        public string Language_Code { get; set; }
        public string Name { get; set; }
        public string Border { get; set; }
        public string Entry { get; set; }
        public string Exit { get; set; }
        public bool Header { get; set; }
        public int IsActive { get; set; }

      
    }

    public class TouristVisa{
    
        public int Id { get; set; }
        public string Language_Code { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool Header { get; set; }
        public int IsActive { get; set; }

    }

    public class MissionWorld:DefaultColumn {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language_Code { get; set; }
        public string Country { get; set; }
        public string Mission { get; set; }

        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public bool Header { get; set; }
    }
}