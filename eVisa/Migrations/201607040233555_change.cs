namespace eVisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Children", "ApplicationId");
            DropColumn("dbo.ChildrenTemps", "ApplicationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChildrenTemps", "ApplicationId", c => c.Int(nullable: false));
            AddColumn("dbo.Children", "ApplicationId", c => c.Int(nullable: false));
        }
    }
}
