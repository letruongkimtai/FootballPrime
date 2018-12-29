using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballPrime_Website.Models;

namespace FootballPrime_Website.Controllers.Admin
{
    public class PostsController : Controller
    {
        private FootballPrimeDbContext db = new FootballPrimeDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.PostType);
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
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
            ViewBag.PostTypeID = new SelectList(db.PostTypes, "PostTypeID", "PostTypeName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,Title,Date,Author,Img,Quote,Content,PostTypeID")] Post post, HttpPostedFileBase f)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(f.FileName);
                if (f != null && f.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/img/" + f.FileName));
                    string fullPath = Request.MapPath("~/img/" + post.Img);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    f.SaveAs(path);
                    post.Img = fileName;
                }
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostTypeID = new SelectList(db.PostTypes, "PostTypeID", "PostTypeName", post.PostTypeID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostTypeID = new SelectList(db.PostTypes, "PostTypeID", "PostTypeName", post.PostTypeID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "PostID,Title,Date,Author,Img,Quote,Content,PostTypeID")] int? id, HttpPostedFileBase f)
        {
            Post post = db.Posts.Find(id);
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(f.FileName);
                if (f != null && f.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/img/" + f.FileName));
                    string fullPath = Request.MapPath("~/img/" + post.Img);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    f.SaveAs(path);
                    post.Img = fileName;
                }
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostTypeID = new SelectList(db.PostTypes, "PostTypeID", "PostTypeName", post.PostTypeID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
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
        public ActionResult DeleteConfirmed(int id)
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
