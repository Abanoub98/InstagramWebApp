using InstgramWebApp.CustomAttribute;
using InstgramWebApp.Models;
using InstgramWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstgramWebApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;


        public ProfileController()
        {
            _db = new ApplicationDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_db));

        }
        // GET: Profile
        public ActionResult Index()
        {
  
            var user = _db.InstaUsers.Find(User.Identity.GetUserId());
            HomeViews homeViews = new HomeViews
            {
                user = user,
                Posts= GetPosts(),
                PostFiles = GetPostFiles(),
                ActiveUserID = user.Id,
            };
            homeViews.user.FriendShips = _db.FriendShips.Where(x => x.User1ID == user.Id || x.User2ID == user.Id).ToList();
            return View(homeViews);
        }

        [Route("Profile/index/{id}")]
        public ActionResult Index(string id)
        {
            var user = _db.InstaUsers.Find(id);
            HomeViews homeViews = new HomeViews
            {
                user = user,
                Posts = GetPosts(),
                PostFiles = GetPostFiles(),
                ActiveUserID = User.Identity.GetUserId(),
            };
            homeViews.user.FriendShips = _db.FriendShips.Where(x => x.User1ID == id || x.User2ID == id).ToList();
            return View(homeViews);
        }

        public ActionResult Friends(string id)
        {
            var user = _db.InstaUsers.Find(id);
            if(user != null)
            {
                user.FriendShips = _db.FriendShips.Where(x => x.User1ID == id || x.User2ID == id).ToList();
                UserFriends userFriends = new UserFriends
                {
                    User = user,
                    ActiveUserId = User.Identity.GetUserId(),
                };
                foreach (var friend in userFriends.User.FriendShips)
                {
                 
                        friend.User1.FriendShips = _db.FriendShips.Where(x => x.User1ID == friend.User1ID || x.User2ID == friend.User1ID).ToList();
                  
                        friend.User2.FriendShips = _db.FriendShips.Where(x => x.User1ID == friend.User2ID || x.User2ID == friend.User2ID).ToList();
                    
                }
                 return View(userFriends);
            }        
            return View();
        }
        public ActionResult Setting()
        {
            var user = _db.InstaUsers.Find(User.Identity.GetUserId());
            SettingViewModel settingViews = new SettingViewModel
            {
                user = user,
            };
            return View(settingViews);
        }

        public ActionResult RemovePhoto()
        {
            var instaUser = _db.InstaUsers.Find(User.Identity.GetUserId());
            instaUser.ImgPath = "/Content/img/default-user-img.jpg";
            _db.SaveChanges();
            TempData["AlertMessage"] = "Profile Info has changed successfully";
            return RedirectToAction("Setting", "Profile");
        }



        [HttpPost]
        public ActionResult ProfileInfo(SettingViewModel formuser)
        {
            var instaUser = _db.InstaUsers.Find(User.Identity.GetUserId());
            //if (!ModelState.IsValid)
            //{
            //    return View("Setting",formuser);
            //}
            if (formuser.ImgFile != null)
            {
                string FileName = Path.GetFileNameWithoutExtension(formuser.ImgFile.FileName);

                //To Get File Extension  
                string FileExtension = Path.GetExtension(formuser.ImgFile.FileName);

                //Add Current Date To Attached File Name  
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                //Get Upload path from Web.Config file AppSettings.  
                //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                var uploadPath = Path.Combine(Server.MapPath("~/Content/UploadedPhotos"), FileName);



                //To copy and save file into server.  
                formuser.ImgFile.SaveAs(uploadPath);
                formuser.user.ImgPath = Path.Combine("/Content/UploadedPhotos", FileName);
                instaUser.ImgPath = formuser.user.ImgPath;
            }
            else
            {
                instaUser.ImgPath = instaUser.ImgPath;
            }
           
            instaUser.FName = formuser.user.FName;
            instaUser.LName = formuser.user.LName;
            instaUser.Bio = formuser.user.Bio;

            _db.SaveChanges();
            TempData["AlertMessage"] = "Profile Info has changed successfully";
            return RedirectToAction("Setting","Profile");
        }


        [HttpPost]
        public ActionResult ChangePassword(PasswordViewModel pass)
        {
            var instaUser = _db.InstaUsers.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(rec => rec.Id == instaUser.Id);
                System.Diagnostics.Debug.WriteLine(_userManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, pass.CurrentPassword));
                 if(_userManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, pass.CurrentPassword) != PasswordVerificationResult.Failed)
                {
                    _userManager.ChangePassword(instaUser.Id, pass.CurrentPassword, pass.NewPassword);
                    TempData["AlertMessage"] = "Password has changed successfully";
                    return RedirectToAction("Setting", "Profile");
                }
                 else
                {
                    ModelState.AddModelError("CurrentPass", "Invalid Current Password!");
                }
                
             
            }
        
            
            return View("Setting", new SettingViewModel() { pass = pass, user = instaUser });
        }

 
        [HttpPost]
        public ActionResult PersonalInfo(User user)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
            var instaUser = _db.InstaUsers.Find(identityUser.Id);
            //if (!ModelState.IsValid)
            //{
            //    return View("Setting",new SettingViewModel { user=user });
            //}
            instaUser.Email = user.Email;
            instaUser.BirthDate = user.BirthDate;
            instaUser.Country = user.Country;
            instaUser.Mobile = user.Mobile;
            identityUser.Email=user.Email;
            identityUser.PhoneNumber = user.Mobile;
            _db.SaveChanges();
            TempData["AlertMessage"] = "Personal Info has changed successfully";
            return RedirectToAction("Setting", "Profile");
        }

        public ActionResult Requests()
        {
            var instaUser = _db.InstaUsers.Find(User.Identity.GetUserId());
            var usersWithMaxPosts = GetUsers();
            var requests=_db.FollowRequests.Where(x => x.ToUserID == instaUser.Id).ToList();
            UserRequests userRequests = new UserRequests
            {
                activeUser = instaUser,
                followRequests = requests,
                Users=usersWithMaxPosts,

            };
            userRequests.Users.ForEach(u => u.FriendShips = _db.FriendShips.ToList());
            return View(userRequests);
        }





        private List<User> GetUsers()
        {
            var users = _db.InstaUsers.Where(x => x.Posts.Count > 3).ToList();
            var activeUserIndex = users.FindIndex(x => x.Id.Equals(User.Identity.GetUserId()));
            if (activeUserIndex >= 0)
            {
                users.RemoveAt(activeUserIndex);
            }
            return users;
        }
        private List<Post> GetPosts()
        {
            var posts = _db.Posts.OrderByDescending(p => p.PostedOn).ToList();
            return posts;
        }
        private List<PostFile> GetPostFiles()
        {
            var postFiles = _db.PostFiles.ToList();
            return postFiles;
        }


        public PartialViewResult _Header()
        {
            var user = _db.InstaUsers.Find(User.Identity.GetUserId());
            return PartialView(user);
        }
    }
}