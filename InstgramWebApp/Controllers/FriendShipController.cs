using InstgramWebApp.CustomAttribute;
using InstgramWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstgramWebApp.Controllers
{
    [Authorize]
    public class FriendShipController : Controller
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;


        public FriendShipController()
        {
            _db = new ApplicationDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_db));

        }

        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "SendRequest")]
        public ActionResult SendRequest(string toUserId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
            FollowRequest request = new FollowRequest
            {
                FromUserID = identityUser.Id,
                ToUserID = toUserId,
            };
            if (ModelState.IsValid)
            {
                _db.FollowRequests.Add(request);
                _db.SaveChanges();
                return RedirectToAction("SearchResult","Home");
            }
            return View(request);
        }

        [HttpPost]
        public ActionResult SendRequestFromProfile(string toUserId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
            FollowRequest request = new FollowRequest
            {
                FromUserID = identityUser.Id,
                ToUserID = toUserId,
            };
            if (ModelState.IsValid)
            {
                _db.FollowRequests.Add(request);
                _db.SaveChanges();
                return RedirectToAction("Index", "Profile", new { id = toUserId });
            }
            return View(request);
        }



        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "Confirm")]
        public ActionResult ConfirmRequest(string toUserId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
            FriendShip friendShip = new FriendShip
            {
                User1ID = identityUser.Id,
                User2ID = toUserId,
                CreatedAt = DateTime.Now,
            };
            var selectedRequest = _db.FollowRequests.SingleOrDefault(x => x.FromUserID == toUserId && x.ToUserID == identityUser.Id);
            if (ModelState.IsValid)
            {
                _db.FriendShips.Add(friendShip);
                _db.FollowRequests.Remove(selectedRequest);
                _db.SaveChanges();
                return RedirectToAction("Requests", "Profile");
            }
            return View(friendShip);
        }


        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "DeleteFromSearch")]
        public ActionResult DeleteRequestFromSearch(string toUserId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());

            var selectedRequest = _db.FollowRequests.SingleOrDefault(x => x.FromUserID == identityUser.Id && x.ToUserID == toUserId);
            if (selectedRequest != null)
            {
                _db.FollowRequests.Remove(selectedRequest);
                _db.SaveChanges();
                return RedirectToAction("SearchResult", "Home");
            }

            return RedirectToAction("SearchResult", "Home");

        }
        [HttpPost]    
        public ActionResult DeleteRequestFromProfile(string toUserId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());

            var selectedRequest = _db.FollowRequests.SingleOrDefault(x => x.FromUserID == identityUser.Id && x.ToUserID == toUserId);
            if (selectedRequest != null)
            {
                _db.FollowRequests.Remove(selectedRequest);
                _db.SaveChanges();
                return RedirectToAction("Index", "Profile", new { id = toUserId });
            }

            return RedirectToAction("Index", "Profile", new { id = toUserId });

        }



        [HttpPost]
        [AllowMultipleButton(Name = "action", Argument = "Unfriend")]
        public ActionResult UnfriendFromSearch(string toUserId)
        {           
            var identityUser = _userManager.FindById(User.Identity.GetUserId());
          
            var selectedFriendShip = _db.FriendShips.SingleOrDefault(x => x.User1ID == toUserId && x.User2ID == identityUser.Id || x.User2ID == toUserId && x.User1ID == identityUser.Id);
            if (selectedFriendShip!=null)
            {
                _db.FriendShips.Remove(selectedFriendShip);
                _db.SaveChanges();
                return RedirectToAction("SearchResult", "Home");
            }
            return View(selectedFriendShip);
        }

        [HttpPost]
        public ActionResult UnfriendFromProfile(string toUserId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());

            var selectedFriendShip = _db.FriendShips.SingleOrDefault(x => x.User1ID == toUserId && x.User2ID == identityUser.Id || x.User2ID == toUserId && x.User1ID == identityUser.Id);
            if (selectedFriendShip!=null)
            {
                _db.FriendShips.Remove(selectedFriendShip);
                _db.SaveChanges();
                return RedirectToAction("Index", "Profile", new { id = toUserId });
            }
            return RedirectToAction("Index", "Profile", new { id = toUserId });
        }

        [HttpPost]
        public ActionResult UnfriendFromFriends(string toUserId)
        {
            var identityUser = _userManager.FindById(User.Identity.GetUserId());

            var selectedFriendShip = _db.FriendShips.SingleOrDefault(x => x.User1ID == toUserId && x.User2ID == identityUser.Id || x.User2ID == toUserId && x.User1ID == identityUser.Id);
            if (selectedFriendShip != null)
            {
                _db.FriendShips.Remove(selectedFriendShip);
                _db.SaveChanges();
                return RedirectToAction("Friends", "Profile", new { id = identityUser.Id });
            }
            return RedirectToAction("Friends", "Profile", new { id = identityUser.Id });
        }
    }
}