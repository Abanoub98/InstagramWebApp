using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class Post : BaseModel
    {

        [StringLength(255)]
        [DisplayName("Write a caption..")]
        public string Caption { get; set; } = string.Empty;
        public int NoOfLikes { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }
        [Required]
        public string CreatorID { get; set; } = string.Empty;
        public virtual IList<PostReact> PostReacts { get; set; } 
        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<PostFile> PostFiles { get; set; } = new List<PostFile>();
        public virtual User Creator { get; set; }
        public virtual IList<ReactNotification>  Notifications { get; set; }

    }
}