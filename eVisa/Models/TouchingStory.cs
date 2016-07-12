using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class TouchingStory:DefaultColumn
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Language_Code { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Country { get; set; }
        public string Subject { get; set; }
        public string Url { get; set; }
        public DateTime Dates { get; set; }
        
       
        public string Content { get; set; }
    }
}