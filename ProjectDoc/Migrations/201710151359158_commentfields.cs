namespace ProjectDoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentfields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "Camp_ID", "dbo.Camp");
            DropIndex("dbo.Comment", new[] { "Camp_ID" });
            RenameColumn(table: "dbo.Comment", name: "Camp_ID", newName: "campID");
            AlterColumn("dbo.Comment", "campID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comment", "campID");
            AddForeignKey("dbo.Comment", "campID", "dbo.Camp", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "campID", "dbo.Camp");
            DropIndex("dbo.Comment", new[] { "campID" });
            AlterColumn("dbo.Comment", "campID", c => c.Int());
            RenameColumn(table: "dbo.Comment", name: "campID", newName: "Camp_ID");
            CreateIndex("dbo.Comment", "Camp_ID");
            AddForeignKey("dbo.Comment", "Camp_ID", "dbo.Camp", "ID");
        }
    }
}
