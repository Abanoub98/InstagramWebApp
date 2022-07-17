using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class UserRequests
    {
        public User activeUser { get; set; }
        public List<User> Users { get; set; }
        public List<FollowRequest> followRequests { get; set; }
    }
}