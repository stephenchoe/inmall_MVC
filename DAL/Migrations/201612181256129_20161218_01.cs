namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161218_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryPhotoes", "DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Specification_PS", c => c.String());
            AddColumn("dbo.ProductPhotoes", "DisplayOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductPhotoes", "DisplayOrder");
            DropColumn("dbo.Products", "Specification_PS");
            DropColumn("dbo.CategoryPhotoes", "DisplayOrder");
        }
    }
}
