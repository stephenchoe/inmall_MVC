namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161220_03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Address");
        }
    }
}
