using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace csce361project1145.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //returns us to the Index View
            return View();
        }

        public ActionResult ViewMap()
        {
            //returns us to the ViewMap View
            return View();
        }

        public ActionResult Search()
        {
            //returns us to the Search View
            return View();
        }

        public ActionResult Upload()
        {
            //returns us to the Upload View
            return View();
        }

        public ActionResult UploadPhoto()
        {
            //Do stuff for uploading, no idea what yet.
            return View("ViewMap");

        }
    }
}
