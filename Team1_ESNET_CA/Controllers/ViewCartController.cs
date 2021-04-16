using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Controllers
{
    public class ViewCartController : Controller
    {
        protected static readonly string connectionString = "Server=(local);Database=Necrosoft_14_04_21; Integrated Security=true";

        private readonly AppData appData;

        public ViewCartController(AppData appData)
        {
            this.appData = appData;
        }

        public IActionResult Index()
        {
            List<ViewCartProduct> products = ViewCartData.GetAllProduct();

            ViewData["products"] = products;

            return View();
        }
        public IActionResult CheckOut(List<Cart> c)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //insert into Order
                string sql = @"INSERT INTO Orders (Order_ID, Order_Date, Email, Quantity)
                               VALUES(@Order_ID, @Order_Date, @Email, @Quantity)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                //check if user is log in
                string session = Request.Cookies["sessionId"];
                if (session != null)
                {
                    foreach (Cart cart in c)
                    {
                        Session sessionid = appData.Sessions.FirstOrDefault(x => x.Email == cart.Email);
                        if (sessionid != null)
                        {
                            //Add to Order Table and Order Detail Tables
                            cmd.Parameters.AddWithValue("@Order_ID", cart.Cart_ID);
                            //cmd.Parameters.AddWithValue("@Order_Date", cart.Date); Check if cart table have date
                            cmd.Parameters.AddWithValue("@Email", cart.Email);
                            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                            cmd.ExecuteNonQuery();
                            return RedirectToAction("getActCode", "Order");
                        }
                    }

                }
                conn.Close();
            }
            return RedirectToAction("index", "Login");
        }
    }
}
