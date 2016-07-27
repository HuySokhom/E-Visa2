namespace eVisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contacgt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactInformations", "PaymentStatus", c => c.String(maxLength: 20));
            AddColumn("dbo.ContactInformationTemps", "PaymentStatus", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactInformationTemps", "PaymentStatus");
            DropColumn("dbo.ContactInformations", "PaymentStatus");
        }
    }
}
