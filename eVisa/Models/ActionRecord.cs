using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class ActionRecord
    {

        public int Id { get; set; }

        public string OnPage { get; set; }

        public string Url { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public DateTime CreateDate { get; set; }
        
    }

}