using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class Users
    {
        public int id { get; set; }
        public string surname { get; set; }
        public string middle_name { get; set; }
        
        public string given_name { get; set; }
        public string sex { get; set; }
        public DateTime? dob { get; set; }
        public string country { get; set; }
        public string nationality { get; set; }
        public string photo { get; set; }
        public string passport_no { get; set; }
        public string passport_issue { get; set; }
        public string country_issue { get; set; }
        public string passport_expire { get; set; }
        public string contact_name { get; set; }
        public string primary_email { get; set; }
        public string secondary_email { get; set; }

        public string phone_number { get; set; }
        public string residential_address { get; set; }
        public string heard_from { get; set; }
        public string user_id { get; set; }
        public string password { get; set; }
        public DateTime create_date { get; set; }
        public string create_by { get; set; }
        public DateTime modified { get; set; }

        public string update_by { get; set; }
    }
}