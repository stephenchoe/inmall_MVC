namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161229_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ReceiverName", c => c.String());
            AddColumn("dbo.Orders", "ReceiverAddress_CityId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ReceiverAddress_DistrictId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ReceiverAddress_ZipCode", c => c.String());
            AddColumn("dbo.Orders", "ReceiverAddress_StreetAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ReceiverAddress_StreetAddress");
            DropColumn("dbo.Orders", "ReceiverAddress_ZipCode");
            DropColumn("dbo.Orders", "ReceiverAddress_DistrictId");
            DropColumn("dbo.Orders", "ReceiverAddress_CityId");
            DropColumn("dbo.Orders", "ReceiverName");
        }
    }
}
