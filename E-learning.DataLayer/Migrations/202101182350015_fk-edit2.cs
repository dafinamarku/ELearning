namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkedit2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materialet", "Kursi_KursId", "dbo.Kurset");
            DropForeignKey("dbo.NivelKurs", "Nivel_Id", "dbo.Nivelet");
            DropForeignKey("dbo.NivelKurs", "Kurs_KursId", "dbo.Kurset");
            DropForeignKey("dbo.Materialet", "Niveli_Id", "dbo.Nivelet");
            DropForeignKey("dbo.TipKurs", "Tip_Id", "dbo.Tipet");
            DropForeignKey("dbo.TipKurs", "Kurs_KursId", "dbo.Kurset");
            DropForeignKey("dbo.Materialet", "Tipi_Id", "dbo.Tipet");
            DropForeignKey("dbo.TipNivels", "Tip_Id", "dbo.Tipet");
            DropForeignKey("dbo.TipNivels", "Nivel_Id", "dbo.Nivelet");
            DropIndex("dbo.Materialet", new[] { "Kursi_KursId" });
            DropIndex("dbo.Materialet", new[] { "Niveli_Id" });
            DropIndex("dbo.Materialet", new[] { "Tipi_Id" });
            DropIndex("dbo.NivelKurs", new[] { "Nivel_Id" });
            DropIndex("dbo.NivelKurs", new[] { "Kurs_KursId" });
            DropIndex("dbo.TipKurs", new[] { "Tip_Id" });
            DropIndex("dbo.TipKurs", new[] { "Kurs_KursId" });
            DropIndex("dbo.TipNivels", new[] { "Tip_Id" });
            DropIndex("dbo.TipNivels", new[] { "Nivel_Id" });
            CreateTable(
                "dbo.KursNivelTip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kursi_KursId = c.Int(),
                        Niveli_Id = c.Int(),
                        Tipi_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kurset", t => t.Kursi_KursId)
                .ForeignKey("dbo.Nivelet", t => t.Niveli_Id)
                .ForeignKey("dbo.Tipet", t => t.Tipi_Id)
                .Index(t => t.Kursi_KursId)
                .Index(t => t.Niveli_Id)
                .Index(t => t.Tipi_Id);
            
            AddColumn("dbo.Materialet", "Filename", c => c.String());
            AddColumn("dbo.Materialet", "Seksioni_Id", c => c.Int());
            CreateIndex("dbo.Materialet", "Seksioni_Id");
            AddForeignKey("dbo.Materialet", "Seksioni_Id", "dbo.KursNivelTip", "Id");
            DropColumn("dbo.Materialet", "Kursi_KursId");
            DropColumn("dbo.Materialet", "Niveli_Id");
            DropColumn("dbo.Materialet", "Tipi_Id");
            DropTable("dbo.NivelKurs");
            DropTable("dbo.TipKurs");
            DropTable("dbo.TipNivels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TipNivels",
                c => new
                    {
                        Tip_Id = c.Int(nullable: false),
                        Nivel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tip_Id, t.Nivel_Id });
            
            CreateTable(
                "dbo.TipKurs",
                c => new
                    {
                        Tip_Id = c.Int(nullable: false),
                        Kurs_KursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tip_Id, t.Kurs_KursId });
            
            CreateTable(
                "dbo.NivelKurs",
                c => new
                    {
                        Nivel_Id = c.Int(nullable: false),
                        Kurs_KursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Nivel_Id, t.Kurs_KursId });
            
            AddColumn("dbo.Materialet", "Tipi_Id", c => c.Int());
            AddColumn("dbo.Materialet", "Niveli_Id", c => c.Int());
            AddColumn("dbo.Materialet", "Kursi_KursId", c => c.Int());
            DropForeignKey("dbo.KursNivelTip", "Tipi_Id", "dbo.Tipet");
            DropForeignKey("dbo.KursNivelTip", "Niveli_Id", "dbo.Nivelet");
            DropForeignKey("dbo.Materialet", "Seksioni_Id", "dbo.KursNivelTip");
            DropForeignKey("dbo.KursNivelTip", "Kursi_KursId", "dbo.Kurset");
            DropIndex("dbo.Materialet", new[] { "Seksioni_Id" });
            DropIndex("dbo.KursNivelTip", new[] { "Tipi_Id" });
            DropIndex("dbo.KursNivelTip", new[] { "Niveli_Id" });
            DropIndex("dbo.KursNivelTip", new[] { "Kursi_KursId" });
            DropColumn("dbo.Materialet", "Seksioni_Id");
            DropColumn("dbo.Materialet", "Filename");
            DropTable("dbo.KursNivelTip");
            CreateIndex("dbo.TipNivels", "Nivel_Id");
            CreateIndex("dbo.TipNivels", "Tip_Id");
            CreateIndex("dbo.TipKurs", "Kurs_KursId");
            CreateIndex("dbo.TipKurs", "Tip_Id");
            CreateIndex("dbo.NivelKurs", "Kurs_KursId");
            CreateIndex("dbo.NivelKurs", "Nivel_Id");
            CreateIndex("dbo.Materialet", "Tipi_Id");
            CreateIndex("dbo.Materialet", "Niveli_Id");
            CreateIndex("dbo.Materialet", "Kursi_KursId");
            AddForeignKey("dbo.TipNivels", "Nivel_Id", "dbo.Nivelet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TipNivels", "Tip_Id", "dbo.Tipet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Materialet", "Tipi_Id", "dbo.Tipet", "Id");
            AddForeignKey("dbo.TipKurs", "Kurs_KursId", "dbo.Kurset", "KursId", cascadeDelete: true);
            AddForeignKey("dbo.TipKurs", "Tip_Id", "dbo.Tipet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Materialet", "Niveli_Id", "dbo.Nivelet", "Id");
            AddForeignKey("dbo.NivelKurs", "Kurs_KursId", "dbo.Kurset", "KursId", cascadeDelete: true);
            AddForeignKey("dbo.NivelKurs", "Nivel_Id", "dbo.Nivelet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Materialet", "Kursi_KursId", "dbo.Kurset", "KursId");
        }
    }
}
