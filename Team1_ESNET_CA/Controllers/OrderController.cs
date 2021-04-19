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
        public IActionResult Index()
        {
            List<Order> actCodes = new List<Order>();
            string sessionId = "SOMETHING MOCK"; //Request.Cookies["sessionId"];
            if (sessionId != null)
            {
                //foreach (Cart c in cart)
                //{
                //    Session sessionid = appData.Sessions.FirstOrDefault(x => x.Email == c.Email);
                //    if (sessionid != null)
                //    {
                List<Order> cartdetail = OrderData.addToOrder();
                string orderdetail = OrderData.generateActCode(cartdetail);
                List<Order> actCode = OrderData.getActCode();
                List<Order> allPdtDetails = OrderData.getPdtInfo(orderdetail);
                //ViewData["actCodes"] = actCodes;
                ViewData["actCodes"] = actCode;
                ViewData["orderInfos"] = allPdtDetails;
                return View();
                //Redirect to Index Action 
                //    }
                //}
            }
            if (sessionId == null)
                return View("Index", "Cart");
            return RedirectToAction("Index", "Order");

        }
    }
}