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
   
    public class CartController : Controller
    {
        protected static readonly string connectionString = "Server=(local);Database=Necrosoft_14_04_21; Integrated Security=true";

        private readonly AppData appData;

        public CartController(AppData appData)
        {
            this.appData = appData;
        }


       

       
        public IActionResult AddToCart( Customer cust , Product pdt,Cart c)
        {

            string Cart_ID = "";
            string uname = "";
            string sessionId = HttpContext.Request.Cookies["sessionId"];

           
            if (sessionId != null)
            {
                Customer customer = appData.Customers.FirstOrDefault(x => x.SessionId == sessionId );

                if (customer != null)
                {
                    Cart_ID = sessionId;

                    // uname = HttpContext.Request.ge
                    uname = cust.Username;
                }
                else
                {
                    Cart_ID = Guid.NewGuid().ToString();
                }

            }
            else
            {
                Cart_ID = Guid.NewGuid().ToString();
            }

            int qty = c.Quantity;

           // return RedirectToAction("ActionName", "ControllerName", new { userId = id });

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"insert into Cart (Cart_ID,Product_ID,Quantity)
                                Values(@Cart_ID,@Product_ID,@Quantity)";

                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@Cart_ID", Cart_ID);
                cmd.Parameters.AddWithValue("@Product_ID",c.Product_ID);
                //cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Quantity", c.Quantity);
                cmd.ExecuteNonQuery();


            }
            return View("Index");
        }


    }
}
