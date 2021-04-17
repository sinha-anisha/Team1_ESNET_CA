/*using Microsoft.AspNetCore.Mvc;
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



        public IActionResult Index(Session email)
        {
            string sessionId = Request.Cookies["sessionId"];

            if (sessionId == null)
            {
                // No username is found in session, so the user needs to login
                return RedirectToAction("Index", "Login");
            }
            //Link Controller to model to Database
            List<Order> FinishedPdt = OrderData.getPdtInfo(email);

            ViewData["orderInfos"] = FinishedPdt;

            return View();
        }
        public IActionResult getActCode(Cart productId, Cart orderId)
        {
            List<string> actCode = OrderData.generateActCode(productId, orderId);
            ViewData["actCodes"] = actCode;
            return View();
        }

    }
}*/
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
        private readonly AppData appData;

        public OrderController(AppData appData)
        {
            this.appData = appData;
        }
        public IActionResult getActCode()
        {
            Cart cart = new Cart();
            string sessionId = "SOMETHING MOCK"; //Request.Cookies["sessionId"];
            if (sessionId != null)
            {
                //foreach (Cart c in cart)
                //{
                //    Session sessionid = appData.Sessions.FirstOrDefault(x => x.Email == c.Email);
                //    if (sessionid != null)
                //    {
                        List<Order> cartdetail = OrderData.addToOrder(cart);
                        OrderData.generateActCode(cartdetail);
                        //Redirect to Index Action 
                //    }
                //}
            }
            if (sessionId == null)
                return View("Index", "Cart");
            return RedirectToAction("Index", "Order");

        }
        public IActionResult Index()
        {
            Order orderdetails = new Order();
            List<Order> allPdtDetails = OrderData.getPdtInfo(orderdetails);
            ViewData["orderInfos"] = allPdtDetails;
            return View();
        } 
    }
}