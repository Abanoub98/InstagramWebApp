using InstgramWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class SettingViewModel
    {
        public User user { get; set; }
        //public User profileInfo { get; set; }
        //public PersonalInfo personalInfo { get; set; }

        public HttpPostedFileBase ImgFile { get; set; }
        public PasswordViewModel pass { get; set; }
    }
}