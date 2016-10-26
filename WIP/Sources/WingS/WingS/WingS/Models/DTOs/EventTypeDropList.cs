using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WingS.Models.DTOs
{
    public class EventTypeDropList
    {
        public List<SelectListItem> EventTypeList { get; set; }
        public Int16 Selected { get; set; }

        public EventTypeDropList()
        {
            EventTypeList = new List<SelectListItem>();
            Selected = 0;
        }
    }
}