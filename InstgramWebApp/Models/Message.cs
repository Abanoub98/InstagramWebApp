using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class Message :BaseModel
    {
        [Required]
        public string FromUserID { get; set; }
        [Required]
        public string ToUserID { get; set; }
        [Required]
        public string MessageText { get; set; }
        [Required]
        public DateTime SentAt { get; set; }

        public User FromUser { get; set; }
        public User ToUser { get; set; }
    }
}