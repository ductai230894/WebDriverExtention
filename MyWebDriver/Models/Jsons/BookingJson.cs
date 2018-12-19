using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Models.Jsons
{

    public class CheckInDate
    {
        public string ISO { get; set; }
        public string i18n { get; set; }
        public string time { get; set; }
    }

    public class CheckOutDate
    {
        public string ISO { get; set; }
        public string i18n { get; set; }
        public string time { get; set; }
    }

    public class Dates
    {
        public CheckInDate checkInDate { get; set; }
        public CheckOutDate checkOutDate { get; set; }
    }

    public class Image
    {
        public string description { get; set; }
        public string url { get; set; }
    }

    public class Url
    {
        public string key { get; set; }
        public string url { get; set; }
    }

    public class Actions
    {
        public List<Url> urls { get; set; }
        public List<object> texts { get; set; }
        public List<object> chats { get; set; }
    }

    public class GuestName
    {
        public bool isLead { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Booking
    {
        public bool cancelled { get; set; }
        public string confNumber { get; set; }
        public Dates dates { get; set; }
        public string url { get; set; }
        public string address { get; set; }
        public string hotel { get; set; }
        public Image image { get; set; }
        public Actions actions { get; set; }
        public bool highlight { get; set; }
        public GuestName guestName { get; set; }
    }

    public class WebToApp
    {
        public string url { get; set; }
        public string visibility { get; set; }
    }

    public class Pagination
    {
        public int elementsPerPage { get; set; }
        public int start { get; set; }
        public int total { get; set; }
    }

    public class BookingJson
    {
        public List<Booking> bookings { get; set; }
        public WebToApp webToApp { get; set; }
        public Pagination pagination { get; set; }
    }
}
