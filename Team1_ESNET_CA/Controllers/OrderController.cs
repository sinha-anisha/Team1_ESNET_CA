using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Controllers
{
    public class OrderController : Controller
    {



        public IActionResult Index(Session e)
        {
            string sessionId = Request.Cookies["sessionId"];

            if (sessionId != null)
            {
                //validate with customer email before retrieving product infomation

                //Link Controller to model to Database
                List<Order> FinishedPdt = OrderData.getPdtInfo(e);

                ViewData["orderInfos"] = FinishedPdt;

                return View();
            }
            // No username is found in session, so the user needs to login
            return RedirectToAction("Index", "Login");
        }
        public IActionResult getActCode(Order prodId , Order ordId)
        {
            List<Order> orderdetail = OrderData.generateActCode(prodId, ordId);
            //ViewData["orderdetails"] = orderdetail; may not need a viewdata.
            return RedirectToAction("index", "order");
        }

    }
}