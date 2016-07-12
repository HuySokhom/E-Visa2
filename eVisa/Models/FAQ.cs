using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class FAQ : DefaultColumn
    {
        public int Id { get; set; }
        public string Language_Code { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
}