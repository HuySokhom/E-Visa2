using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eVisa.Models
{
    public class ApplicationTemp
    {
        public int id { get; set; }

        [StringLength(20)]
        public string ReferenceNo { get; set; }

        public int Line { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string SurName { get; set; }

        [StringLength(50)]
        public string GivenName { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }

        public DateTime? DOB { get; set; }
        
        public DateTime? ApplyDate { get; set; }

        public int Status { get; set; }
        public DateTime? StatusDate { get; set; }

        public string PaymentStatus { get; set; }
        public string EntryStatus { get; set; }
        public DateTime? EntryStatusDate { get; set; }

        [StringLength(20)]
        public string PaymentMethod { get; set; }

        [StringLength(50)]
        public string PassportNo { get; set; }

        [StringLength(50)]
        public string PassportCountry { get; set; }

        public DateTime? PassportIssueDate { get; set; }
        public DateTime? PassportExpiryDate { get; set; }

        [StringLength(50)]
        public string CountryOfBirth { get; set; }

        [StringLength(50)]
        public string Nationality { get; set; }

        public string ResidentialAddress { get; set; }

        public string PointOfEntry { get; set; }

        public DateTime? EntryDate { get; set; }


        [StringLength(50)]
        public string TravelMode { get; set; }

        public string ArrivalVehicleNo { get; set; }

        [StringLength(10)]
        public string ArrivalTime { get; set; }

        public string VisitAddress { get; set; }


        public string VisitPerson { get; set; }


        public string Photo { get; set; }

        public string Path { get; set; }

        public string ChildrenTravel { get; set; }

        public string VisaNo { get; set; }

        public int? FirstTime { get; set; }

        public int? GroupTour { get; set; }

        public string StatusImage { get; set; }

        public DateTime? ChargeBackDate { get; set; }


        public int Amount { get; set; }

        public int EmailFlag { get; set; }

        public string CreateFrom { get; set; }

        public string Source { get; set; }

        public string Txn_id { get; set; }

        public string ReferenceNoO { get; set; }

        public string VisaNoO { get; set; }

        public string Lupdt { get; set; }

        public string Userid { get; set; }

        public string Userid2 { get; set; }

        public string Code { get; set; }

        public string MiddleName { get; set; }

        public int Crit { get; set; }

        public string VisaType { get; set; }

        public int RefundType { get; set; }

        public int PaidType { get; set; }

        public string Comment { get; set; }

        public string UploadDoc { get; set; }

        public string EveryApps { get; set; }

        public string SubAcc { get; set; }

        public int Amoung_SGD { get; set; }

        public DateTime? RefundDate { get; set; }

        public string PointOfExit { get; set; }

        public DateTime CreateDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }


    }
}