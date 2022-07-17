using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureSchema(this DbModelBuilder modelBuilder)
        {
            //User Relationships (FKs)
            modelBuilder.Entity<User>().HasMany(u => u.FriendShips).WithRequired(fs => fs.User1).HasForeignKey(fs => fs.User1ID).WillCascadeOnDelete();
            modelBuilder.Entity<User>().HasMany(u => u.FriendShips).WithRequired(fs => fs.User2).HasForeignKey(fs => fs.User2ID).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasMany(u => u.FollowRequests).WithRequired(fr => fr.FromUser).HasForeignKey(fr => fr.FromUserID).WillCascadeOnDelete();
            modelBuilder.Entity<User>().HasMany(u => u.FollowRequests).WithRequired(fr => fr.ToUser).HasForeignKey(fr => fr.ToUserID).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasMany(u => u.Posts).WithRequired(p => p.Creator).HasForeignKey(p => p.CreatorID).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(u => u.PostReacts).WithRequired(pr => pr.Reactor).HasForeignKey(pr => pr.ReactorID).WillCascadeOnDelete();
            modelBuilder.Entity<User>().HasMany(u => u.Comments).WithRequired(c => c.Creator).HasForeignKey(pr => pr.CreatorID).WillCascadeOnDelete();

            modelBuilder.Entity<User>().HasMany(u => u.Stories).WithRequired(s => s.Creator).HasForeignKey(s => s.CreatorID).WillCascadeOnDelete();
            modelBuilder.Entity<User>().HasMany(u => u.StoryViews).WithRequired(sv => sv.Viewer).HasForeignKey(sv => sv.ViewerID).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasMany(u => u.Messages).WithRequired(m => m.FromUser).HasForeignKey(m => m.FromUserID).WillCascadeOnDelete();
            modelBuilder.Entity<User>().HasMany(u => u.Messages).WithRequired(m => m.ToUser).HasForeignKey(m => m.ToUserID).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasMany(u => u.ReactNotifications).WithRequired(rn => rn.FromUser).HasForeignKey(rn => rn.FromUserID).WillCascadeOnDelete();
            modelBuilder.Entity<User>().HasMany(u => u.ReactNotifications).WithRequired(m => m.PostOwner).HasForeignKey(m => m.PostOwnerID).WillCascadeOnDelete(false);





            //Post Relationships (FKs)
            modelBuilder.Entity<Post>().HasMany(p => p.Comments).WithRequired(c => c.Post).HasForeignKey(c => c.PostID).WillCascadeOnDelete();
            modelBuilder.Entity<Post>().HasMany(p => p.PostReacts).WithRequired(pr => pr.Post).HasForeignKey(pr => pr.PostID).WillCascadeOnDelete();
            modelBuilder.Entity<Post>().HasMany(p => p.PostFiles).WithRequired(pf => pf.Post).HasForeignKey(pf => pf.PostID).WillCascadeOnDelete();
            modelBuilder.Entity<Post>().HasMany(p => p.Notifications).WithRequired(n => n.Post).HasForeignKey(n => n.PostID).WillCascadeOnDelete();


            //Story Relationships (FKs)
            modelBuilder.Entity<Story>().HasMany(s => s.StoryViews).WithRequired(sv => sv.Story).HasForeignKey(sv => sv.StoryID).WillCascadeOnDelete();
            modelBuilder.Entity<Story>().HasMany(s => s.StoryFiles).WithRequired(sf => sf.Story).HasForeignKey(sf => sf.StoryID).WillCascadeOnDelete();
        }
    }
}