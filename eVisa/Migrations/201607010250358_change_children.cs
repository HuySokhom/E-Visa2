namespace eVisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_children : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Children", "ChildDob", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ChildrenTemps", "ChildDob", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChildrenTemps", "ChildDob", c => c.String());
            AlterColumn("dbo.Children", "ChildDob", c => c.String());
        }
    }
}
