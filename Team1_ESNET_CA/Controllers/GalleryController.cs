using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Models;
using Team1_ESNET_CA.Data;
using System.Data.SqlClient;


namespace Team1_ESNET_CA.Controllers
{
    public class GalleryController : Controller
    {

        private readonly AppData appData;

        public GalleryController(AppData appData)
        {
            this.appData = appData;
        }

        

        public IActionResult Index(string search)
        {

            List<Product> products = Product_Data.GetProducts();
            List<Product> productsn = Product_Data.GetProducts();
            ViewData["products"] = products;

            var productlist = from s in Product_Data.GetProducts()
                              select s;
            if (!String.IsNullOrEmpty(search))
            {
                productlist = productlist.Where(s => s.Product_Name.Contains(search) || search == null);
            }
            string sessionId = HttpContext.Request.Cookies["sessionId"];
            if (sessionId != null)
            {
                Customer customer = appData.Customers.Find(x => x.SessionId == sessionId);
                if (customer == null)
                    return RedirectToAction("Index", "Logout");

                ViewData["sessionId"] = sessionId;
                
            }
            
            return View(productlist.ToList());
        }


    }
}
