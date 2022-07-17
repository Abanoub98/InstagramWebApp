using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class FollowRequest : BaseModel
    {
        [Required]
        public string FromUserID { get; set; }
        [Required]
        public string ToUserID { get; set; }
        public virtual User FromUser { get; set; }
        public User ToUser { get; set; }
    }
}