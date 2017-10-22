using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Areas.Teacher.Controllers
{
    public class HomeController : Controller
    {
        public  HomeController()
        {

        }
        // GET: Teacher/Home
        [Authorize(Roles = "Teacher")]
        public ActionResult Index()
        {
            return View();
        }
    }
}