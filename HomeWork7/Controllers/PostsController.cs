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
    public class PostsController : Controller
    {
        private UserContext db = new UserContext();

        // GET: Posts
        public ActionResult Index()
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            return View(db.Posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(Guid? id)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            return View();
        }

        // POST: Posts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImgPath,Title,Text,PublicationDate")] Post post)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (ModelState.IsValid)
            {
                post.Id = Guid.NewGuid();
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImgPath,Title,Text,PublicationDate")] Post post)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            ViewData["IsAuthenticated"] = "Authenticated";
            ViewData["IsAdmin"] = "Admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
