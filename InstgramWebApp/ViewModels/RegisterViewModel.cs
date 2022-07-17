using InstgramWebApp.Models;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class RegisterViewModel
    {
        public User User { get; set; }

        [Required]
        public string Password { get; set; }

  
        public HttpPostedFileBase ImgFile { get; set; }
    }
}