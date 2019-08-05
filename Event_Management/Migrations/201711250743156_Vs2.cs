namespace Event_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vs2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Email", c => c.String());
            AlterColumn("dbo.Books", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Halls", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Halls", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Halls", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Halls", "Status", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Halls", "Status", c => c.String());
            AlterColumn("dbo.Halls", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Halls", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Halls", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Name", c => c.String());
            DropColumn("dbo.Books", "Email");
        }
    }
}
