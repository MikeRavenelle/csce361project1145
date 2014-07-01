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
            var context = new dsimpsonEntities4();
            test.latitude = 5;
            test.longitude = 6;
            context.locations.Add(test);
            context.SaveChanges();


            //Do stuff for uploading, no idea what yet.
            return View("ViewMap");

        }

        [Authorize]
        public string[] extractLocation(Bitmap image)
        {
            PropertyItem[] propItems = image.PropertyItems;

            
            ASCIIEncoding encodings = new ASCIIEncoding();
            string latRef ="0";
            String[] table = new String[2];
            table[0] = " ";
            table[1] = " ";
            float latDegrees = 0;
            float latMinutes = 0;
            float latSeconds = 0;
            float longDegrees = 0;
            float longMinutes = 0;
            float longSeconds = 0;
            try
            {
                //latitude Ref prop[40]

                latRef = encodings.GetString(propItems[40].Value);

                
                //Latitude 
                uint latDegreesNumerator = BitConverter.ToUInt32(propItems[41].Value, 0);
                uint latDegreesDenominator = BitConverter.ToUInt32(propItems[41].Value, 4);
                 latDegrees = latDegreesNumerator / (float)latDegreesDenominator;

                uint latMinutesNumerator = BitConverter.ToUInt32(propItems[41].Value, 8);
                uint latMinutesDenominator = BitConverter.ToUInt32(propItems[41].Value, 12);
                 latMinutes = latMinutesNumerator / (float)latMinutesDenominator;

                uint latSecondsNumerator = BitConverter.ToUInt32(propItems[41].Value, 16);
                uint latSecondsDenominator = BitConverter.ToUInt32(propItems[41].Value, 20);
                 latSeconds = latSecondsNumerator / (float)latSecondsDenominator;

            }
            catch
            {
                Console.WriteLine("Error in latitude ");
            }
             
            try
            {
                //Longitude Ref prop[42]
                string longRef = encodings.GetString(propItems[42].Value);

           //longitude 
                uint longDegreesNumerator = BitConverter.ToUInt32(propItems[43].Value, 0);
                uint longDegreesDenominator = BitConverter.ToUInt32(propItems[43].Value, 4);
                 longDegrees = longDegreesNumerator / (float)longDegreesDenominator;

                uint longMinutesNumerator = BitConverter.ToUInt32(propItems[43].Value, 8);
                uint longMinutesDenominator = BitConverter.ToUInt32(propItems[43].Value, 12);
                 longMinutes = longMinutesNumerator / (float)longMinutesDenominator;

                uint longSecondsNumerator = BitConverter.ToUInt32(propItems[43].Value, 16);
                uint longSecondsDenominator = BitConverter.ToUInt32(propItems[43].Value, 20);
                 longSeconds = longSecondsNumerator / (float)longSecondsDenominator;
            }
            catch
            {
                Console.WriteLine("Error in longitude");
            }

            //table 0 is latitude; table 1 is longitude 
           table[0] = latDegrees +" "+latMinutes+" "+latSeconds;
           table[1] = longDegrees + " " + longMinutes + " " + longSeconds;

           var context = new dsimpsonEntities4();
           var location = context.locations.Where(x => x.longitude == table[1] && x.latitude == table[0]).ToList();
           if(location.Count > 0)
           {
               return location.locationId;
           }
           else
           {
               var newLocation = new locations();
               newLocation.longitude = table[1];
               newLocation.latitude = table[0];
               context.users.Add(newLocation);
               context.SaveChanges();
               
               return newLocation.locationId;
            }
        }

        [Authorize]
        public ActionResult getLocations()
        {

            var context = new dsimpsonEntities4();
            var locations = context.locations.ToList();
            //Where example:
           // var locations = context.locations.Where(x => x.locationId == 2).ToList();
            return Json(locations.Select(x => new
            {
                id = x.locationId,
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
            var context = new dsimpsonEntities4();

            var locations = context.locations.Where(x => x.locationId == locationInt).ToList();
                
            return Json(locations.Select(x => new
            {
                
                locationId = x.locationId,
                longitude = x.longitude,
                latitude = x.latitude
            }),
                  JsonRequestBehavior.AllowGet);
        }
        

        [System.Web.Services.WebMethod]
        public ActionResult getPictures(String locationId)
        {
            var locationInt = Convert.ToInt32(locationId);
            System.Diagnostics.Debug.WriteLine(locationId);
            var context = new dsimpsonEntities4();
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
            var context = new dsimpsonEntities4();
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
            var context = new dsimpsonEntities4();
            var users = context.users.Where(x => x.userId == userInt).ToList();
            //Where example:
            // var locations = context.locations.Where(x => x.locationId == 2).ToList();
            return Json(users.Select(x => new
            {
                firstName = x.firstName,
                lastName = x.LastName

            }),
                  JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [System.Web.Services.WebMethod]
        public ActionResult addComment(String userId, String pictureId, String commentText)
        {
            var comment = new comment();
            var context = new dsimpsonEntities4();

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
            
            var context = new dsimpsonEntities4();
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

            var context = new dsimpsonEntities4();
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


        [System.Web.Services.WebMethod]
        public ActionResult getPhotoByUser(String userId)
        {
            var context = new dsimpsonEntities4();
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

        [System.Web.Services.WebMethod]
        public ActionResult getUserId()
        {
            var userName = User.Identity.Name;
            var context = new dsimpsonEntities4();

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
                newUser.LastName = "null";
                context.users.Add(newUser);
                context.SaveChanges();

                return Json(JsonConvert.SerializeObject(new { userId = newUser.userId }));

            }

        }



    }

    
}
