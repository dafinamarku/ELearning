namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationshiponetooneMaterialUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserKurs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserKurs", "Kurs_KursId", "dbo.Kurset");
            DropIndex("dbo.ApplicationUserKurs", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserKurs", new[] { "Kurs_KursId" });
            AddColumn("dbo.Kurset", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Kurset", "Instruktori_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "DrejtonKursId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Kurs_KursId", c => c.Int());
            CreateIndex("dbo.Kurset", "ApplicationUser_Id");
            CreateIndex("dbo.Kurset", "Instruktori_Id");
            CreateIndex("dbo.AspNetUsers", "DrejtonKursId");
            CreateIndex("dbo.AspNetUsers", "Kurs_KursId");
            AddForeignKey("dbo.AspNetUsers", "DrejtonKursId", "dbo.Kurset", "KursId", cascadeDelete: true);
            AddForeignKey("dbo.Kurset", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Kurset", "Instruktori_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Kurs_KursId", "dbo.Kurset", "KursId");
            DropTable("dbo.ApplicationUserKurs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserKurs",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Kurs_KursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Kurs_KursId });
            
            DropForeignKey("dbo.AspNetUsers", "Kurs_KursId", "dbo.Kurset");
            DropForeignKey("dbo.Kurset", "Instruktori_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Kurset", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "DrejtonKursId", "dbo.Kurset");
            DropIndex("dbo.AspNetUsers", new[] { "Kurs_KursId" });
            DropIndex("dbo.AspNetUsers", new[] { "DrejtonKursId" });
            DropIndex("dbo.Kurset", new[] { "Instruktori_Id" });
            DropIndex("dbo.Kurset", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Kurs_KursId");
            DropColumn("dbo.AspNetUsers", "DrejtonKursId");
            DropColumn("dbo.Kurset", "Instruktori_Id");
            DropColumn("dbo.Kurset", "ApplicationUser_Id");
            CreateIndex("dbo.ApplicationUserKurs", "Kurs_KursId");
            CreateIndex("dbo.ApplicationUserKurs", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserKurs", "Kurs_KursId", "dbo.Kurset", "KursId", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserKurs", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
