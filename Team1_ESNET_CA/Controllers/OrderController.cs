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
        /* This section is not done yet!!
         
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
            }
            */
            public IActionResult Index()
            {
            string[] pdtImg =
            {
                "/img/VSLogo.png",
                "/img/NodeJSLogo.png"
            };

            string[] pdtName =
            {
                "Microsoft Visual Studio",
                "Node JS"
            };

            string[] pdtDesc =
            {
                "Lorem Ipsum",
                "Lorem Ipsum"
            };

            string[] orderDate =
            {
                "9 April 2021",
                "26 December 2020"
            };

            int[] orderQty =
            {
                2,
                1
            };

            /*Not sure how to generate random activation code..??
            string[] actCode =
            {

            }
            */

            //Extract image data from OrderData after validation
            ViewData["pdtImg"] = pdtImg;
            ViewData["pdtName"] = pdtName;
            ViewData["pdtDesc"] = pdtDesc;
            ViewData["orderDate"] = orderDate;
            ViewData["orderQty"] = orderQty;

            return View();
        }
    }
}