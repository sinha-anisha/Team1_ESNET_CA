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
            //Link Controller to model to Database
            //List<Order> FinishedPdt = OrderData.getPdtInfo();
                
              
            string[] pdtImg =
            {
                "/img/VSLogo.png",
                "/img/NodeJSLogo.png"
            };
                
                //ViewData["pdtInfos"] = FinishedPdt;
                ViewData["pdtImg"] = pdtImg;
            return View();
        }
        public IActionResult getActCode()
        {
           // List<Order> actCode = OrderData.getActCode();
            return View();
        }

    }
}