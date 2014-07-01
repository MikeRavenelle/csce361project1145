using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using csce361project1145.Models;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Data.Entity;
using Newtonsoft.Json;



namespace csce361project1145.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //returns us to the Index View
            return View();
        }

        [Authorize]
        public ActionResult ViewMap()
        {
            //returns us to the ViewMap View
            return View();
        }

        [Authorize]
        public ActionResult Search()
        {
            //returns us to the Search View
            return View();
        }

        [Authorize]
        public ActionResult Upload()
        {
            //returns us to the Upload View
            return View();
        }

        [Authorize]
        public ActionResult UploadPhoto()
        {
            var test = new location();
            var context = new dsimpsonEntities8();
            test.latitude = "5";
            test.longitude = "6";
            context.locations.Add(test);
            context.SaveChanges();


            //Do stuff for uploading, no idea what yet.
            return View("ViewMap");

        }

        

        [Authorize]
        public ActionResult getLocations()
        {

            var context = new dsimpsonEntities8();
            var locations = context.locations.ToList();
            //Where example:
           // var locations = context.locations.Where(x => x.locationId == 2).ToList();
            return Json(locations.Select(x => new
            {
                locationId = x.locationId,
                longitude = x.longitude,
                latitude = x.latitude
            }),
                  JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [System.Web.Services.WebMethod]
        public ActionResult getLocationsById(String locationId)
        {
            var locationInt = Convert.ToInt32(locationId);
            var context = new dsimpsonEntities8();

            var locations = context.locations.Where(x => x.locationId == locationInt).ToList();
                
            return Json(locations.Select(x => new
            {
                
                locationId = x.locationId,
                longitude = x.longitude,
                latitude = x.latitude
            }),
                  JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [System.Web.Services.WebMethod]
        public ActionResult getPictures(String locationId)
        {
            var locationInt = Convert.ToInt32(locationId);
            System.Diagnostics.Debug.WriteLine(locationId);
            var context = new dsimpsonEntities8();
            var pictures = context.pictures.Where(x => x.locationId == locationInt).ToList();
            //Where example:
            // var locations = context.locations.Where(x => x.locationId == 2).ToList();
            return Json(pictures.Select(x => new
            {
                userId = x.userId,
                pictureId = x.pictureId,
                locationId = x.locationId,
                url = x.url,
                caption = x.caption
            }),
                  JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [System.Web.Services.WebMethod]
        public ActionResult getComments(String pictureId)
        {
            var pictureInt = Convert.ToInt32(pictureId);
            var context = new dsimpsonEntities8();
            var comments = context.comments.Where(x => x.pictureId == pictureInt).ToList();
            //Where example:
            // var locations = context.locations.Where(x => x.locationId == 2).ToList();
            return Json(comments.Select(x => new
            {
                commentId = x.commentId,
                commentText = x.commentText,
                userId = x.userId,
                pictureId = x.pictureId
                
            }),
                  JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [System.Web.Services.WebMethod]
        public ActionResult getUser(String userId)
        {
            var userInt = Convert.ToInt32(userId);
            var context = new dsimpsonEntities8();
            var users = context.users.Where(x => x.userId == userInt).ToList();
            //Where example:
            // var locations = context.locations.Where(x => x.locationId == 2).ToList();
            return Json(users.Select(x => new
            {
                userName = x.userName

            }),
                  JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [System.Web.Services.WebMethod]
        public ActionResult addComment(String userId, String pictureId, String commentText)
        {
            var comment = new comment();
            var context = new dsimpsonEntities8();

            comment.userId = Convert.ToInt32(userId);
            comment.pictureId = Convert.ToInt32(pictureId);
            comment.commentText = commentText;
            context.comments.Add(comment);
            context.SaveChanges();

            return View("ViewMap");
        }

        [Authorize]
        [System.Web.Services.WebMethod]
        public ActionResult removeComment(String commentId)
        {
            
            var context = new dsimpsonEntities8();
            var commentInt = Convert.ToInt32(commentId);
            var comment = context.comments.Where(x => x.commentId == commentInt).ToList();
            context.comments.Remove(comment[0]);
            context.SaveChanges();

            return View("ViewMap");
        }

        [Authorize]
        [System.Web.Services.WebMethod]
        public ActionResult removePicture(String pictureId, String locationId)
        {

            var context = new dsimpsonEntities8();
            var pictureInt = Convert.ToInt32(pictureId);
            var locationInt = Convert.ToInt32(locationId);

            var comment = context.comments.Where(x => x.pictureId == pictureInt).ToList();
            for (int index = 0; index < comment.Count; ++index)
            {
                context.comments.Remove(comment[index]);
            }
            context.SaveChanges();

            var picture = context.pictures.Where(x => x.pictureId == pictureInt).ToList();
            context.pictures.Remove(picture[0]);
            context.SaveChanges();

            picture = context.pictures.Where(x => x.locationId == locationInt).ToList();
            if (picture.Count == 0)
            {
                var location = context.locations.Where(x => x.locationId == locationInt).ToList();
                context.locations.Remove(location[0]);
                context.SaveChanges();
            }

            return View("ViewMap");
        }

        [Authorize]
            var context = new dsimpsonEntities5();
        [System.Web.Services.WebMethod]
        public ActionResult getPhotoByUser(String userId)
        {
            var context = new dsimpsonEntities8();
            var userInt = Convert.ToInt32(userId);

            var photos = context.pictures.Where(x => x.userId == userInt).ToList();

            return Json(photos.Select(x => new
            {
                pictureId = x.pictureId,
                caption = x.caption,
                locationId = x.locationId,
                userId  = x.userId,
                url = x.url
            }),

            JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [System.Web.Services.WebMethod]
        public ActionResult getUserId()
        {
            var userName = User.Identity.Name;
            var context = new dsimpsonEntities8();

            var user = context.users.Where(x => x.userName == userName).ToList();
            if( user.Count > 0 )
            {
                return Json(user.Select(x => new
                {
                    userId = x.userId
                }),

                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                var newUser = new user();

                newUser.userName = userName;
                newUser.firstName = "null";
                newUser.lastName = "null";
                context.users.Add(newUser);
                context.SaveChanges();

                return Json(JsonConvert.SerializeObject(new { userId = newUser.userId }));

            }

        }



    }

    
}
