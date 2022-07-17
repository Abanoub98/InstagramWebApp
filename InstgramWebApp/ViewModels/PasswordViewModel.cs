using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class PasswordViewModel
    {
        [DisplayName("Current Password")]
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [DisplayName("New Password")]
        [Required]
        public string NewPassword { get; set; } = string.Empty;

        [DisplayName("Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Confirm password doesn't match, Try again !")]
        [Required]
        public string RepeatNewPassword { get; set; } = string.Empty;
    }
}