using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class ContactInformationTemp
    {

        public int id { get; set; }

        [StringLength(20)]
        public string ReferenceNo { get; set; }
        
        [StringLength(50)]
        public string SurName { get; set; }
        
        [StringLength(50)]
        public string GivenName { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }
        
        [StringLength(50)]
        public string Country { get; set; }
        
        [StringLength(50)]
        public string PhoneNo { get; set; }

        [StringLength(100)]
        public string PrimaryEmail { get; set; }
        
        [StringLength(100)]
        public string SecondaryEmail { get; set; }

        [StringLength(100)]
        public string HeardFrom { get; set; }
        public string Lupdt { get; set; }

        [StringLength(20)]
        public string UserId { get; set; }

        [StringLength(550)]
        public string Password { get; set; }

        [StringLength(20)]
        public string Profile { get; set; }
        public DateTime? DOB { get; set; }
        public string Nationality { get; set; }
        public string Photo { get; set; }
        public string PassportNo { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public string CountryIssue { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public string ContactName { get; set; }
        public string ResidentialAddress { get; set; }

        [StringLength(20)]
        public string PaymentStatus { get; set; }
        public int Status { get; set; }
        public string mobile_id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}