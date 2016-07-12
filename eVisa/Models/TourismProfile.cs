using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class TourismProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language_Code { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int IsActive { get; set; }

    }
}