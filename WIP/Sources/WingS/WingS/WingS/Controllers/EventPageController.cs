using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class EventPageController : Controller
    {
        [HttpPost]
        public ActionResult CreateEvent(CreateEventInfo eventInfo, IEnumerable<HttpPostedFileBase> Images)
        {
            Event newEvent = null;
            //Add thread to DB
            using (var db = new EventDAL())
            {
                //newEvent = db.AddNewThread(eventInfo);
            }
            //Add Imgaes of Discussion to server
            try
            {
                foreach (HttpPostedFileBase img in Images)
                {
                    //rebuild imgae name
                    string imageName = WsConstant.randomString() + Path.GetExtension(img.FileName).ToLower();
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Upload"), imageName);
                    img.SaveAs(path);
                    string imgaeUrl = "Content/Upload/" + imageName;
                    //Add Image to db.
                    using (var db = new AlbumImageDAL())
                    {
                        db.AddNewAlbum(new ThreadAlbumImageDTO(newEvent.EventID, imgaeUrl));
                    }

                }
            }
            catch (Exception)
            {
                return Redirect("/#/Error");
            }
            return Redirect("/#/Home");
        }
    }
}