namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KursNivelTipFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KursNivelTip", "Kursi_KursId", "dbo.Kurset");
            DropForeignKey("dbo.KursNivelTip", "Niveli_Id", "dbo.Nivelet");
            DropForeignKey("dbo.KursNivelTip", "Tipi_Id", "dbo.Tipet");
            DropIndex("dbo.KursNivelTip", new[] { "Kursi_KursId" });
            DropIndex("dbo.KursNivelTip", new[] { "Niveli_Id" });
            DropIndex("dbo.KursNivelTip", new[] { "Tipi_Id" });
            RenameColumn(table: "dbo.KursNivelTip", name: "Kursi_KursId", newName: "KursiId");
            RenameColumn(table: "dbo.KursNivelTip", name: "Niveli_Id", newName: "NiveliId");
            RenameColumn(table: "dbo.KursNivelTip", name: "Tipi_Id", newName: "TipiId");
            AlterColumn("dbo.Kurset", "Emri", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.KursNivelTip", "KursiId", c => c.Int(nullable: false));
            AlterColumn("dbo.KursNivelTip", "NiveliId", c => c.Int(nullable: false));
            AlterColumn("dbo.KursNivelTip", "TipiId", c => c.Int(nullable: false));
            CreateIndex("dbo.KursNivelTip", "KursiId");
            CreateIndex("dbo.KursNivelTip", "NiveliId");
            CreateIndex("dbo.KursNivelTip", "TipiId");
            AddForeignKey("dbo.KursNivelTip", "KursiId", "dbo.Kurset", "KursId", cascadeDelete: true);
            AddForeignKey("dbo.KursNivelTip", "NiveliId", "dbo.Nivelet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KursNivelTip", "TipiId", "dbo.Tipet", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KursNivelTip", "TipiId", "dbo.Tipet");
            DropForeignKey("dbo.KursNivelTip", "NiveliId", "dbo.Nivelet");
            DropForeignKey("dbo.KursNivelTip", "KursiId", "dbo.Kurset");
            DropIndex("dbo.KursNivelTip", new[] { "TipiId" });
            DropIndex("dbo.KursNivelTip", new[] { "NiveliId" });
            DropIndex("dbo.KursNivelTip", new[] { "KursiId" });
            AlterColumn("dbo.KursNivelTip", "TipiId", c => c.Int());
            AlterColumn("dbo.KursNivelTip", "NiveliId", c => c.Int());
            AlterColumn("dbo.KursNivelTip", "KursiId", c => c.Int());
            AlterColumn("dbo.Kurset", "Emri", c => c.String());
            RenameColumn(table: "dbo.KursNivelTip", name: "TipiId", newName: "Tipi_Id");
            RenameColumn(table: "dbo.KursNivelTip", name: "NiveliId", newName: "Niveli_Id");
            RenameColumn(table: "dbo.KursNivelTip", name: "KursiId", newName: "Kursi_KursId");
            CreateIndex("dbo.KursNivelTip", "Tipi_Id");
            CreateIndex("dbo.KursNivelTip", "Niveli_Id");
            CreateIndex("dbo.KursNivelTip", "Kursi_KursId");
            AddForeignKey("dbo.KursNivelTip", "Tipi_Id", "dbo.Tipet", "Id");
            AddForeignKey("dbo.KursNivelTip", "Niveli_Id", "dbo.Nivelet", "Id");
            AddForeignKey("dbo.KursNivelTip", "Kursi_KursId", "dbo.Kurset", "KursId");
        }
    }
}
