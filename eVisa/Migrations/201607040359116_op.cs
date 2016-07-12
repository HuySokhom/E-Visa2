namespace eVisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class op : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Children", "Application_id", "dbo.Applications");
            DropIndex("dbo.Children", new[] { "Application_id" });
            DropColumn("dbo.Children", "Application_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Children", "Application_id", c => c.Int());
            CreateIndex("dbo.Children", "Application_id");
            AddForeignKey("dbo.Children", "Application_id", "dbo.Applications", "id");
        }
    }
}
