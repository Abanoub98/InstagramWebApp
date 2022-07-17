namespace InstgramWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCommentIDinNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReactNotifications", "CommentID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReactNotifications", "CommentID");
        }
    }
}
