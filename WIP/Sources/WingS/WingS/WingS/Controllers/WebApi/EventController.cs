using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WingS.DataAccess;
using WingS.DataHelper;
using WingS.Models;
using WingS.Models.DTOs;

namespace WingS.Controllers
{
    public class EventController : ApiController
    {
        //Get top view event
        [HttpGet]
        public IHttpActionResult GetTopFourViewedEvent()
        {
            List<Event> topEvent = null;
            var basicEventList = new List<EventBasicInfo>();
            using (var db = new EventDAL())
            {
                //Get top event.
                topEvent = db.GetTopEventByView(4);
                foreach(Event e in topEvent)
                {
                    //Lấy ra ảnh tương ứng với mỗi 1 event với Status = 1
                    //Note: ảnh có status bằng 1 là ảnh dùng để hiển thị trên trang Home
                    var eventMainImage = db.GetMainImageEventById(e.EventID);

                    basicEventList.Add(new EventBasicInfo
                    {
                        EventID = e.EventID,
                        EventName = e.EventName,
                        Content = e.Description,
                        CreatorID = e.CreatorID,
                        ImageUrl = eventMainImage.ImageUrl,
                        Status = e.Status
                    });
                }
            }
            return Ok( new HTTPMessageDTO { Status= WsConstant.HttpMessageType.SUCCESS, Data= basicEventList });
        }
    }
}
