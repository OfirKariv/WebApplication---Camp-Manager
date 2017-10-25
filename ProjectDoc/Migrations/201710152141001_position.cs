namespace ProjectDoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class position : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Player", "Position", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Player", "Position", c => c.String());
        }
    }
}
