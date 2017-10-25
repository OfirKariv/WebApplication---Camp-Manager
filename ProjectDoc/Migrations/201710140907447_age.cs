namespace ProjectDoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class age : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Player", "Camp_ID", "dbo.Camp");
            DropIndex("dbo.Player", new[] { "Camp_ID" });
            CreateTable(
                "dbo.PlayerCamp",
                c => new
                    {
                        Player_ID = c.Int(nullable: false),
                        Camp_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_ID, t.Camp_ID })
                .ForeignKey("dbo.Player", t => t.Player_ID, cascadeDelete: true)
                .ForeignKey("dbo.Camp", t => t.Camp_ID, cascadeDelete: true)
                .Index(t => t.Player_ID)
                .Index(t => t.Camp_ID);
            
            AddColumn("dbo.Player", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Player", "GoalsSum", c => c.Int(nullable: false));
            AddColumn("dbo.Player", "AssistsSum", c => c.Int(nullable: false));
            AddColumn("dbo.Player", "InterceptionsSum", c => c.Int(nullable: false));
            CreateIndex("dbo.Stats", "PlayerID");
            AddForeignKey("dbo.Stats", "PlayerID", "dbo.Player", "ID", cascadeDelete: true);
            DropColumn("dbo.Player", "Goals");
            DropColumn("dbo.Player", "Assists");
            DropColumn("dbo.Player", "Interceptions");
            DropColumn("dbo.Player", "Camp_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Player", "Camp_ID", c => c.Int());
            AddColumn("dbo.Player", "Interceptions", c => c.String());
            AddColumn("dbo.Player", "Assists", c => c.String());
            AddColumn("dbo.Player", "Goals", c => c.String());
            DropForeignKey("dbo.Stats", "PlayerID", "dbo.Player");
            DropForeignKey("dbo.PlayerCamp", "Camp_ID", "dbo.Camp");
            DropForeignKey("dbo.PlayerCamp", "Player_ID", "dbo.Player");
            DropIndex("dbo.PlayerCamp", new[] { "Camp_ID" });
            DropIndex("dbo.PlayerCamp", new[] { "Player_ID" });
            DropIndex("dbo.Stats", new[] { "PlayerID" });
            DropColumn("dbo.Player", "InterceptionsSum");
            DropColumn("dbo.Player", "AssistsSum");
            DropColumn("dbo.Player", "GoalsSum");
            DropColumn("dbo.Player", "Age");
            DropTable("dbo.PlayerCamp");
            CreateIndex("dbo.Player", "Camp_ID");
            AddForeignKey("dbo.Player", "Camp_ID", "dbo.Camp", "ID");
        }
    }
}
