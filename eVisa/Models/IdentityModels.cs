using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace eVisa.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            CreatedDate = DateTime.Now;
            IsActive = 1;
            Id = base.Id;
        }

        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public int IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("eVisaDBContext")
        {
            

        }
        
       
    }

    public class eVisaContext : DbContext {
        public eVisaContext()
            : base("eVisaDBContext")
        {
            

        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<NameofPort> NameofPorts { get; set; }
        public DbSet<TouristVisa> TouristVisas { get; set; }
        public DbSet<TourismProfile> Tourisms { get; set; }
        public DbSet<Ministry> Ministries { get; set; }
        public DbSet<MissionWorld> MissionWorlds { get; set; }
        public DbSet<ForeignMission> ForeignMissions { get; set; }
        public DbSet<CreditsThanks> CreditsThanks { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<TouchingStory> TouchingStorys { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<ContactInformation> ContactInformation { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Children> Children { get; set; }

        public DbSet<ApplicationTemp> ApplicationTemp { get; set; }

        public DbSet<ContactInformationTemp> ContactInformationTemp { get; set; }

        public DbSet<ChildrenTemp> ChildrenTemp { get; set; }

        public DbSet<ActionRecord> ActionRecord { get; set; }


    }



}