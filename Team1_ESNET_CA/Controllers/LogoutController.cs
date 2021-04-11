using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Controllers
{
    public class LogoutController : Controller
    {

        private readonly AppData appData;

        public LogoutController(AppData appData)
        {
            this.appData = appData;
        }
        public IActionResult Index()
        {
            // remove the user's sessionId from our record
            string sessionId = Request.Cookies["sessionId"];
            Customer cust = appData.Customers.Find(x => x.SessionId == sessionId);
            if (cust != null)
                cust.SessionId = null;  // denote user has logged off

            // remove cookie on user's browser
            Response.Cookies.Delete("sessionId");

            // direct user back to our default gallery
            return RedirectToAction("Index", "Home");
        }
    }
}
