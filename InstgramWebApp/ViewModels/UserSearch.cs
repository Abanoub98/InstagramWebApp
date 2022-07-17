using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class UserSearch
    {
        public User activeUser { get; set; } 
        public List<User> users { get; set; }
    }
}