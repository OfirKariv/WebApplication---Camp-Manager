namespace ProjectDoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class writer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "Writer_ID", "dbo.Player");
            DropIndex("dbo.Comment", new[] { "Writer_ID" });
            AddColumn("dbo.Comment", "Writer", c => c.String());
            DropColumn("dbo.Comment", "Writer_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment", "Writer_ID", c => c.Int());
            DropColumn("dbo.Comment", "Writer");
            CreateIndex("dbo.Comment", "Writer_ID");
            AddForeignKey("dbo.Comment", "Writer_ID", "dbo.Player", "ID");
        }
    }
}
