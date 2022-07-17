using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class FriendShip :BaseModel
    {
        [Required]
        public string User1ID { get; set; }
        [Required]
        public string User2ID { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }

    

    }
}