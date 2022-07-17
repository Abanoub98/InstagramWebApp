using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using InstgramWebApp.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InstgramWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>

    {
        public DbSet <User> InstaUsers { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryFile> StoryFiles { get; set; }
        public DbSet<FollowRequest> FollowRequests { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReact> PostReacts { get; set; }
        public DbSet<PostFile> PostFiles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<StoryView> StoryViews { get; set; }
        public DbSet<ReactNotification> ReactNotifications { get; set; }
        public DbSet<FriendShip> FriendShips { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureSchema();
        }
    }
}