namespace ProjectDoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endstartDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Camp", "sDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Camp", "eDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Camp", "CamptDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Camp", "CamptDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Camp", "eDate");
            DropColumn("dbo.Camp", "sDate");
        }
    }
}
