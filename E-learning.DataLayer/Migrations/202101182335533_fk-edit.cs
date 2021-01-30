namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkedit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NivelKurs",
                c => new
                    {
                        Nivel_Id = c.Int(nullable: false),
                        Kurs_KursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Nivel_Id, t.Kurs_KursId })
                .ForeignKey("dbo.Nivelet", t => t.Nivel_Id, cascadeDelete: true)
                .ForeignKey("dbo.Kurset", t => t.Kurs_KursId, cascadeDelete: true)
                .Index(t => t.Nivel_Id)
                .Index(t => t.Kurs_KursId);
            
            CreateTable(
                "dbo.TipKurs",
                c => new
                    {
                        Tip_Id = c.Int(nullable: false),
                        Kurs_KursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tip_Id, t.Kurs_KursId })
                .ForeignKey("dbo.Tipet", t => t.Tip_Id, cascadeDelete: true)
                .ForeignKey("dbo.Kurset", t => t.Kurs_KursId, cascadeDelete: true)
                .Index(t => t.Tip_Id)
                .Index(t => t.Kurs_KursId);
            
            CreateTable(
                "dbo.TipNivels",
                c => new
                    {
                        Tip_Id = c.Int(nullable: false),
                        Nivel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tip_Id, t.Nivel_Id })
                .ForeignKey("dbo.Tipet", t => t.Tip_Id, cascadeDelete: true)
                .ForeignKey("dbo.Nivelet", t => t.Nivel_Id, cascadeDelete: true)
                .Index(t => t.Tip_Id)
                .Index(t => t.Nivel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipNivels", "Nivel_Id", "dbo.Nivelet");
            DropForeignKey("dbo.TipNivels", "Tip_Id", "dbo.Tipet");
            DropForeignKey("dbo.TipKurs", "Kurs_KursId", "dbo.Kurset");
            DropForeignKey("dbo.TipKurs", "Tip_Id", "dbo.Tipet");
            DropForeignKey("dbo.NivelKurs", "Kurs_KursId", "dbo.Kurset");
            DropForeignKey("dbo.NivelKurs", "Nivel_Id", "dbo.Nivelet");
            DropIndex("dbo.TipNivels", new[] { "Nivel_Id" });
            DropIndex("dbo.TipNivels", new[] { "Tip_Id" });
            DropIndex("dbo.TipKurs", new[] { "Kurs_KursId" });
            DropIndex("dbo.TipKurs", new[] { "Tip_Id" });
            DropIndex("dbo.NivelKurs", new[] { "Kurs_KursId" });
            DropIndex("dbo.NivelKurs", new[] { "Nivel_Id" });
            DropTable("dbo.TipNivels");
            DropTable("dbo.TipKurs");
            DropTable("dbo.NivelKurs");
        }
    }
}
