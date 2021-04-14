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
       
        

        public IActionResult Index(Order ProductName)
        {
            //Link Controller to model to Database
            List<Order> FinishedPdt = OrderData.getPdtInfo(ProductName);
                
                
                ViewData["pdtInfos"] = FinishedPdt;

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