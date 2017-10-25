namespace ProjectDoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class top : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Camp", "Location", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Camp", "Location", c => c.String());
        }
    }
}
