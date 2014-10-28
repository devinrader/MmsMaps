using System;
using System.Collections.Generic;
using System.Configuration;
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
            ViewBag.BestBuyKey = ConfigurationManager.AppSettings["BestBuyKey"];
            return View();
        }

        public ActionResult SendMap(string fromAddress, string toAddress, string toPhoneNumber)
        {
            string accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
            string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"]; ;
            string fromPhoneNumber = ConfigurationManager.AppSettings["TwilioFromNumber"];

            var client = new TwilioRestClient(accountSid, authToken);
            var result = client.SendMessage(
                fromPhoneNumber,
                toPhoneNumber,
                null,
                new string[] {
                    string.Format("http://dev.virtualearth.net/REST/v1/Imagery/Map/Road/Routes?wp.0={0};64;1&wp.1={1};66;2&key={2}", Server.UrlEncode(fromAddress), Server.UrlEncode(toAddress), ConfigurationManager.AppSettings["BingMapsKey"])
                });

            if (result.RestException != null)
            {
                return Json(result.RestException);
            }

            return Json(true);
        }
    }
}