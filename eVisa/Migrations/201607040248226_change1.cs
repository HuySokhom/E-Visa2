namespace eVisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Children", "Application_id", c => c.Int());
            CreateIndex("dbo.Children", "Application_id");
            AddForeignKey("dbo.Children", "Application_id", "dbo.Applications", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Children", "Application_id", "dbo.Applications");
            DropIndex("dbo.Children", new[] { "Application_id" });
            DropColumn("dbo.Children", "Application_id");
        }
    }
}
