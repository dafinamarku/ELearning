namespace E_learning.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaterialValidationConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Materialet", "Titulli", c => c.String(nullable: false));
            AlterColumn("dbo.Materialet", "Pershkrimi", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Materialet", "Pershkrimi", c => c.String());
            AlterColumn("dbo.Materialet", "Titulli", c => c.String());
        }
    }
}
