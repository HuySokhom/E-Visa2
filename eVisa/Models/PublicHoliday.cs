using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class PublicHoliday : DefaultColumn
    {
        public int Id { get; set; }
        public string Language_Code { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public int NumOfDay { get; set; }
        public string Day { get; set; }
        public string Event { get; set; }
        public string Note { get; set; }
    }
}