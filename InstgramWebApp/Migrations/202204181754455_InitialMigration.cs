namespace InstgramWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PostID = c.String(nullable: false, maxLength: 128),
                        CreatorID = c.String(nullable: false, maxLength: 128),
                        CommentText = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Username = c.String(nullable: false),
                        FName = c.String(nullable: false),
                        LName = c.String(),
                        Email = c.String(nullable: false),
                        Bio = c.String(),
                        Mobile = c.String(nullable: false),
                        Country = c.String(),
                        ImgPath = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FollowRequests",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FromUserID = c.String(nullable: false, maxLength: 128),
                        ToUserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FromUserID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ToUserID)
                .Index(t => t.FromUserID)
                .Index(t => t.ToUserID);
            
            CreateTable(
                "dbo.FriendShips",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        User1ID = c.String(nullable: false, maxLength: 128),
                        User2ID = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User2ID)
                .Index(t => t.User1ID)
                .Index(t => t.User2ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FromUserID = c.String(nullable: false, maxLength: 128),
                        ToUserID = c.String(nullable: false, maxLength: 128),
                        MessageText = c.String(nullable: false),
                        SentAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FromUserID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ToUserID)
                .Index(t => t.FromUserID)
                .Index(t => t.ToUserID);
            
            CreateTable(
                "dbo.PostReacts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PostID = c.String(nullable: false, maxLength: 128),
                        ReactorID = c.String(nullable: false, maxLength: 128),
                        Reaction = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ReactorID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.ReactorID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Caption = c.String(maxLength: 255),
                        NoOfLikes = c.Int(nullable: false),
                        CreatorID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorID)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.ReactNotifications",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FromUserID = c.String(nullable: false, maxLength: 128),
                        PostID = c.String(nullable: false, maxLength: 128),
                        PostOwnerID = c.String(nullable: false, maxLength: 128),
                        Reaction = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FromUserID, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.PostOwnerID)
                .Index(t => t.FromUserID)
                .Index(t => t.PostID)
                .Index(t => t.PostOwnerID);
            
            CreateTable(
                "dbo.PostFiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PostID = c.String(nullable: false, maxLength: 128),
                        FilePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                        ImgPath = c.String(nullable: false),
                        CreatorID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: true)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.StoryViews",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StoryID = c.String(nullable: false, maxLength: 128),
                        ViewerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stories", t => t.StoryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ViewerID)
                .Index(t => t.StoryID)
                .Index(t => t.ViewerID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.StoryViews", "ViewerID", "dbo.Users");
            DropForeignKey("dbo.Stories", "CreatorID", "dbo.Users");
            DropForeignKey("dbo.StoryViews", "StoryID", "dbo.Stories");
            DropForeignKey("dbo.ReactNotifications", "PostOwnerID", "dbo.Users");
            DropForeignKey("dbo.Posts", "CreatorID", "dbo.Users");
            DropForeignKey("dbo.PostReacts", "ReactorID", "dbo.Users");
            DropForeignKey("dbo.PostReacts", "PostID", "dbo.Posts");
            DropForeignKey("dbo.PostFiles", "PostID", "dbo.Posts");
            DropForeignKey("dbo.ReactNotifications", "PostID", "dbo.Posts");
            DropForeignKey("dbo.ReactNotifications", "FromUserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Messages", "ToUserID", "dbo.Users");
            DropForeignKey("dbo.Messages", "FromUserID", "dbo.Users");
            DropForeignKey("dbo.FriendShips", "User2ID", "dbo.Users");
            DropForeignKey("dbo.FriendShips", "User1ID", "dbo.Users");
            DropForeignKey("dbo.FollowRequests", "ToUserID", "dbo.Users");
            DropForeignKey("dbo.FollowRequests", "FromUserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "CreatorID", "dbo.Users");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.StoryViews", new[] { "ViewerID" });
            DropIndex("dbo.StoryViews", new[] { "StoryID" });
            DropIndex("dbo.Stories", new[] { "CreatorID" });
            DropIndex("dbo.PostFiles", new[] { "PostID" });
            DropIndex("dbo.ReactNotifications", new[] { "PostOwnerID" });
            DropIndex("dbo.ReactNotifications", new[] { "PostID" });
            DropIndex("dbo.ReactNotifications", new[] { "FromUserID" });
            DropIndex("dbo.Posts", new[] { "CreatorID" });
            DropIndex("dbo.PostReacts", new[] { "ReactorID" });
            DropIndex("dbo.PostReacts", new[] { "PostID" });
            DropIndex("dbo.Messages", new[] { "ToUserID" });
            DropIndex("dbo.Messages", new[] { "FromUserID" });
            DropIndex("dbo.FriendShips", new[] { "User2ID" });
            DropIndex("dbo.FriendShips", new[] { "User1ID" });
            DropIndex("dbo.FollowRequests", new[] { "ToUserID" });
            DropIndex("dbo.FollowRequests", new[] { "FromUserID" });
            DropIndex("dbo.Comments", new[] { "CreatorID" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.StoryViews");
            DropTable("dbo.Stories");
            DropTable("dbo.PostFiles");
            DropTable("dbo.ReactNotifications");
            DropTable("dbo.Posts");
            DropTable("dbo.PostReacts");
            DropTable("dbo.Messages");
            DropTable("dbo.FriendShips");
            DropTable("dbo.FollowRequests");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}
