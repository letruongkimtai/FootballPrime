using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballPrime_Website.Models;
using PagedList;

namespace FootballPrime_Website.Controllers
{
    public class ProductController : Controller
    {
        FootballPrimeDbContext db = new FootballPrimeDbContext();
        // GET: Product
        public ActionResult Index(int? page)
        {
            int pageSize = 8; //Số mục hiện trên 1 trang
            int pageNumber = (page ?? 1); //Mặc định vào sẽ ở trang 1
            var allProduct = db.Products.OrderByDescending(a => a.PrName).ToList();
            return View(allProduct.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ProductMenu()
        {
            return PartialView(db.ProductTypes);
        }

        public ActionResult ProductWithType(int id)
        {
            var p = from s in db.Products
                       where s.PrTypeID == id
                       select s;
            return View(p);
        }
    }
}