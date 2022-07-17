using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class UserPost
    {
        public User User { get; set; }
        public Post Post { get; set; }

        [Required]
        [StringLength(400)]
        public string CommentText { get; set; }
    }
}