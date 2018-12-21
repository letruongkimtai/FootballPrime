using FootballPrime_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FootballPrime_Website.Controllers
{
    public class PostController : Controller
    {
        FootballPrimeDbContext db = new FootballPrimeDbContext();
        // GET: Post
        public ActionResult Index(int id)
        {
            var p = from s in db.Posts
                    where id == s.PostID
                    select s;
            return View(p.Single());
        }
        public ActionResult ShowAllPost(int? page)
        {
            int pageSize = 5; //Số mục hiện trên 1 trang
            int pageNumber = (page ?? 1); //Mặc định vào sẽ ở trang 1
            var allPost = db.Posts.OrderByDescending(a => a.Date).ToList();
            return View(allPost.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult PostWithType(int id)
        {
            var item = from s in db.Posts
                       where s.PostTypeID == id
                       select s;
            return View(item);
        }
    }
}