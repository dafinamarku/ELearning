namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kurset",
                c => new
                    {
                        KursId = c.Int(nullable: false, identity: true),
                        Emri = c.String(),
                    })
                .PrimaryKey(t => t.KursId);
            
            CreateTable(
                "dbo.Materialet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulli = c.String(),
                        Pershkrimi = c.String(),
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
            
            CreateTable(
                "dbo.Nivelet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Emri = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tipet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Emri = c.String(),
                        Mbiemri = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserKurs", "Kurs_KursId", "dbo.Kurset");
            DropForeignKey("dbo.ApplicationUserKurs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Materialet", "Tipi_Id", "dbo.Tipet");
            DropForeignKey("dbo.Materialet", "Niveli_Id", "dbo.Nivelet");
            DropForeignKey("dbo.Materialet", "Kursi_KursId", "dbo.Kurset");
            DropIndex("dbo.ApplicationUserKurs", new[] { "Kurs_KursId" });
            DropIndex("dbo.ApplicationUserKurs", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Materialet", new[] { "Tipi_Id" });
            DropIndex("dbo.Materialet", new[] { "Niveli_Id" });
            DropIndex("dbo.Materialet", new[] { "Kursi_KursId" });
            DropTable("dbo.ApplicationUserKurs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tipet");
            DropTable("dbo.Nivelet");
            DropTable("dbo.Materialet");
            DropTable("dbo.Kurset");
        }
    }
}
