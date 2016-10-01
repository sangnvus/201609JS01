using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class ThreadController : Controller
    {
        // GET: Thread
        [HttpPost]
        public ActionResult CreateDiscussion(CreateThreadInfo thread, IEnumerable<HttpPostedFileBase> Images)
        {
            WingS.Models.Thread newThread = null;
            //Add thread to DB
            using (var db = new ThreadDAL())
            {
                newThread = db.AddNewThread(thread);
            }
            //Add Imgaes of Discussion to server
            try
            {
                foreach (HttpPostedFileBase img in Images)
                {
                    //rebuild imgae name
                    string imageName = WsConstant.randomString() + Path.GetExtension(img.FileName).ToLower();
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Upload/ThreadImage"), imageName);
                    img.SaveAs(path);
                    string imgaeUrl = "Upload/ThreadImage/" + imageName;
                    //Add Image to db.
                    using (var db = new AlbumImageDAL())
                    {
                        db.AddNewAlbum(new ThreadAlbumImageDTO(newThread.ThreadId, imgaeUrl));
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