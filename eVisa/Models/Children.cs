using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eVisa.Models
{
    public class Children
    {
        public int id { get; set; }

        [StringLength(20)]
        public string ReferenceNo { get; set; }

        public int Line1 { get; set; }
        public int Line2 { get; set; }

        [StringLength(50)]
        public string ChildSurName { get; set; }
        
        [StringLength(50)]
        public string ChildGivenName { get; set; }

        [StringLength(20)]
        public string ChildSex { get; set; }

        public DateTime ChildDob { get; set; }
        public string ChildPhoto { get; set; }
        public int Status { get; set; }


        public DateTime StatusDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}