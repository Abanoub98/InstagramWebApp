using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class Comment :BaseModel 
    {
        [Required]
        public string PostID { get; set; }
        [Required]
        public string CreatorID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CommentText{ get; set; }
        public Post Post { get; set; }
        public virtual User Creator { get; set; }
    }
}