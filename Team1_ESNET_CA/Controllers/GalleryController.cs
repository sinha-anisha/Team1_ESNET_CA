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
        protected static readonly string connectionString = "Server=(local);Database=Necrosoft_Gen_Edited; Integrated Security=true";

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
            //ViewData["productsn"] = productsn;

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


        public IActionResult AddToCart(Product pdt, int qty)
        {

            string Cart_ID = "";
            string sessionId = HttpContext.Request.Cookies["sessionId"];


            if (sessionId != null)
            {
                Customer customer = appData.Customers.Find(x => x.SessionId == sessionId);

                if (customer != null)
                {
                     Cart_ID = sessionId;
                }
                else
                {
                     Cart_ID = Guid.NewGuid().ToString();
                }

            }

            List<Product> products = Product_Data.GetProducts();
            List<Cart> carts = new List<Cart>();

            ViewData["products"] = products;
          


            // use sessionId to determine if user has already logged in
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"insert into Cart (Cart_ID,Product_ID,Email,Quantity)
                                Values(@Cart_ID,@Product_ID,@Email,@Quantity)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                Cart cart = new Cart()
                {
                    cmd.Parameters.AddWithValue("@Cart_ID",Cart_ID),
                    cmd.Parameters.AddWithValue("@Poduct_ID", pdt.Product_ID),
                    cmd.Parameters.AddWithValue("@Email", ),
                    cmd.Parameters.AddWithValue("@Quantity",qty)

                };

                carts.Add(cart);
                 ViewData["Carts"] = carts;

                return View("Gallery");
            }
        }

       

    }
}
