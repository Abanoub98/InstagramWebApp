using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class UserStory
    {
        public Story Story { get; set; }
        public string ActiveUserId;
    }
}