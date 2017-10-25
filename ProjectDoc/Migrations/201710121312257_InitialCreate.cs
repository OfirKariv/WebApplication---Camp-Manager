namespace ProjectDoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Camp",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        NumOfPlayers = c.Int(nullable: false),
                        CamptDate = c.DateTime(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CommentDate = c.DateTime(nullable: false),
                        CommentText = c.String(),
                        Writer_ID = c.Int(),
                        Camp_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Player", t => t.Writer_ID)
                .ForeignKey("dbo.Camp", t => t.Camp_ID)
                .Index(t => t.Writer_ID)
                .Index(t => t.Camp_ID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        Team = c.String(),
                        Position = c.String(),
                        Goals = c.String(),
                        Assists = c.String(),
                        Interceptions = c.String(),
                        Camp_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Camp", t => t.Camp_ID)
                .Index(t => t.Camp_ID);
            
            CreateTable(
                "dbo.Stats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CampID = c.Int(nullable: false),
                        PlayerID = c.Int(nullable: false),
                        PlayerGoals = c.Int(nullable: false),
                        PlayerAssists = c.Int(nullable: false),
                        PlayerInter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Player", "Camp_ID", "dbo.Camp");
            DropForeignKey("dbo.Comment", "Camp_ID", "dbo.Camp");
            DropForeignKey("dbo.Comment", "Writer_ID", "dbo.Player");
            DropIndex("dbo.Player", new[] { "Camp_ID" });
            DropIndex("dbo.Comment", new[] { "Camp_ID" });
            DropIndex("dbo.Comment", new[] { "Writer_ID" });
            DropTable("dbo.Stats");
            DropTable("dbo.Player");
            DropTable("dbo.Comment");
            DropTable("dbo.Camp");
            DropTable("dbo.Admin");
        }
    }
}
