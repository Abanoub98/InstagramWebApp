using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class PostReact :BaseModel
    {
        [Required]
        public string PostID { get; set; }
        [Required]
        public string ReactorID { get; set; }
        [Required]
        public string Reaction { get; set; }

        public Post Post { get; set; }
        public User Reactor { get; set; }
    }
}