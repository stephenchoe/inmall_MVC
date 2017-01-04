namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161215_05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductPhotoes", "FileBytes", c => c.Binary());
            AddColumn("dbo.CategoryPhotoes", "FileBytes", c => c.Binary());
            AddColumn("dbo.CategoryPhotoes", "Top", c => c.Boolean(nullable: false));
            DropColumn("dbo.ProductPhotoes", "Bytes");
            DropColumn("dbo.CategoryPhotoes", "Bytes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CategoryPhotoes", "Bytes", c => c.Binary());
            AddColumn("dbo.ProductPhotoes", "Bytes", c => c.Binary());
            DropColumn("dbo.CategoryPhotoes", "Top");
            DropColumn("dbo.CategoryPhotoes", "FileBytes");
            DropColumn("dbo.ProductPhotoes", "FileBytes");
        }
    }
}
