using InstgramWebApp.CustomAttribute;
using InstgramWebApp.Models;
using InstgramWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstgramWebApp.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;


        public PostController()
        {
            _db = new ApplicationDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_db));

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "CreatePost")]
        public ActionResult Create(PostViewModel post)
        {
            var user = _db.InstaUsers.Find(User.Identity.GetUserId());
            if (!ModelState.IsValid)
            {
                return View("Create", post);
            }

            foreach(var  image in post.ImgFiles)
            {
                //Use Namespace called :  System.IO  
                string FileName = Path.GetFileNameWithoutExtension(image.FileName);

                //To Get File Extension  
                string FileExtension = Path.GetExtension(image.FileName);

                //Add Current Date To Attached File Name  
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                //Get Upload path from Web.Config file AppSettings.  
                //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                var uploadPath = Path.Combine(Server.MapPath("~/Content/UploadedPhotos"), FileName);

                //To copy and save file into server.  
                image.SaveAs(uploadPath);

                var postFile = new PostFile()
                {
                    PostID = post.Post.Id,
                    FilePath = Path.Combine("/Content/UploadedPhotos", FileName)
                };

                post.Post.PostFiles.Add(postFile);
            }
            post.Post.CreatorID = user.Id;
            post.Post.Caption=post.Caption;
            post.Post.PostedOn = DateTime.Now;
            _db.Posts.Add(post.Post);
            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }


        [HttpPost]
        public ActionResult DeletePost (string postId)
        {
            var post = _db.Posts.Find(postId);
            if (post != null)
            {
                if (post.PostFiles.Count > 0)
                {
                    foreach (var postFile in post.PostFiles)
                    {
                        string path = Server.MapPath(postFile.FilePath);
                        FileInfo file = new FileInfo(path);
                        if (file.Exists)//check file exsit or not  
                        {
                            file.Delete();
                        }
                    }
                }
              
                _db.Posts.Remove(post);
                _db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "CreateStory")]
        public ActionResult CreateStory(StoryViewModel story)
        {     
            var user = _db.InstaUsers.Find(User.Identity.GetUserId());
            user.Stories = _db.Stories.Where(x => x.CreatorID == user.Id).ToList();
            if (!ModelState.IsValid)
            {
                return View("Create", story);
            }

            if (user.Stories.Count > 0)
            {
                foreach (var image in story.ImgFiles)
                {
                    //Use Namespace called :  System.IO  
                    string FileName = Path.GetFileNameWithoutExtension(image.FileName);

                    //To Get File Extension  
                    string FileExtension = Path.GetExtension(image.FileName);

                    //Add Current Date To Attached File Name  
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                    //Get Upload path from Web.Config file AppSettings.  
                    //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                    var uploadPath = Path.Combine(Server.MapPath("~/Content/UploadedPhotos"), FileName);

                    //To copy and save file into server.  
                    image.SaveAs(uploadPath);


                    var storyFile = new StoryFile()
                    {
                        StoryID = user.Stories.ElementAt(0).Id,
                        FilePath = Path.Combine("/Content/UploadedPhotos", FileName)
                    };

                   storyFile.Story=_db.Stories.SingleOrDefault(s=>s.Id==storyFile.StoryID);
                    _db.StoryFiles.Add(storyFile);
         
                }
                _db.SaveChanges();


                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var image in story.ImgFiles)
                {
                    //Use Namespace called :  System.IO  
                    string FileName = Path.GetFileNameWithoutExtension(image.FileName);

                    //To Get File Extension  
                    string FileExtension = Path.GetExtension(image.FileName);

                    //Add Current Date To Attached File Name  
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                    //Get Upload path from Web.Config file AppSettings.  
                    //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                    var uploadPath = Path.Combine(Server.MapPath("~/Content/UploadedPhotos"), FileName);

                    //To copy and save file into server.  
                    image.SaveAs(uploadPath);

                    var storyFile = new StoryFile
                    {
                        StoryID = story.Story.Id,
                        FilePath = Path.Combine("/Content/UploadedPhotos", FileName)
                    };

                    story.Story.StoryFiles.Add(storyFile);
                }
                story.Story.CreatorID = user.Id;
                story.Story.CreatedAt = DateTime.Now;
                _db.Stories.Add(story.Story);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            } 
        }


        [HttpPost]
        public ActionResult DeleteStory(string storyId)
        {
            var story = _db.Stories.Find(storyId);
            if (story != null)
            {
                if (story.StoryFiles.Count > 0)
                {
                    foreach (var storyFile in story.StoryFiles)
                    {
                        string path = Server.MapPath(storyFile.FilePath);
                        FileInfo file = new FileInfo(path);
                        if (file.Exists)//check file exsit or not  
                        {
                            file.Delete();
                        }
                    }
                }

                _db.Stories.Remove(story);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "Like")]
        public ActionResult LikeHomePost(string postId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());

            var post=_db.Posts.SingleOrDefault(x => x.Id == postId);
            post.NoOfLikes++;

            PostReact postReact = new PostReact {
                PostID = postId,
                ReactorID = identityUser.Id,
                Reaction = "like",
            };



            if (ModelState.IsValid)
            {
                ReactNotification reactNotification = new ReactNotification
                {
                    FromUserID = identityUser.Id,
                    PostID = postId,
                    PostOwnerID = post.CreatorID,
                    Reaction ="Like",
                };               
                _db.PostReacts.Add(postReact);

                if (reactNotification.PostOwnerID != reactNotification.FromUserID)
                {
                    _db.ReactNotifications.Add(reactNotification);
                }
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "Dislike")]
        public ActionResult DisLikeHomePost(string postId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());

            var post = _db.Posts.SingleOrDefault(x => x.Id == postId);
            post.NoOfLikes--;

           var react=_db.PostReacts.SingleOrDefault(x => x.PostID == postId && x.ReactorID==identityUser.Id && x.Reaction=="like");
           


            if (ModelState.IsValid)
            {
                var notification=_db.ReactNotifications.SingleOrDefault(x=>x.PostID==postId && x.FromUserID==identityUser.Id && x.PostOwnerID==post.CreatorID && x.Reaction == "Like");
                if (notification!=null)
                {
                    _db.ReactNotifications.Remove(notification);
                }
                _db.PostReacts.Remove(react);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "LikeSingle")]
        public ActionResult LikeSinglePost(string postId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());

            var post = _db.Posts.SingleOrDefault(x => x.Id == postId);
            post.NoOfLikes++;

            PostReact postReact = new PostReact
            {
                PostID = postId,
                ReactorID = identityUser.Id,
                Reaction = "like",
            };



            if (ModelState.IsValid)
            {
                ReactNotification reactNotification = new ReactNotification
                {
                    FromUserID = identityUser.Id,
                    PostID = postId,
                    PostOwnerID = post.CreatorID,
                    Reaction = "Like",
                };
                _db.PostReacts.Add(postReact);

                if (reactNotification.PostOwnerID != reactNotification.FromUserID)
                {
                    _db.ReactNotifications.Add(reactNotification);
                }
                _db.PostReacts.Add(postReact);
                _db.SaveChanges();
                return RedirectToAction("Details", "Post", new { id = postId });
            }
            return RedirectToAction("Details", "Post", new { id = postId });
        }

        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "DislikeSingle")]
        public ActionResult DisLikeSinglePost(string postId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());

            var post = _db.Posts.SingleOrDefault(x => x.Id == postId);
            post.NoOfLikes--;

            var react = _db.PostReacts.SingleOrDefault(x => x.PostID == postId && x.ReactorID == identityUser.Id && x.Reaction == "like");



            if (ModelState.IsValid)
            {
                var notification = _db.ReactNotifications.SingleOrDefault(x => x.PostID == postId && x.FromUserID == identityUser.Id && x.PostOwnerID == post.CreatorID && x.Reaction=="Like");
                if (notification != null)
                {
                    _db.ReactNotifications.Remove(notification);
                }
                _db.PostReacts.Remove(react);
                _db.SaveChanges();
                return RedirectToAction("Details", "Post", new { id = postId });
            }
            return RedirectToAction("Details", "Post", new { id = postId });
        }



        [HttpPost]
        public ActionResult CommentHomePost(HomeViews homeViews, string postId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
            var post = _db.Posts.SingleOrDefault(x => x.Id == postId);

            Comment comment = new Comment
            {
                PostID = postId,
                CreatorID = identityUser.Id,
                CommentText = homeViews.CommentText,
                CreatedDate = DateTime.Now,
           };
                
            if (ModelState.IsValid)
            {
                ReactNotification reactNotification = new ReactNotification
                {
                    FromUserID = identityUser.Id,
                    PostID = postId,
                    PostOwnerID = post.CreatorID,
                    Reaction = "Comment",
                    CommentID = comment.Id,
                };
         

                if (reactNotification.PostOwnerID != reactNotification.FromUserID)
                {
                    _db.ReactNotifications.Add(reactNotification);
                }
                _db.Comments.Add(comment);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult UnCommentHomePost(string commentId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
            var comment = _db.Comments.SingleOrDefault(x => x.Id == commentId);
            var post=_db.Posts.SingleOrDefault(x => x.Id == comment.PostID);

            if (comment != null)
            {
                var notification = _db.ReactNotifications.SingleOrDefault(x => x.PostID == post.Id &&x.CommentID ==commentId && x.FromUserID == identityUser.Id && x.PostOwnerID == post.CreatorID && x.Reaction=="Comment");
                if (notification != null)
                {
                    _db.ReactNotifications.Remove(notification);
                }
                _db.Comments.Remove(comment);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CommentSinglePost(UserPost userPost, string postId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
            var post = _db.Posts.SingleOrDefault(x => x.Id == postId);
            Comment comment = new Comment
            {
                PostID = postId,
                CreatorID = identityUser.Id,
                CommentText = userPost.CommentText,
                CreatedDate = DateTime.Now,
            };
            if (ModelState.IsValid)
            {
                ReactNotification reactNotification = new ReactNotification
                {
                    FromUserID = identityUser.Id,
                    PostID = postId,
                    PostOwnerID = post.CreatorID,
                    Reaction = "Comment",
                    CommentID = comment.Id,
                };


                if (reactNotification.PostOwnerID != reactNotification.FromUserID)
                {
                    _db.ReactNotifications.Add(reactNotification);
                }
                _db.Comments.Add(comment);
                _db.SaveChanges();
                return RedirectToAction("Details","Post", new { id = postId });
            }
            return RedirectToAction("Details", "Post", new { id = postId });
        }

        [HttpPost]
        public ActionResult UnCommentSinglePost(string postId,string commentId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
            var comment = _db.Comments.SingleOrDefault(x => x.Id == commentId);
            var post = _db.Posts.SingleOrDefault(x => x.Id == comment.PostID);

            if (comment != null)
            {
                var notification = _db.ReactNotifications.SingleOrDefault(x => x.PostID == post.Id &&x.CommentID==commentId &&x.FromUserID == identityUser.Id && x.PostOwnerID == post.CreatorID && x.Reaction == "Comment");
                if (notification != null)
                {
                    _db.ReactNotifications.Remove(notification);
                }
                _db.Comments.Remove(comment);
                _db.SaveChanges();
                return RedirectToAction("Details", "Post", new { id = postId });
            }
            return RedirectToAction("Details", "Post", new { id = postId });
        }




        public ActionResult Details(string id)
        {
            var user = _db.InstaUsers.Find(User.Identity.GetUserId());
            var post = _db.Posts.SingleOrDefault(p => p.Id == id);
            


            if (post == null)
            {
                return HttpNotFound();
            }

            UserPost userPost = new UserPost { 
               User = user,
               Post=post
            };  
             userPost.Post.Comments=userPost.Post.Comments.OrderBy(c => c.CreatedDate).ToList();


            return View(userPost);
        }

 

        public PartialViewResult _Header()
        {
            var user = _db.InstaUsers.Find(User.Identity.GetUserId());
            return PartialView(user);
        }
    }
}