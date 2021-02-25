namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KursNivelTipFKNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KursNivelTip", "KursiId", "dbo.Kurset");
            DropForeignKey("dbo.KursNivelTip", "NiveliId", "dbo.Nivelet");
            DropForeignKey("dbo.KursNivelTip", "TipiId", "dbo.Tipet");
            DropIndex("dbo.KursNivelTip", new[] { "KursiId" });
            DropIndex("dbo.KursNivelTip", new[] { "NiveliId" });
            DropIndex("dbo.KursNivelTip", new[] { "TipiId" });
            AlterColumn("dbo.KursNivelTip", "KursiId", c => c.Int());
            AlterColumn("dbo.KursNivelTip", "NiveliId", c => c.Int());
            AlterColumn("dbo.KursNivelTip", "TipiId", c => c.Int());
            CreateIndex("dbo.KursNivelTip", "KursiId");
            CreateIndex("dbo.KursNivelTip", "NiveliId");
            CreateIndex("dbo.KursNivelTip", "TipiId");
            AddForeignKey("dbo.KursNivelTip", "KursiId", "dbo.Kurset", "KursId");
            AddForeignKey("dbo.KursNivelTip", "NiveliId", "dbo.Nivelet", "Id");
            AddForeignKey("dbo.KursNivelTip", "TipiId", "dbo.Tipet", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KursNivelTip", "TipiId", "dbo.Tipet");
            DropForeignKey("dbo.KursNivelTip", "NiveliId", "dbo.Nivelet");
            DropForeignKey("dbo.KursNivelTip", "KursiId", "dbo.Kurset");
            DropIndex("dbo.KursNivelTip", new[] { "TipiId" });
            DropIndex("dbo.KursNivelTip", new[] { "NiveliId" });
            DropIndex("dbo.KursNivelTip", new[] { "KursiId" });
            AlterColumn("dbo.KursNivelTip", "TipiId", c => c.Int(nullable: false));
            AlterColumn("dbo.KursNivelTip", "NiveliId", c => c.Int(nullable: false));
            AlterColumn("dbo.KursNivelTip", "KursiId", c => c.Int(nullable: false));
            CreateIndex("dbo.KursNivelTip", "TipiId");
            CreateIndex("dbo.KursNivelTip", "NiveliId");
            CreateIndex("dbo.KursNivelTip", "KursiId");
            AddForeignKey("dbo.KursNivelTip", "TipiId", "dbo.Tipet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KursNivelTip", "NiveliId", "dbo.Nivelet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KursNivelTip", "KursiId", "dbo.Kurset", "KursId", cascadeDelete: true);
        }
    }
}
