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
        protected static readonly string connectionString = "Server=(local);Database=Necrosoft_14_04_21; Integrated Security=true";

        private readonly AppData appData;
        public static int flag = 0;
        public static int usercount = 0;
        

        public GalleryController(AppData appData)
        {
            this.appData = appData;
        }




        public IActionResult Index(string search, Product pdt, Cart c)
        {
            List<Session> sess = SessionData.GetAllSessions();
            List<Cart> products = ViewCartData.GetQuantity();
            List<Cart> product = ViewCartData.GetQuantityBeforeLogin();
            List<Product> prod = Product_Data.GetProducts();
           // List<Product> prodbefore = ViewCartData.GetQuantityBeforeLogin();

            ViewData["products"] = prod;
            var productlist = from s in Product_Data.GetProducts()
                              select s;
            if (!String.IsNullOrEmpty(search))
            {
                productlist = productlist.Where(s => s.Product_Name.Contains(search) || search == null);
            }

            string user = null;

            int flag1 = 0;
            string sessionId = Request.Cookies["sessionId"];
            string tempSession = "90821121-25ea-4303-9467-e62c71cf7c01";
          
            int Quan = 0;
            //int prod_id = 0;
            foreach (var s in sess)
            {
                if (s.Session_ID == sessionId)
                    user = s.Email;
            }
            
            
            
            
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"insert into [Cart_After_Login] (Email,Product_ID,Quantity)
                                Values(@Email,@Product_ID,@Quantity)";

                string sql1 = @"insert into [Cart_Before_Login] (Session_Cart_ID,Product_ID,Quantity)
                                Values(@Session_Cart_ID,@Product_ID,@Quantity)";

                string sql2 = @"select Session_ID,Email from Session";
                string sql5 = @"SELECT sum(Quantity) as sum FROM Cart_After_Login where Email='" + user + "'";
                string sql6 = @"SELECT sum(Quantity) as sum FROM Cart_before_Login";
                string sql7 = @"delete FROM Cart_before_Login";
                string sql8 = @"select * FROM Cart_before_Login";


                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                SqlCommand cmd5 = new SqlCommand(sql5, conn);
                SqlCommand cmd6 = new SqlCommand(sql6, conn);
                SqlCommand cmd7 = new SqlCommand(sql7, conn);
                SqlCommand cmd8 = new SqlCommand(sql8, conn);



                




               
                /*SqlDataReader reader = cmd8.ExecuteReader();

                while (reader.Read())
                {
                    Cart ct = new Cart()
                    {
                        Product_ID = (int)reader["Product_ID"],
                        Quantity = (int)reader["Quantity"]
                    };
                }*/
           






            if (flag > 0)
                {

                    if (sessionId != null)
                    {
                        if (user != null)
                        {
                           
                            //Update Quantity basis productid
                            int count = product.Count;
                            if (count == 0)
                            {
                                foreach (var v in products)
                                {
                                    if (v.Product_ID == c.Product_ID && user == v.Email)
                                    {
                                        c.Quantity += v.Quantity;
                                        flag = 1;
                                        string sql3 = @"update Cart_After_Login set Quantity=" + c.Quantity + " where Product_ID=" + c.Product_ID + "and Email='" + user + "'";
                                        SqlCommand cmd3 = new SqlCommand(sql3, conn);
                                        cmd3.ExecuteNonQuery();

                                    }
                                }
                                if (flag != 1)
                                {

                                    c.Email = user;
                                    cmd.Parameters.AddWithValue("@Email", c.Email);
                                    cmd.Parameters.AddWithValue("@Product_ID", c.Product_ID);
                                    cmd.Parameters.AddWithValue("@Quantity", c.Quantity);

                                    cmd.ExecuteNonQuery();
                                }

                            }

                            else
                            {
                                c.Email = user;
                                foreach (var v in products)
                                {
                                    foreach (var p in product)
                                    {
                                        if (v.Product_ID == p.Product_ID && v.Email==user)
                                        {
                                            flag1 = 99;
                                            //c.Quantity = c.Quantity + v.Quantity ;
                                           /* string sql3 = @"select Quantity from Cart_After_Login where Product_ID=" + p.productId + "and Email='" + user + "'";
                                            SqlCommand cmd3 = new SqlCommand(sql3, conn);

                                            int qty = cmd3.ExecuteNonQuery();*/
                                            c.Quantity =  v.Quantity + p.Quantity;

                                            string sql4 = @"update Cart_After_Login set Quantity=" + c.Quantity + " where Product_ID=" + p.Product_ID + "and Email='" + user + "'";
                                            SqlCommand cmd4 = new SqlCommand(sql4, conn);
                                            cmd4.ExecuteNonQuery();
                                        }
                                    }
                                }
                                if (flag1 != 99)
                                {
                                    foreach (var p in product)
                                    {

                                        cmd.Parameters.AddWithValue("@Email", user);
                                        cmd.Parameters.AddWithValue("@Product_ID",p.Product_ID);
                                        cmd.Parameters.AddWithValue("@Quantity", p.Quantity);

                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();
                                    }
                                }

                            }
                        }

                    }
                    else
                    {
                        foreach (var v in product)
                        {
                            if (v.Product_ID == c.Product_ID)
                            {
                                flag = 99;
                                c.Quantity = c.Quantity + v.Quantity;
                                string sql3 = @"update Cart_Before_Login set Quantity=" + c.Quantity + " where Product_ID=" + c.Product_ID;
                                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                                cmd3.ExecuteNonQuery();
                            }
                        }
                   
                        if (flag != 99)
                        {

                            cmd1.Parameters.AddWithValue("@Session_Cart_ID", tempSession);
                            cmd1.Parameters.AddWithValue("@Product_ID", c.Product_ID);
                            cmd1.Parameters.AddWithValue("@Quantity", c.Quantity);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            c.Quantity = 0;
                        }



                    }


                        if (user != null)
                        {
                            try
                            {
                                Quan += (int)cmd5.ExecuteScalar();
                            }
                            catch (Exception e)
                            {
                                c.Quantity = c.Quantity;
                            }

                        }

                        else if (flag > 0)
                        {
                            try
                            {
                                Quan += (int)cmd6.ExecuteScalar();
                            }
                            catch (Exception e)
                            {
                                Quan= c.Quantity;
                            }
                        }
                        else
                        {
                            //cmd7.ExecuteNonQuery();
                            Quan = 0;
                        }
                
                }
                else
                {
                    cmd7.ExecuteNonQuery();
                    
                    Quan = 0;
                }
            }

            
            ++flag;

            ViewData["qty"] = Quan.ToString();
            ViewData["sessionId"] = sessionId;
            
            return View(productlist.ToList());




        }


    }

}

    
