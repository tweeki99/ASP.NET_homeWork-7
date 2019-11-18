using HomeWork7.DataAcces;
using HomeWork7.Models;
using HomeWork7.Providers;
using HomeWork7.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork7.Controllers
{
    public class HomeController : Controller
    {
        CustomRoleProvider customRoleProvider = new CustomRoleProvider();
        private UserContext db = new UserContext();

        public ActionResult Index()
        {
            List<PostViewModel> viewModel = new List<PostViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                List<Post> posts = new List<Post>();
                posts = db.Posts.ToList();
               
                foreach (var post in posts)
                {
                    viewModel.Add(new PostViewModel { ImgPath = post.ImgPath, PublicationDate = post.PublicationDate, Text = post.Text, Title = post.Title });
                }
                
                ViewData["Posts"] = viewModel;

                ViewData["IsAuthenticated"] = "Authenticated";
                if (customRoleProvider.IsUserInRole(User.Identity.Name, "admin"))
                    ViewData["IsAdmin"] = "Admin";
                else
                    ViewData["IsAdmin"] = "NotAdmin";
            }
            else
            {
                ViewData["IsAuthenticated"] = "NotAuthenticated";
                ViewData["IsAdmin"] = "NotAdmin";
            }
            

            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ZatychkaAdmina()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["IsAuthenticated"] = "Authenticated";
                if (customRoleProvider.IsUserInRole(User.Identity.Name, "admin"))
                    ViewData["IsAdmin"] = "Admin";
                else
                    ViewData["IsAdmin"] = "NotAdmin";
            }
            else
            {
                ViewData["IsAuthenticated"] = "NotAuthenticated";
                ViewData["IsAdmin"] = "NotAdmin";
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}