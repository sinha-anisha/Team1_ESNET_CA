using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Team1_ESNET_CA.Controllers
{
    public class ViewCartController : Controller
    {
        public ViewCartController(ViewCartData viewCartData) 
        {
            this.ViewCartData = viewCartData;
        }

        public IActionResult Index()
        {
            List<ViewCartProduct> products = ViewCartData.GetAllProduct();

            ViewData["products"] = products;

            return View();
        }


    }
}
