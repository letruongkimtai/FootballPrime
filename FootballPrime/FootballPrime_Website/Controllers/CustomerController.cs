using FootballPrime_Website.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPrime_Website.Controllers
{
    public class CustomerController : Controller
    {
        FootballPrimeDbContext db = new FootballPrimeDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registion(FormCollection collection, Customer customer)
        {
            var name = collection["CtmName"];
            var username = collection["UserName"];
            var password = collection["Password"];
            var cfmpassword = collection["cfmPassword"];
            var tel = collection["Tel"];
            var address = collection["Address"];
            var email = collection["Email"];
            if (password == cfmpassword)
            {
                try
                {
                    customer.CtmName = name;
                    customer.UserName = username;
                    customer.Password = password;
                    customer.Tel = int.Parse(tel);
                    customer.Address = address;
                    customer.Email = email;
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (DbEntityValidationException e)
                {
                    throw e;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ViewData["Error"] = "Mật khẩu và nhập lại mật khẩu không trùng khớp";
            }
            return this.Registion();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection, Customer customer)
        {
            var userName = collection["userName"];
            var password = collection["password"];
            customer = db.Customers.SingleOrDefault(c => c.UserName == userName && c.Password == password);
            if (customer != null)
            {
                ViewData["msg"] = "Đăng nhập thành công";
                Session["Account"] = customer.UserName;
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewData["msg"] = "Tên đăng nhập hoặc mật khẩu không chính xác";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["Account"] = null;
            return RedirectToAction("Login");
        }
    }
}