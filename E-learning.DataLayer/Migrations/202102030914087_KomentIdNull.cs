namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KomentIdNull : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Komentet", name: "Koment_Id", newName: "KomentId");
            RenameIndex(table: "dbo.Komentet", name: "IX_Koment_Id", newName: "IX_KomentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Komentet", name: "IX_KomentId", newName: "IX_Koment_Id");
            RenameColumn(table: "dbo.Komentet", name: "KomentId", newName: "Koment_Id");
        }
    }
}
