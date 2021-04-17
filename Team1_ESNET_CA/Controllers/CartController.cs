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

        public IActionResult AddToCart(Product pdt, Cart c)
        {
            List<Session> sess = SessionData.GetAllSessions();
            List<ViewCartProduct> products = ViewCartData.GetQuantity();


            c.Total_Quantity = c.Total_Quantity + c.Quantity;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"insert into [Cart_After_Login] (Email,Product_ID,Quantity)
                                Values(@Email,@Product_ID,@Quantity)";

                string sql1 = @"insert into [Cart_Before_Login] (Session_Cart_ID,Product_ID,Quantity)
                                Values(@Session_Cart_ID,@Product_ID,@Quantity)";

                string sql2 = @"select Session_ID,Email from Session";




                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);


                string user = "";
                string sessionId = Request.Cookies["sessionId"];

                foreach (var s in sess)
                {
                    if (s.Session_ID == sessionId)
                        user = s.Email;
                }

                if (sessionId != null)
                {
                    if (user != null)
                    {
                        //Update Quantity basis productid

                        foreach (var v in products)
                        {
                            if (v.productId == c.Product_ID)
                            {
                                c.Quantity = c.Quantity + v.Quantity;
                                string sql3 = @"update Cart_After_Login set Quantity=" + c.Quantity + " where Product_ID=" + c.Product_ID;
                                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                                cmd3.ExecuteNonQuery();

                            }
                            else
                            {
                                c.Email = user;
                                cmd.Parameters.AddWithValue("@Email", c.Email);
                                cmd.Parameters.AddWithValue("@Product_ID", pdt.Product_ID);
                                cmd.Parameters.AddWithValue("@Quantity", c.Quantity);

                                cmd.ExecuteNonQuery();
                            }


                        }

                    }

                    else
                    {
                        //Update Quantity basis productid
                        foreach (var v in products)
                        {
                            if (v.productId == c.Product_ID)
                            {
                                c.Quantity = c.Quantity + v.Quantity;
                                string sql3 = @"update Cart_Before_Login set Quantity=" + c.Quantity + " where Product_ID=" + c.Product_ID;
                                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                                cmd3.ExecuteNonQuery();
                            }
                            else
                            {

                                c.Session_Cart_ID = sessionId;
                                cmd1.Parameters.AddWithValue("@Session_Cart_ID", c.Session_Cart_ID);
                                cmd1.Parameters.AddWithValue("@Product_ID", pdt.Product_ID);
                                cmd1.Parameters.AddWithValue("@Quantity", c.Quantity);
                                cmd1.ExecuteNonQuery();

                            }

                        }

                    }
                }
                else
                {
                    c.Session_Cart_ID = Guid.NewGuid().ToString();
                    cmd1.Parameters.AddWithValue("@Session_Cart_ID", c.Session_Cart_ID);
                    cmd1.Parameters.AddWithValue("@Product_ID", pdt.Product_ID);
                    cmd1.Parameters.AddWithValue("@Quantity", c.Quantity);
                    cmd1.ExecuteNonQuery();

                }

                    ViewData["Total_Qty_Cart"] = c.Total_Quantity;

                    return RedirectToAction("Index", "Gallery");
                

            }
        }
    }
}

