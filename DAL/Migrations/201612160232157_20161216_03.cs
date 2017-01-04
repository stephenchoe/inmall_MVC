namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161216_03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Specification_MadeIn", c => c.String());
            AddColumn("dbo.Products", "Specification_MadeBy", c => c.String());
            AddColumn("dbo.Products", "Specification_Size", c => c.String());
            DropColumn("dbo.Products", "Specification");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Specification", c => c.String());
            DropColumn("dbo.Products", "Specification_Size");
            DropColumn("dbo.Products", "Specification_MadeBy");
            DropColumn("dbo.Products", "Specification_MadeIn");
        }
    }
}
