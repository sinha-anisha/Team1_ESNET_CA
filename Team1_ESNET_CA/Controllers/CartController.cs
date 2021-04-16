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

        public IActionResult AddToCart(string Email, Product pdt, Cart c)
        {




            c.Total_Qty_Cart = c.Total_Qty_Cart + c.Quantity;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"insert into Cart (Cart_ID,Email,Total_Qty_Cart)
                                Values(@Cart_ID,@Email,@Total_Qty_Cart)";

                string sql1 = @"insert into Cart_Product(Cart_ID,Product_ID,Quantity)
                                Values(@Cart_ID,@Product_ID,@Quantity)";



                SqlCommand cmdWithEmail = new SqlCommand(sql, conn);
                SqlCommand cmdNoEmail = new SqlCommand(sql, conn);
                SqlCommand cmd3 = new SqlCommand(sql1, conn);
                //SqlCommand cmd4 = new SqlCommand(sql1, conn);


                /* cmdWithEmail.Parameters.AddWithValue("@Cart_ID", Cart_ID);
                 cmdWithEmail.Parameters.AddWithValue("@Email", c.Email);
                 cmdWithEmail.Parameters.AddWithValue("@Total_Qty_Cart", c.Total_Qty_Cart);*/

                /*cmdNoEmail.Parameters.AddWithValue("@Cart_ID", Cart_ID);
                cmdNoEmail.Parameters.AddWithValue("@Email", "NULL");
                cmdNoEmail.Parameters.AddWithValue("@Total_Qty_Cart", c.Total_Qty_Cart);
*/
                //cmd.ExecuteNonQuery();

                /* cmd3.Parameters.AddWithValue("@Cart_ID", Cart_ID);
                 cmd3.Parameters.AddWithValue("@Product_ID", pdt.Product_ID);
                 cmd3.Parameters.AddWithValue("@Quantity", c.Quantity);*/
                //cmd1.ExecuteNonQuery();
                string sessionId = Request.Cookies["sessionId"];

                if (sessionId != null)
                {
                    Session session = appData.Sessions.FirstOrDefault(x => x.Email == Email);
                    if (session != null)
                    {
                        c.Cart_ID = sessionId;
                        c.Email = Email;
                        cmdWithEmail.Parameters.AddWithValue("@Cart_ID", c.Cart_ID);
                        cmdWithEmail.Parameters.AddWithValue("@Email", c.Email);
                        cmdWithEmail.Parameters.AddWithValue("@Total_Qty_Cart", c.Total_Qty_Cart);

                        cmd3.Parameters.AddWithValue("@Cart_ID", c.Cart_ID);
                        cmd3.Parameters.AddWithValue("@Product_ID", pdt.Product_ID);
                        cmd3.Parameters.AddWithValue("@Quantity", c.Quantity);
                        cmdWithEmail.ExecuteNonQuery();
                        cmd3.ExecuteNonQuery();
                    }
                }
                else
                {
                    c.Cart_ID = Guid.NewGuid().ToString();
                    cmdNoEmail.Parameters.AddWithValue("@Cart_ID", c.Cart_ID);
                    cmdNoEmail.Parameters.AddWithValue("@Email", "NULL");
                    cmdNoEmail.Parameters.AddWithValue("@Total_Qty_Cart", c.Total_Qty_Cart);
                    cmd3.Parameters.AddWithValue("@Cart_ID", c.Cart_ID);
                    cmd3.Parameters.AddWithValue("@Product_ID", pdt.Product_ID);
                    cmd3.Parameters.AddWithValue("@Quantity", c.Quantity);
                    cmdNoEmail.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    conn.Close();
                }
                //cmd3.ExecuteNonQuery();
                ViewData["sessionId"] = Request.Cookies["sessionId"];
                ViewData["Total_Qty_Cart"] = c.Total_Qty_Cart;

            }
            return RedirectToAction("Index", "Gallery");
        }


    }
}


/*if (sessionId != null)
           {
               Customer customer = appData.Customers.FirstOrDefault(x => x.SessionId == sessionId );

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