namespace InstgramWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeStoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoryFiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StoryID = c.String(nullable: false, maxLength: 128),
                        FilePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stories", t => t.StoryID, cascadeDelete: true)
                .Index(t => t.StoryID);
            
            DropColumn("dbo.Stories", "ImgPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stories", "ImgPath", c => c.String(nullable: false));
            DropForeignKey("dbo.StoryFiles", "StoryID", "dbo.Stories");
            DropIndex("dbo.StoryFiles", new[] { "StoryID" });
            DropTable("dbo.StoryFiles");
        }
    }
}
