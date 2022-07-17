using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class ReactNotification :BaseModel
    {
        [Required]
        public string FromUserID { get; set; }
        [Required]
        public string PostID { get; set; }
        [Required]
        public string PostOwnerID { get; set; }
        [Required]
        public string Reaction { get; set; }
        public string CommentID { get; set; }
        public Post Post { get; set; }
        public virtual User FromUser { get; set; }
        public User PostOwner { get; set; }


    }
}