namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserKurs", newName: "KursApplicationUsers");
            DropPrimaryKey("dbo.KursApplicationUsers");
            AddPrimaryKey("dbo.KursApplicationUsers", new[] { "Kurs_KursId", "ApplicationUser_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.KursApplicationUsers");
            AddPrimaryKey("dbo.KursApplicationUsers", new[] { "ApplicationUser_Id", "Kurs_KursId" });
            RenameTable(name: "dbo.KursApplicationUsers", newName: "ApplicationUserKurs");
        }
    }
}
