namespace WingS.Models.DTOs
{
    public class NganLuongCheckOut
    {
        public string option_payment { get; set; }
        public string bankcode { get; set; }
        public string buyer_fullname { get; set; }
        public string buyer_email { get; set; }
        public string buyer_mobile { get; set; }
        public string numberMoney { get; set; }
        public string inputMoney { get; set; }
        public string DonateContent { get; set; }
        public string isPublic { get; set; }
        public int DonateEventId { get; set; }
        public int DonateUserId { get; set; }

        public NganLuongCheckOut()
        {
            option_payment = "";
            bankcode = "";
            buyer_fullname = "";
            buyer_email = "";
            buyer_mobile = "";
            numberMoney = "";
            inputMoney = "";
            DonateContent = "";
            isPublic = "";
            DonateEventId = 0;
            DonateUserId = 0;
        }
    }
}