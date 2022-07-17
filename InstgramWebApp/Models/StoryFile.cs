using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class StoryFile : BaseModel

    {
        [Required]
        public string StoryID { get; set; }
        [Required]
        public string FilePath { get; set; }

        public Story Story { get; set; }
    
    }
}