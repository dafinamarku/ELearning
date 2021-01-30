namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeksioniId_ForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materialet", "Seksioni_Id", "dbo.KursNivelTip");
            DropIndex("dbo.Materialet", new[] { "Seksioni_Id" });
            RenameColumn(table: "dbo.Materialet", name: "Seksioni_Id", newName: "SeksioniId");
            AlterColumn("dbo.Materialet", "SeksioniId", c => c.Int(nullable: false));
            CreateIndex("dbo.Materialet", "SeksioniId");
            AddForeignKey("dbo.Materialet", "SeksioniId", "dbo.KursNivelTip", "Id", cascadeDelete: true);
            DropColumn("dbo.Materialet", "SeksionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materialet", "SeksionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Materialet", "SeksioniId", "dbo.KursNivelTip");
            DropIndex("dbo.Materialet", new[] { "SeksioniId" });
            AlterColumn("dbo.Materialet", "SeksioniId", c => c.Int());
            RenameColumn(table: "dbo.Materialet", name: "SeksioniId", newName: "Seksioni_Id");
            CreateIndex("dbo.Materialet", "Seksioni_Id");
            AddForeignKey("dbo.Materialet", "Seksioni_Id", "dbo.KursNivelTip", "Id");
        }
    }
}
