using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Team1_ESNET_CA.Data;

namespace Team1_ESNET_CA.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderData orderdata; //readonly immutable
        public OrderController( OrderData orderData)
        {
            this orderData = orderData; //OOPCS
        }
        public IActionResult Index()
        {
            //Validate the right user before display the details
            /* string sessionId = Request.Cookies["sessionId"];
             if (sessionId != null)
             {*/
                //Validate sessionId with User's sessionID
                //Check with Anisha if we are going to put in cookies
                //Confirm with Anisha if there is gonna be a central model for access to all models (eg. AppData)
                Customers customer = Customers.Find(x => x.sessionId == sessionId);
                if (sessionId == null)
                {
                    Console.WriteLine("Invalid Transaction, Please Login again");
                    return RedirectToAction ("Index", "Login")
                }
                //Extract image data from OrderData after validation
                ViewData["pdtImg"] = orderdata.ProductImg;
                ViewData["pdtName"] = orderdata.ProductName;
                ViewData["pdtDesc"] = orderdata.ProductDesc;
                ViewData["orderDate"] = orderdata.Orderdate;
                ViewData["orderQty"] = orderdata.OrderQuantity;
                ViewData["actCode"] = orderdata.ActivationCode;
                ViewData["sessionId"] = sessionId;

             return View("Index", "Order");*/

            return View();
        }
    }
}