using InstgramWebApp.Models;
using InstgramWebApp.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InstgramWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;


        public HomeController()
        {
            _db = new ApplicationDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_db));

        }
        public ActionResult Index()
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
            var user = _db.InstaUsers.Find(identityUser.Id);
            var usersWithMaxPosts = GetUsers(identityUser);
            var posts = GetPosts(user);
            var postReacts = GetPostReacts();
            var postFiles = GetPostFiles();
            var comments = GetComments();
            var stories = GetStories(user);
            HomeViews homeViews = new HomeViews
            {
                user = user,
                Users = usersWithMaxPosts,
                Posts = posts,
                Stories = stories,
                DateTimeAgo = @DateTime.Now,
            };
            homeViews.user.Stories=_db.Stories.Where(st => st.CreatorID == user.Id).ToList();
            homeViews.Users.ForEach(x => x.FriendShips = _db.FriendShips.Where(y => y.User1ID == x.Id || y.User2ID == x.Id).ToList());

            return View(homeViews);
        }

        public PartialViewResult _Header()
        {
            var user = _db.InstaUsers.Find(User.Identity.GetUserId());
            return PartialView(user);
        }
        public ActionResult Reels()
        {
            var postsWithManyLikes=_db.Posts.Where(x=>x.NoOfLikes > 50).Include(x=>x.PostFiles).ToList();
            return View(postsWithManyLikes);
        }

        public ActionResult SearchResult(string searchInput)
        {
            var user = _db.InstaUsers.Find(User.Identity.GetUserId());
            var listOfUsers = _db.InstaUsers.Where(x => x.FName.StartsWith(searchInput) || x.Email.StartsWith(searchInput) || searchInput == null ).ToList();
            var activeUserIndex= listOfUsers.FindIndex(x => x.Id.Equals(user.Id));
            if (activeUserIndex >=0)
            {
                listOfUsers.RemoveAt(activeUserIndex);
            }
            UserSearch userSearch = new UserSearch
            {
                activeUser = user,
                users = listOfUsers,
            };
            userSearch.activeUser.FollowRequests = _db.FollowRequests.Where(x => x.FromUserID == user.Id || x.ToUserID == user.Id).ToList(); 
            userSearch.activeUser.FriendShips=_db.FriendShips.Where(x => x.User1ID == user.Id ||x.User2ID==user.Id).ToList();
            userSearch.users.ForEach(x =>x.FriendShips =_db.FriendShips.Where(y=>y.User1ID==x.Id||y.User2ID==x.Id).ToList());
            return View(userSearch);
        }

        public ActionResult StoryView(string id)
        {
            UserStory userStory = new UserStory
            {
                ActiveUserId = User.Identity.GetUserId(),
                Story= _db.Stories.SingleOrDefault(x => x.Id == id),
             };
          
            if (userStory.Story != null)
            {

                userStory.Story.Creator=_db.InstaUsers.SingleOrDefault(x=>x.Id== userStory.Story.CreatorID);
                userStory.Story.StoryFiles = _db.StoryFiles.Where(x => x.StoryID == userStory.Story.Id).ToList();
            }
    
            return View(userStory);
        }





        //User  section
        private List<User> GetUsers(IdentityUser identityUser )
        {
            var users = _db.InstaUsers.Where(x => x.Posts.Count > 3).ToList();
            var activeUserIndex = users.FindIndex(x => x.Id.Equals(identityUser.Id));

            if (activeUserIndex >= 0)
            {
                users.RemoveAt(activeUserIndex);
            }
            return users;
        }
        private List<User> GetUsersWithStories()
        {
            var users = _db.InstaUsers.Where(x => x.Stories.Count()>0).ToList();
            return users;
        }
        //Post section
        private List<Post> GetPosts(User user)
        {

            user.FriendShips = _db.FriendShips.Where(y => y.User1ID == user.Id || y.User2ID == user.Id).ToList();

            
            var posts = new List<Post>();
            foreach (var friend in user.FriendShips)
            {
                if (friend.User1ID != user.Id)
                {
                    posts = posts.Concat(_db.Posts.Where(x => x.CreatorID == friend.User1ID)).ToList();
                }
                else
                {
                    posts = posts.Concat(_db.Posts.Where(x => x.CreatorID == friend.User2ID)).ToList();
                }
            }


            posts = posts.Concat(_db.Posts.Where(x => x.CreatorID == user.Id)).ToList();

      

            posts=posts.OrderByDescending(p => p.PostedOn).ToList();

            return posts;
        }
        private List<PostFile> GetPostFiles()
        {
            var postFiles = _db.PostFiles.ToList();
            return postFiles;
        }
        private List<PostReact> GetPostReacts()
        {
            var postReacts = _db.PostReacts.ToList();
            return postReacts;
        }
        private List<Comment> GetComments()
        {
            var comments = _db.Comments.Include(c =>c.Post).OrderBy(c=>c.CreatedDate).ToList();
            return comments;
        }

        //Story section
       
        private List<Story> GetStories(User user)
        {
      
            var stories = new List<Story>();
            foreach (var friend in user.FriendShips)
            {
                if (friend.User1ID != user.Id)
                {
                    stories = stories.Concat(_db.Stories.Where(x => x.CreatorID == friend.User1ID)).ToList();
                }
                else
                {
            
                    stories = stories.Concat(_db.Stories.Where(x => x.CreatorID == friend.User2ID)).ToList();
                }
            }

            stories = stories.Concat(_db.Stories.Where(x => x.CreatorID == user.Id)).ToList();


            stories = stories.OrderByDescending(s => s.CreatedAt).ToList();

            var activeUserIndex = stories.FindIndex(x => x.CreatorID.Equals(user.Id));
            if (activeUserIndex >= 0)
            {
                stories.RemoveAt(activeUserIndex);
                stories.Insert(0, user.Stories.First());
            }
            return stories;
        }
        private List<Story> GetStoriesForView()
        {
            var stories = _db.Stories.Include(s => s.Creator).ToList();
            return stories;
        }
        private List<StoryView> GetStoryViews()
        {
            var storiesViews = _db.StoryViews.ToList();
            return storiesViews;
        }

        //Nav-Notification section
        private List<FriendShip> GetFriendShips(User user)
        {
            user.FriendShips = _db.FriendShips.Where(y => y.User1ID == user.Id || y.User2ID == user.Id).ToList();           
            return (List<FriendShip>)user.FriendShips;
        }



    }



}