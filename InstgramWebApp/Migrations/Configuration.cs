namespace InstgramWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using InstgramWebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<InstgramWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InstgramWebApp.Models.ApplicationDbContext context)
        {

           


        //    context.InstaUsers.AddOrUpdate(u => u.Id,
        //    new User()
        //    {
        //        Id = 1.ToString(),
        //        Bio = "cs student",
        //        FName = "Abanoub",
        //        LName = "Rafaat",
        //        Country ="Egypt",
        //        BirthDate = DateTime.Now,
        //        Email = "bebo_myemail@yahoo.com",
        //        Mobile = 01282692682.ToString(),
        //        Username ="bebo98",
        //        ImgPath ="/Content/img/profile-pic.jpeg"
        //    },
        //     new User()
        //     {
        //         Id = 2.ToString(),
        //         Bio = "python student",
        //         FName = "Andrew",
        //         LName = "Emad",
        //         Country = "Egypt",
        //         BirthDate = DateTime.Now,
        //         Email = "andrew_myemail@yahoo.com",
        //         Mobile = 01221692682.ToString(),
        //         Username = "andrewelgen",
        //         ImgPath = "/Content/img/A.png"
        //     });



        //    context.Stories.AddOrUpdate(s => s.Id,
        //       new Story()
        //       {
        //           Id = 1.ToString(),
        //           CreatorID = 1.ToString(),
        //           ImgPath = "/Content/img/cover 5.png",
        //           CreatedAt = DateTime.Now,
        //       },
        //       new Story()
        //       {
        //           Id = 2.ToString(),
        //           CreatorID = 1.ToString(),
        //           ImgPath = "/Content/img/cover 6.png",
        //           CreatedAt = DateTime.Now,
        //       });



        //    context.Posts.AddOrUpdate(p => p.Id,
        //    new Post()
        //    {
        //        Id = 1.ToString(),
        //        Caption = "my FIRST post",
        //        NoOfLikes = 10,
        //        CreatorID = 1.ToString(),

        //    },
        //    new Post()
        //    {
        //        Id = 2.ToString(),
        //        Caption = "Best Vacation time",
        //        NoOfLikes = 35,
        //        CreatorID = 2.ToString(),

        //    },
        //    new Post()
        //    {
        //        Id = 3.ToString(),
        //        Caption = "my second post",
        //        NoOfLikes = 15,
        //        CreatorID = 1.ToString(),

        //    });

        //    context.PostFiles.AddOrUpdate(pf => pf.Id,
        //       new PostFile()
        //       {
        //           Id = 1.ToString(),
        //           PostID = 1.ToString(),
        //           FilePath = "/Content/img/cover 1.png"

        //       },
        //       new PostFile()
        //       {
        //           Id = 2.ToString(),
        //           PostID = 1.ToString(),
        //           FilePath = "/Content/img/cover 2.png"

        //       },
        //        new PostFile()
        //        {
        //            Id = 3.ToString(),
        //            PostID = 2.ToString(),
        //            FilePath = "/Content/img/cover 3.png"

        //        }, 
        //        new PostFile()
        //        {
        //            Id = 4.ToString(),
        //            PostID = 3.ToString(),
        //            FilePath = "/Content/img/cover 4.png"

        //        });

        //    context.Comments.AddOrUpdate(c => c.Id,
        //    new Comment()
        //    {
        //        PostID = 1.ToString(),
        //        CreatorID = 2.ToString(),
        //         CommentText ="nice post",
        //         Id = 1.ToString(),                 
        //    },
        //    new Comment()
        //    {
        //        PostID = 1.ToString(),
        //        CreatorID = 1.ToString(),
        //        CommentText = "thank you",
        //        Id = 2.ToString(),
        //    },
        //    new Comment()
        //    {
        //        PostID = 2.ToString(),
        //        CreatorID = 2.ToString(),
        //        CommentText = "gamed",
        //        Id = 3.ToString(),
        //    },
        //    new Comment()
        //    {
        //        PostID = 3.ToString(),
        //        CreatorID = 2.ToString(),
        //        CommentText = "7biby",
        //        Id = 4.ToString(),
        //    });
        }
    }
}
