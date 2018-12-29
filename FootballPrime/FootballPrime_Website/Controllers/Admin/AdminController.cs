using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballPrime_Website.Models;



namespace FootballPrime_Website.Controllers.Admin
{
    public class AdminController : Controller
    {
        FootballPrimeDbContext db = new FootballPrimeDbContext();
       
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Bao()
        {
            return View(db.Products.ToList());
        }
        [HttpPost]
        public ActionResult Login(FormCollection Collection)
        {
            var tendn = Collection["username"];
            var matkhau = Collection["password"];
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên tài khoản";
            }
            else if(String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                var ad = db.Admins.SingleOrDefault(n => n.AdmName == tendn && n.AdmPwd == matkhau);
                    if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
             }
            return View();
        }
    }
}