namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeksionId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materialet", "SeksionId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materialet", "SeksionId");
        }
    }
}
