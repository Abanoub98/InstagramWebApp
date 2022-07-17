using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InstgramWebApp.Models
{
    public class Story : BaseModel
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatorID { get; set; }
        public virtual User Creator { get; set; }

        public virtual IList<StoryFile> StoryFiles { get; set; } = new List<StoryFile>();
        public IList<StoryView> StoryViews { get; set; }

   
    }
}