namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoriId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Komentet", name: "Autori_Id", newName: "AutoriId");
            RenameIndex(table: "dbo.Komentet", name: "IX_Autori_Id", newName: "IX_AutoriId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Komentet", name: "IX_AutoriId", newName: "IX_Autori_Id");
            RenameColumn(table: "dbo.Komentet", name: "AutoriId", newName: "Autori_Id");
        }
    }
}
