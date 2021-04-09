using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjectionWshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Team1_ESNET_CA.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderData orderdata; //readonly immutable
        public OrderController( OrderData orderdata)
        {
            this orderdata = orderdata; //OOPCS
        }
        public IActionResult Index()
        {
            //Validate the right user before display the details
            string sessionId = Request.Cookies["sessionId"];
            if(sessionId != null)
            {
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
                ViewData["OrderImg"] = orderdata.ProductImg;
                ViewData["OrderName"] = orderdata.ProductName;
                ViewData["OrderDesc"] = orderdata.ProductDesc;
                ViewData["OrderDate"] = orderdata.Orderdate;
                ViewData["OrderNum"] = orderdata.OrderQuantity;
                ViewData["ActCode"] = orderdata.ActivationCode;
                ViewData["sessionId"] = sessionId;
            }

            return View("Index", "Order");
        }
    }
}
