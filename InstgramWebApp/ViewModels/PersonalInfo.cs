using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.ViewModels
{
    public class PersonalInfo
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [DisplayName("Mobile Number")]
        public string Mobile { get; set; }
        public string Country { get; set; }
       

        [DisplayName("Birthday")]
        public DateTime BirthDate { get; set; }
    }
}