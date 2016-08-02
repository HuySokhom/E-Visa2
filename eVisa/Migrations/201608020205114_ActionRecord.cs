namespace eVisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActionRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OnPage = c.String(),
                        Url = c.String(),
                        UserId = c.String(),
                        UserEmail = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActionRecords");
        }
    }
}
