using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballPrime_Website.Models;
using System.IO;
using System.Net;
using System.Data.Entity;

namespace FootballPrime_Website.Controllers.Admin
{
    public class ProductAdController : BaseController
    {
        FootballPrimeDbContext db = new FootballPrimeDbContext();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(db.Products.ToList().OrderByDescending(n => n.PrID));
        }

        // GET: ADMIN/Product/Create
        [HttpGet]

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase img)
        {
            if (img == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn hình ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/img/"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        img.SaveAs(path);
                    }
                    product.Pic = filename;
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "ProductAd");
            }
        }


        public ActionResult DropList()
        {

            return PartialView();
        }

        public ActionResult Details(int id)
        {
            Product product = db.Products.SingleOrDefault(n => n.PrID == id);
            ViewBag.PrID = product.PrID;
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        public ActionResult Edit(int? id) // trên đây chắc có sẵn rồi khỏi sửa còn mấy cái detail delete create ng tối cổ có ghi sẵn hết 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "PrID,PrName,Pic,Describe,Price,PrTypeID")] int? id, HttpPostedFileBase f)
        {
            Product product = db.Products.Find(id);
            if (ModelState.IsValid)
            {
                
                var fileName = Path.GetFileName(f.FileName);
                if (f != null && f.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/img/" + f.FileName));
                    string fullPath = Request.MapPath("~/img/" + product.Pic);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    f.SaveAs(path);
                    product.Pic = fileName;
                } 
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product product = db.Products.SingleOrDefault(n => n.PrID == id);
            ViewBag.PrID = product.PrID;
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult TrueDelete(int id)
        {
            Product product = db.Products.SingleOrDefault(n => n.PrID == id);
            ViewBag.PrID = product.PrID;
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("index", "ProductAd");
        }
    }
}