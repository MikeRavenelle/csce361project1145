using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using csce361project1145.Models;
using System.Data.Entity;

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
            var test = new location();
            var context = new dsimpsonEntities4();
            test.latitude = 5;
            test.longitude = 6;
            context.locations.Add(test);
            context.SaveChanges();


            //Do stuff for uploading, no idea what yet.
            return View("ViewMap");

        }

        public ActionResult getLocations()
        {

            var context = new dsimpsonEntities4();
            var locations = context.locations.ToList();
            return Json(locations.Select(x => new
            {
                longitude = x.longitude,
                latitude = x.latitude
            }),
                  JsonRequestBehavior.AllowGet);
        }
    }
}
