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

        public GalleryController(AppData appData)
        {
            this.appData = appData;
        }

      
        
        public IActionResult Index(string search,int Quantity)
        {

             List<Customer> customers = new List<Customer>();
            List<Cart> cart = new List<Cart>();
            List<Session> sess = SessionData.GetAllSessions();
            List<Product> products = Product_Data.GetProducts();
            List<Product> productsn = Product_Data.GetProducts();
            int Quan = Quantity;

            ViewData["products"] = products;
            var productlist = from s in Product_Data.GetProducts()
                              select s;
            if (!String.IsNullOrEmpty(search))
            {
                productlist = productlist.Where(s => s.Product_Name.Contains(search) || search == null);
            }
            string username = "Guest";
            String Email = null;
           
            string sessionId = HttpContext.Request.Cookies["sessionId"];

           

            foreach (var s in sess)
            {
                if (s.Session_ID == sessionId)
                {
                    Email = s.Email;
                }
            }

           
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT First_Name FROM Customer where Email='"+Email+"'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer cust = new Customer()
                    {
                        First_Name = (string)reader["First_Name"],
                    };
                    customers.Add(cust);
                }
            }
            foreach (var cs in customers)
            {
                username = cs.First_Name;
            }

           
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT sum(Quantity) as sum FROM Cart_After_Login where Email='" + Email + "'";
                    string sql1 = @"SELECT sum(Quantity) as sum FROM Cart_before_Login";
                    string sql2 = @"delete FROM Cart_before_Login";
                   


                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                   
               

                if (Email != null)
                {
                    try
                    {
                        Quan += (int)cmd.ExecuteScalar();
                    }catch(Exception e)
                    {
                        Quan = Quantity;
                    }
                    
                }

                else if (flag > 0)
                {
                    try
                    {
                        Quan += (int)cmd1.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        Quan = Quantity;
                    }
                }
                else
                {
                    cmd2.ExecuteNonQuery();
                    Quan = 0;
                }
            }

                 ++flag;

                ViewData["sessionId"] = sessionId;
                ViewData["UserName"] = username;
                ViewData["qty"] = Quan.ToString();

             
            return View(productlist.ToList());
        }


        public IActionResult Index1(string search, int Quantity)
        {
            TempData["temp"] = 20;
            TempData.Keep();
            return View();
        }


        }
}


