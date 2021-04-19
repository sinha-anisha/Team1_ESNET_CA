
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;

namespace CA.Controllers
{
    public class ViewCartController : Controller
    {
        protected static readonly string connectionString = "server=(local); Database=Necrosoft_14_04_21; Integrated Security=true";





        public IActionResult Index(Cart c)
        {
            List<Product> products = Product_Data.GetProducts();
            List<Cart> cartAfter = ViewCartData.GetQuantity();
            List<Cart> cartBefore = ViewCartData.GetQuantityBeforeLogin();
            List<Cart> viewcart = new List<Cart>();
            List<Session> sess = new List<Session>();
            List<ViewCartProduct> vc = new List<ViewCartProduct>();
            ViewData["products"] = products;

            string sessionId = Request.Cookies["sessionId"];





            string user = null;

            foreach (var s in sess)
            {
                if (s.Session_ID == sessionId)
                    user = s.Email;
            }


            if (user != null)
            {
                viewcart = ViewCartData.GetQuantity();
            }
            else
            {
                viewcart = ViewCartData.GetQuantityBeforeLogin();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT Product.Product_ID, Product.Product_Name,Product.Product_Image,
                            Product.Product_Description,Product.Unit_Price,
                            Product.Download_Link FROM Product where Product.Product_ID=" + c.Product_ID;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Product_ID = (int)reader["Product_ID"],
                        Product_Name = (string)reader["Product_Name"],
                        Product_Image = (string)reader["Product_Image"],
                        Product_Description = (string)reader["Product_Description"],

                        Unit_Price = (double)reader["Unit_Price"],

                        Download_Link = (string)reader["Download_Link"]
                    };
                    products.Add(product);
                }
            }

            foreach (var q in products)
            {
                foreach (var t in viewcart)
                {
                    if (q.Product_ID == t.Product_ID)
                    {
                        ViewCartProduct viewcartprod = new ViewCartProduct()
                        {
                            productId = t.Product_ID,
                            productImage = q.Product_Image,
                            productDescription = q.Product_Description,
                            unitPrice = q.Unit_Price,
                            Quantity = t.Quantity

                        };
                        vc.Add(viewcartprod);

                    }

                }


                ViewData["Products"] = products;
                ViewData["viewcartprod"] = vc;

                
            }

            return View();

        }




            public IActionResult changeQuantity(Cart c)
            {
                List<Session> sess = SessionData.GetAllSessions();

                string sessionId = Request.Cookies["sessionId"];




                List<Cart> products = new List<Cart>();
                string user = null;

                foreach (var s in sess)
                {
                    if (s.Session_ID == sessionId)
                        user = s.Email;
                }


                if (user != null)
                {
                    products = ViewCartData.GetQuantity();
                }
                else
                {
                    products = ViewCartData.GetQuantityBeforeLogin();
                }




                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();



                    string sql = @"update Cart_After_Login set Quantity =" + c.Quantity + "Email=" + user;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    string sql1 = @"update Cart_Before_Login set Quantity =" + c.Quantity + " and Product_ID=" + c.Product_ID;
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);


                    foreach (var prod in products)
                    {
                        if (prod.Email == user)
                        {
                            cmd.ExecuteNonQuery();

                        }

                    }

                    if (user == null)
                        cmd1.ExecuteNonQuery();


                    ViewData["prodbefore"] = products;
                    ViewData["prod"] = products;

                    return View();
                }
            }
        
    }
}

    





