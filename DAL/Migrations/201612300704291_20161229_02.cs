namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161229_02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppUserId = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Address_CityId = c.Int(nullable: false),
                        Address_DistrictId = c.Int(nullable: false),
                        Address_ZipCode = c.String(),
                        Address_StreetAddress = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonInfoes");
        }
    }
}
