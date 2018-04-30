using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunRise.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
