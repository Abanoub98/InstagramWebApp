using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace InstgramWebApp.Models
{
    public class User : BaseModel
    {
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Bio")]
        [StringLength(255)]
        public string Bio { get; set; }

        [Required]
        [Phone]
        [DisplayName("Mobile Number")]
        public string Mobile { get; set; }
        public string Country { get; set; }
        [DisplayName("Profile Picture")]
        public string ImgPath { get; set; }

        [DisplayName("Birthday")]
        public DateTime BirthDate { get; set; }
        public IList<Post> Posts { get; set; }
        public IList<Story> Stories { get; set; }
        public virtual IList<FriendShip> FriendShips { get; set; }
        public virtual IList<FollowRequest> FollowRequests { get; set; }
        public IList<Message> Messages { get; set; }
        public virtual IList<ReactNotification> ReactNotifications { get; set; }
        public IList<StoryView> StoryViews { get; set; }
        public IList<PostReact> PostReacts { get; set; }
        public IList<Comment> Comments { get; set; }


    }
}