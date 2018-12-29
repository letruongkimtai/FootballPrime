using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballPrime_Website.Models;



namespace FootballPrime_Website.Controllers.Admin
{
    public class RoleController : Controller
    {
        
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
    }
}