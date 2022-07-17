using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class StoryView : BaseModel
    {
        [Required]
        public string StoryID { get; set; }
        [Required]
        public string ViewerID { get; set; }

        public User Viewer { get; set; }
        public Story Story { get; set; }
    }
}