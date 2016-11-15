using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class EventCircleTileDTO
    {
        public int NumberInComeEvent { get; set; }
        public int NumberActiveEvent { get; set; }
        public int NumberDoneEvent { get; set; }
        public int NumberBanEvent { get; set; }
        public int NumberAllEvent { get; set; }

        public EventCircleTileDTO()
        {
            NumberActiveEvent = 0;
            NumberDoneEvent = 0;
            NumberBanEvent = 0;
            NumberAllEvent = 0;
        }
    }
}