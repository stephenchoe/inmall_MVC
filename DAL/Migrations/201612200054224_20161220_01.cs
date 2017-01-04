namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161220_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "UserHostAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "UserHostAddress");
        }
    }
}
