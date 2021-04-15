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

          
            c.Total_Qty_Cart = c.Total_Qty_Cart + c.Quantity;

           



            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"insert into Cart (Cart_ID,Email,Total_Qty_Cart)
                                Values(@Cart_ID,@Email,@Total_Qty_Cart)";

                string sql1 = @"insert into Cart_Product(Cart_ID,Product_ID,Quantity)
                                Values(@Cart_ID,@Product_ID,@Quantity)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd1 = new SqlCommand(sql1, conn);


                cmd.Parameters.AddWithValue("@Cart_ID", Cart_ID);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Total_Qty_Cart", c.Total_Qty_Cart);
                
                cmd.ExecuteNonQuery();



                cmd1.Parameters.AddWithValue("@Cart_ID", Cart_ID);
                cmd1.Parameters.AddWithValue("@Product_ID", pdt.Product_ID);
                cmd1.Parameters.AddWithValue("@Quantity", c.Quantity);
                cmd1.ExecuteNonQuery();

                ViewData["Total_Qty_Cart"] = c.Total_Qty_Cart;

            }
            return RedirectToAction("Index", "Gallery");
        }


    }
}

/*if (sessionId != null)
{
    Customer customer = appData.Customers.FirstOrDefault(x => x.SessionId == sessionId);

    if (customer != null)
    {
        Cart_ID = sessionId;
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
}*/