namespace Event_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vs1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Mobile = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        HallId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Halls", t => t.HallId, cascadeDelete: true)
                .Index(t => t.HallId);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Details = c.String(nullable: false),
                        Mobile = c.Int(nullable: false),
                        Seats = c.Int(nullable: false),
                        Status = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "HallId", "dbo.Halls");
            DropIndex("dbo.Books", new[] { "HallId" });
            DropTable("dbo.Halls");
            DropTable("dbo.Books");
        }
    }
}
