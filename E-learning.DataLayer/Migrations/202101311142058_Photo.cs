namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kurset", "Photo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Photo");
            DropColumn("dbo.Kurset", "Photo");
        }
    }
}
