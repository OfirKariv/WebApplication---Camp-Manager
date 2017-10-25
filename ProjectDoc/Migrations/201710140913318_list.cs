namespace ProjectDoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class list : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerCamp", "Player_ID", "dbo.Player");
            DropForeignKey("dbo.PlayerCamp", "Camp_ID", "dbo.Camp");
            DropIndex("dbo.PlayerCamp", new[] { "Player_ID" });
            DropIndex("dbo.PlayerCamp", new[] { "Camp_ID" });
            AddColumn("dbo.Camp", "Player_ID", c => c.Int());
            AddColumn("dbo.Camp", "Player_ID1", c => c.Int());
            AddColumn("dbo.Player", "Camp_ID", c => c.Int());
            CreateIndex("dbo.Camp", "Player_ID");
            CreateIndex("dbo.Camp", "Player_ID1");
            CreateIndex("dbo.Player", "Camp_ID");
            AddForeignKey("dbo.Camp", "Player_ID", "dbo.Player", "ID");
            AddForeignKey("dbo.Camp", "Player_ID1", "dbo.Player", "ID");
            AddForeignKey("dbo.Player", "Camp_ID", "dbo.Camp", "ID");
            DropTable("dbo.PlayerCamp");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlayerCamp",
                c => new
                    {
                        Player_ID = c.Int(nullable: false),
                        Camp_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_ID, t.Camp_ID });
            
            DropForeignKey("dbo.Player", "Camp_ID", "dbo.Camp");
            DropForeignKey("dbo.Camp", "Player_ID1", "dbo.Player");
            DropForeignKey("dbo.Camp", "Player_ID", "dbo.Player");
            DropIndex("dbo.Player", new[] { "Camp_ID" });
            DropIndex("dbo.Camp", new[] { "Player_ID1" });
            DropIndex("dbo.Camp", new[] { "Player_ID" });
            DropColumn("dbo.Player", "Camp_ID");
            DropColumn("dbo.Camp", "Player_ID1");
            DropColumn("dbo.Camp", "Player_ID");
            CreateIndex("dbo.PlayerCamp", "Camp_ID");
            CreateIndex("dbo.PlayerCamp", "Player_ID");
            AddForeignKey("dbo.PlayerCamp", "Camp_ID", "dbo.Camp", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PlayerCamp", "Player_ID", "dbo.Player", "ID", cascadeDelete: true);
        }
    }
}
