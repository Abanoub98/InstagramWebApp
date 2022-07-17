using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class HomeViews
    {
        //User & Suggestion section

        public User user { get; set; }  
        public List<User> Users { get; set; }

        //Post section
        public List<Post> Posts { get; set; }
        public List<PostFile> PostFiles { get; set; }
        public List<PostReact> PostReacts { get; set; }
        public List<Comment> Comments { get; set; }

        public string ActiveUserID { get; set; }

        [Required]
        [StringLength(400)]
        public string CommentText { get; set; }

        //Story section
        public List<Story> Stories { get; set; }
        public List<StoryFile> StoryFiles { get; set; }
        public List<StoryView> StoryViews { get; set; }

        //Nav-Notification section
        public List<ReactNotification> ReactNotifications { get; set; }

        public DateTime DateTimeAgo { get; set; }  


    }
}