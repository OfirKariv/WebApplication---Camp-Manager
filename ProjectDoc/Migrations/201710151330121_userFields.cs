namespace ProjectDoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Player", "ficici", c => c.Boolean(nullable: false));
            AddColumn("dbo.Player", "topChef", c => c.Boolean(nullable: false));
            AddColumn("dbo.Player", "lastStop", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Player", "lastStop");
            DropColumn("dbo.Player", "topChef");
            DropColumn("dbo.Player", "ficici");
        }
    }
}
