using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeWork7.DataAcces;
using HomeWork7.Models;

namespace HomeWork7.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private UserContext db = new UserContext();
        
        public ActionResult Index()
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            var users = db.Users.Include(u => u.Role);
            return View(users.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        
        public ActionResult Create()
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Password,Age,RoleId")] User user)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }
        
        public ActionResult Edit(int? id)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Password,Age,RoleId")] User user)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }
        
        public ActionResult Delete(int? id)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
