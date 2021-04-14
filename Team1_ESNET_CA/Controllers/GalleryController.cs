using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Models;
using Team1_ESNET_CA.Data;

namespace Team1_ESNET_CA.Controllers
{
    public class GalleryController : Controller
    {
        private readonly AppData appData;

        public GalleryController(AppData appData)
        {
            this.appData = appData;
        }

        public IActionResult Index()
        {
            List<Product> products = Product_Data.GetProducts();
            List<Product> productsn = Product_Data.GetProducts();
            ViewData["products"] = products;
            ViewData["productsn"] = productsn;

            // to highlight "Office" as the selected menu-item
            

            // use sessionId to determine if user has already logged in
            string sessionId = HttpContext.Request.Cookies["sessionId"];
            if (sessionId != null)
            {
                Customer customer = appData.Customers.Find(x => x.SessionId == sessionId);
                if (customer == null)
                    return RedirectToAction("Index", "Logout");

                ViewData["sessionId"] = sessionId;
                
            }

            return View("Gallery");
        }
    }
}
