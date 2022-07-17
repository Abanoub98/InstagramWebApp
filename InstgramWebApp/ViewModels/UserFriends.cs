using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class UserFriends
    {
        public string ActiveUserId { get; set; }
        public User User { get; set; }
    }
}