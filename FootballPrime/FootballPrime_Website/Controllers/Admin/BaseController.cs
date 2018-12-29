using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPrime_Website.Controllers.Admin
{
    public class BaseController : Controller
    {
        public string alertSuccess(string alertType)
        {
            string strScript;
            strScript = "<script>" + "alert( 'Bạn đã " + alertType + " thành công!' );window.location.hostname;</script>";
            return strScript;
        }

        public string alertError(string alertType)
        {
            string strScript;
            strScript = "<script>" + "alert( 'Có lỗi xảy ra:  " + alertType + " !!! ' );window.location.hostname;</script>";
            return strScript;
        }
    }
}