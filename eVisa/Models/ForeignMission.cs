using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class ForeignMission:DefaultColumn
    {
        public int Id { get; set; }
        public string Language_Code { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
    }
}