using FootballPrime_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPrime_Website.Controllers
{
    public class HomeController : Controller
    {
        FootballPrimeDbContext db = new FootballPrimeDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var list = db.Posts.OrderByDescending(n => n.Date).ToList();
            return View(list.Take(3));
        }

        public ActionResult SideNews()
        {
            int id = 6;
            var news = db.Posts.Where(n => n.PostTypeID == id);
            return PartialView(news.Take(2));
        }

        public ActionResult BreakingNews()
        {
            var list = db.Posts.OrderByDescending(n => n.Date).ToList();
            return PartialView(list.Take(1));
        }

        public ActionResult Menu()
        {

            return PartialView(db.PostTypes);
        }

        public ActionResult MorePost()
        {
            int id = 5;
            var news = db.Posts.Where(n => n.PostTypeID == id);
            return PartialView(news.Take(5));
        }
    }
}