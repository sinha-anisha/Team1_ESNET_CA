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
            List<ViewCartProduct> product = ViewCartData.GetQuantityBeforeLogin();


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
                int flag = 0;
                string sessionId = Request.Cookies["sessionId"];
                string tempSession= "90821121-25ea-4303-9467-e62c71cf7c01";

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
                            if (v.productId == c.Product_ID && user== v.Email)
                            {
                                c.Quantity += v.Quantity;
                                flag = 1;
                                string sql3 = @"update Cart_After_Login set Quantity=" + c.Quantity + " where Product_ID=" + c.Product_ID + "and Email='" + user+"'";
                                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                                cmd3.ExecuteNonQuery();

                            }
                        }
                        if (flag!=1)
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
                    foreach (var v in product)
                    {
                        if (v.productId == c.Product_ID)
                        {
                            flag = 1;
                            c.Quantity = c.Quantity + v.Quantity;
                            string sql3 = @"update Cart_Before_Login set Quantity=" + c.Quantity + " where Product_ID=" + c.Product_ID;
                            SqlCommand cmd3 = new SqlCommand(sql3, conn);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                    if (flag != 1)
                    {
                        
                        cmd1.Parameters.AddWithValue("@Session_Cart_ID", tempSession);
                        cmd1.Parameters.AddWithValue("@Product_ID", pdt.Product_ID);
                        cmd1.Parameters.AddWithValue("@Quantity", c.Quantity);
                        cmd1.ExecuteNonQuery();
                    }
                }

                    ViewData["Total_Qty_Cart"] = c.Quantity;


                    return RedirectToAction("Index", "Gallery");
                

            }
        }
    }
}

