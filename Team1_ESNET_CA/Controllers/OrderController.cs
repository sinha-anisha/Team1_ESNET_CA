using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1_ESNET_CA.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            //Create var and link it up to SQL via (Folder.ActionName) = OrderData.GetOrderDetails(Any Parameters?)
            //ViewData
            return View();
        }
    }
}
