namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161216_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PayWayProducts",
                c => new
                    {
                        PayWay_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PayWay_Id, t.Product_Id })
                .ForeignKey("dbo.PayWays", t => t.PayWay_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.PayWay_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.Products", "Summary_Item1", c => c.String());
            AddColumn("dbo.Products", "Summary_Item2", c => c.String());
            AddColumn("dbo.Products", "Summary_Item3", c => c.String());
            AddColumn("dbo.Products", "Summary_Item4", c => c.String());
            AddColumn("dbo.Products", "Summary_Item5", c => c.String());
            AddColumn("dbo.Products", "Features_Item1", c => c.String());
            AddColumn("dbo.Products", "Features_Item2", c => c.String());
            AddColumn("dbo.Products", "Features_Item3", c => c.String());
            AddColumn("dbo.Products", "Features_Item4", c => c.String());
            AddColumn("dbo.Products", "Features_Item5", c => c.String());
            AddColumn("dbo.Suppliers", "Email", c => c.String());
            DropColumn("dbo.Products", "Summary");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Summary", c => c.String());
            DropForeignKey("dbo.PayWayProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.PayWayProducts", "PayWay_Id", "dbo.PayWays");
            DropIndex("dbo.PayWayProducts", new[] { "Product_Id" });
            DropIndex("dbo.PayWayProducts", new[] { "PayWay_Id" });
            DropColumn("dbo.Suppliers", "Email");
            DropColumn("dbo.Products", "Features_Item5");
            DropColumn("dbo.Products", "Features_Item4");
            DropColumn("dbo.Products", "Features_Item3");
            DropColumn("dbo.Products", "Features_Item2");
            DropColumn("dbo.Products", "Features_Item1");
            DropColumn("dbo.Products", "Summary_Item5");
            DropColumn("dbo.Products", "Summary_Item4");
            DropColumn("dbo.Products", "Summary_Item3");
            DropColumn("dbo.Products", "Summary_Item2");
            DropColumn("dbo.Products", "Summary_Item1");
            DropTable("dbo.PayWayProducts");
        }
    }
}
