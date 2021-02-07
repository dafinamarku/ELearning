namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Komentet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Komentet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Teksti = c.String(nullable: false),
                        DataEKrijimit = c.DateTime(nullable: false),
                        Autori_Id = c.String(maxLength: 128),
                        Koment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Autori_Id)
                .ForeignKey("dbo.Komentet", t => t.Koment_Id)
                .Index(t => t.Autori_Id)
                .Index(t => t.Koment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Komentet", "Koment_Id", "dbo.Komentet");
            DropForeignKey("dbo.Komentet", "Autori_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Komentet", new[] { "Koment_Id" });
            DropIndex("dbo.Komentet", new[] { "Autori_Id" });
            DropTable("dbo.Komentet");
        }
    }
}
