using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class Contact : DefaultColumn
    {
        public int Id { get; set; }
        public string Language_Code { get; set; }

        public string GeneralFax { get; set; }
        public string GeneralEmail { get; set; }
        public string GeneralTel { get; set; }

        public string PaymentFax { get; set; }
        public string Paymentemail { get; set; }
        public string PaymentTel { get; set; }

        public string Photohelp { get; set; }

        public string Photoguide { get; set; }
        public string guidelink { get; set; }
    }
}