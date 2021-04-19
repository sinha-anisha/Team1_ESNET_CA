using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Controllers
{
    public class RegisterController : Controller
    {
       // protected static readonly string connectionString = "Server=(local);Database=CA_DB; Integrated Security=true";
        protected static readonly string connectionString = "Server=(local);Database=Necrosoft_LAST; Integrated Security=true";
        private readonly AppData appData;

        public RegisterController(AppData appData)
        {
            this.appData = appData;
        }


        public IActionResult Index()
        {
            return View();
        }

      
          public ActionResult InsertData(Customer cust,string Email)
        {

            Customer c = appData.Customers.FirstOrDefault(x => x.Username == Email);

            string sessionId = HttpContext.Request.Cookies["sessionId"];

            if (c != null)
            {

                ViewData["errMsg"] = "User already Exists.";

                return View("Index");
            }
            else
            {


                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"insert into Customer (Email,First_Name,Last_Name,Password,Mobile)
                                Values(@Email,@First_Name,@Last_Name,@Password,@Mobile)";

                    SqlCommand cmd = new SqlCommand(sql, conn);


                    cmd.Parameters.AddWithValue("@Email", cust.Email);
                    cmd.Parameters.AddWithValue("@First_Name", cust.First_Name);
                    cmd.Parameters.AddWithValue("@Last_Name", cust.Last_Name);
                    cmd.Parameters.AddWithValue("@Password", cust.Password);
                    cmd.Parameters.AddWithValue("@Mobile", cust.Mobile);

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        ViewData["errMsg"] = "Registration Completed.";
                        ViewData["sessionId"] = sessionId;
                        return RedirectToAction("Index", "Gallery");
                    }
                    else
                    {
                        ViewData["errMsg"] = "Registration NOT Completed.";
                        ViewData["sessionId"] = sessionId;
                        return View();
                    }
                }

            }  

          
        }

    }
}
