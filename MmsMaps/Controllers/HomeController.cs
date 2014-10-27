using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;

namespace MmsMaps.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendMap(string fromAddress, string toAddress, string toPhoneNumber)
        {
            string accountsid = "[YOUR_TWILIO_ACCOUNT_SID]";
            string authtoken = "[YOUR_TWILIO_AUTH_TOKEN]";
            string fromPhoneNumber = "[YOUR_TWILIO_PHONE_NUMBER]";

            var client = new TwilioRestClient(accountsid, authtoken);
            var result = client.SendMessage(
                fromPhoneNumber,
                toPhoneNumber,
                null,
                new string[] {
                    string.Format("http://dev.virtualearth.net/REST/v1/Imagery/Map/Road/Routes?wp.0={0};64;1&wp.1={1};66;2&key=[YOUR_BING_MAPS_KEY]", Server.UrlEncode(fromAddress), Server.UrlEncode(toAddress))
                });

            if (result.RestException != null)
            {
                return Json(result.RestException);
            }

            return Json(true);
        }
    }
}