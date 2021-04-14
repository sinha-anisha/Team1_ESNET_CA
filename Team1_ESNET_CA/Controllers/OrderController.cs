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
       
        

        public IActionResult Index(Product ProductID)
        {
         string sessionId = Request.Cookies["sessionId"];

         if (sessionId == null)
         {
            // No username is found in session, so the user needs to login
            return RedirectToAction("Index", "Login");
         }
            //Link Controller to model to Database
            List<Product> FinishedPdt = OrderData.getPdtInfo(ProductID);

                ViewData["pdtInfos"] = FinishedPdt;

            return RedirectToAction("getOrderdetails");
        }
        public IActionResult getOrderdetails (Order Order_ID)
        {
            List<Order> orderDetails = OrderData.getOrderInfo(Order_ID);

            ViewData["orderInfos"] = orderDetails;
            return View();
        }
        public IActionResult getActCode(Order orderIden)
        {
            List<string> actCode = OrderData.getActCode(orderIden);
            ViewData["actCodes"] = actCode;
            return View();
        }

    }
}