namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInstruktoriId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "DrejtonKursId", "dbo.Kurset");
            DropForeignKey("dbo.Kurset", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Kurset", "Instruktori_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Kurs_KursId", "dbo.Kurset");
            DropIndex("dbo.Kurset", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Kurset", new[] { "Instruktori_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "DrejtonKursId" });
            DropIndex("dbo.AspNetUsers", new[] { "Kurs_KursId" });
            CreateTable(
                "dbo.ApplicationUserKurs",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Kurs_KursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Kurs_KursId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Kurset", t => t.Kurs_KursId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Kurs_KursId);
            
            AddColumn("dbo.Kurset", "InstruktoriId", c => c.String());
            DropColumn("dbo.Kurset", "ApplicationUser_Id");
            DropColumn("dbo.Kurset", "Instruktori_Id");
            DropColumn("dbo.AspNetUsers", "DrejtonKursId");
            DropColumn("dbo.AspNetUsers", "Kurs_KursId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Kurs_KursId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "DrejtonKursId", c => c.Int(nullable: false));
            AddColumn("dbo.Kurset", "Instruktori_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Kurset", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ApplicationUserKurs", "Kurs_KursId", "dbo.Kurset");
            DropForeignKey("dbo.ApplicationUserKurs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserKurs", new[] { "Kurs_KursId" });
            DropIndex("dbo.ApplicationUserKurs", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Kurset", "InstruktoriId");
            DropTable("dbo.ApplicationUserKurs");
            CreateIndex("dbo.AspNetUsers", "Kurs_KursId");
            CreateIndex("dbo.AspNetUsers", "DrejtonKursId");
            CreateIndex("dbo.Kurset", "Instruktori_Id");
            CreateIndex("dbo.Kurset", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Kurs_KursId", "dbo.Kurset", "KursId");
            AddForeignKey("dbo.Kurset", "Instruktori_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Kurset", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "DrejtonKursId", "dbo.Kurset", "KursId", cascadeDelete: true);
        }
    }
}
