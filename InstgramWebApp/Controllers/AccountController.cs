using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InstgramWebApp.Models;
using InstgramWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace InstgramWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;

        private  SignInManager <IdentityUser,string> _signInManager;

        public AccountController()
        {
            _db = new ApplicationDbContext();
           _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_db));
           
        }
        // GET: Account


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ViewModels.LoginViewModel login)
        {
            _signInManager = new SignInManager<IdentityUser, string>(_userManager, HttpContext.GetOwinContext().Authentication);
            if (ModelState.IsValid)
            {
                var user =  _userManager.Find(login.Username, login.Password);
                if (user != null)
                {
                    _signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("index","Home");
                }
                else
                {
                    ModelState.AddModelError("LoginState", "Invalid username or password");
                }
            }
            return View(login);
        }


        [HttpGet]
        public ActionResult LogOff()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(ViewModels.LoginViewModel login)
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login", "Account");
        }


        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(ViewModels.RegisterViewModel formData)
        {
                if (!ModelState.IsValid)
                    {
                    return View ("Register",formData);
                    }
            //Use Namespace called :  System.IO  
           
            if (formData.ImgFile!=null)
            {
                string FileName = Path.GetFileNameWithoutExtension(formData.ImgFile.FileName);
                //To Get File Extension  
                string FileExtension = Path.GetExtension(formData.ImgFile.FileName);

                //Add Current Date To Attached File Name  
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                //Get Upload path from Web.Config file AppSettings.  
                //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                var uploadPath = Path.Combine(Server.MapPath("~/Content/UploadedPhotos"), FileName);



                //To copy and save file into server.  
                formData.ImgFile.SaveAs(uploadPath);


                formData.User.ImgPath = Path.Combine("/Content/UploadedPhotos", FileName);
            }
            else
            {

                formData.User.ImgPath = "/Content/img/default-user-img.jpg";
            }
            var identityUser = new IdentityUser
            {
                UserName = formData.User.Username,
                PhoneNumber = formData.User.Mobile,
                Email = formData.User.Email,
                Id = formData.User.Id
            };
            _userManager.Create(identityUser, formData.Password);
            _db.InstaUsers.Add(formData.User);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }


    }
}