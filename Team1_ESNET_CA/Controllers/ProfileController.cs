using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;
using System.Data.SqlClient;

namespace Team1_ESNET_CA.Controllers
{
    public class ProfileController : Controller
    {
        protected static readonly string connectionString = "Server=(local);Database=Necrosoft_14_04_21; Integrated Security=true";
        private readonly AppData appData;

        public ProfileController(AppData appData)
        {
            this.appData = appData;
        }


        public IActionResult  Index()
        {
            List<Session> sess = SessionData.GetAllSessions();
            List<Customer> customers = CustomerData.GetAllCustomers(appData.Customers);
            string sessionId = Request.Cookies["sessionId"];
            string username = "";
            foreach (var s in sess)
            {
                if (s.Session_ID == sessionId)
                    username = s.Email;
            }
            Customer cust = appData.Customers.FirstOrDefault(x => x.Username == username);

            if (cust != null)
            {
                foreach (var c in customers)
                {
                    if (c.Email == username)
                    {
                        ViewData["Username"] = username;
                        ViewData["First_Name"] = c.First_Name;
                        ViewData["Last_Name"] = c.Last_Name;
                        ViewData["Password"] = c.Password;
                        ViewData["Mobile"] = c.Mobile;
                    };

                }
                //ViewData["sessionId"] = sessionId;
                ViewData["Total"] = "20000";
                return View();
            }
            else
            {
                return View();
            }
        }

        public void btnUpdate_Click()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"insert into Customer (First_Name,Last_Name,Mobile) Values (@txtFName,@txtLName,@txtMobile)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                
            }
        }
        
    }
    
}
