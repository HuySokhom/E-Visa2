using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class DefaultColumn
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int IsActive { get; set; }
    }
}