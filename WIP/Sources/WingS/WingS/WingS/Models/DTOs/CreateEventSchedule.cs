using System.Web.Mvc;

namespace WingS.Models.DTOs
{
    public class CreateEventSchedule
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Description { get; set; }

        public CreateEventSchedule()
        {
            FromDate = "";
            ToDate = "";
            Description = "";
        }
        public CreateEventSchedule(string from,string to, string des)
        {
            FromDate = from;
            ToDate = to;
            Description = des;
        }
    }
}