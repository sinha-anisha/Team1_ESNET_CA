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
            string email = null;
            List<Order> actCodes = new List<Order>();
                List<Session> sess = SessionData.GetAllSessions();

                string sessionId = Request.Cookies["sessionId"];

                foreach (var s in sess)
                {
                    if (s.Session_ID == sessionId)
                        email = s.Email;
                }
            if (sessionId != null)
            {
                List<Order> cartdetail = OrderData.addToOrder();
                email = OrderData.generateActCode(cartdetail);
                //If user i
                List<Order> actCode = OrderData.getActCode(email);
                List<Order> allPdtDetails = OrderData.getPdtInfo(email);
                //ViewData["actCodes"] = actCodes;
                ViewData["actCodes"] = actCode;
                ViewData["orderInfos"] = allPdtDetails;
                return View();
            }
            else
                return View("Index", "Cart");
            return View();


        }
        public IActionResult fromPurchaseHistory()
        {
            List<Cart> cart = new List<Cart>();
            string email = null;
            List<Session> sess = SessionData.GetAllSessions();

            string sessionId = Request.Cookies["sessionId"];

            foreach (var s in sess)
            {
                if (s.Session_ID == sessionId)
                    email = s.Email;
            }
            List<Order> actCode = OrderData.getActCode(email);
            List<Order> allPdtDetails = OrderData.getPdtInfo(email);
            ViewData["actCodes"] = actCode;
            ViewData["orderInfos"] = allPdtDetails;
            return View("Index");
        }

        }
    }