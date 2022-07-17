using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class PostFile :BaseModel
    {

        [Required]
        public string PostID { get; set; }
        [Required]
        public string FilePath { get; set; }

        public Post Post { get; set; }
    }
}