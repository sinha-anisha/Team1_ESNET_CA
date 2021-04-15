using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Controllers
{
    public class LoginController : Controller
    {
        protected static readonly string connectionString = "Server=(local);Database=Necrosoft_14_04_21; Integrated Security=true";

        private readonly AppData appData;

        public LoginController(AppData appData)
        {
            this.appData = appData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Authenticate(string username, string password)
        {
            Customer cust = appData.Customers.FirstOrDefault(x => x.Username == username &&
                x.Password == password);

            if (cust == null)
            {
                ViewData["username"] = username;
                ViewData["errMsg"] = "No such user or incorrect password.";

                return View("Index");
            }
            else
            {
                cust.Email = username;
                Session session = new Session()
                {
                    Session_ID = Guid.NewGuid().ToString(),
                    TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds()
                };


                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"insert into Session(Session_ID,Email,TimeStamp)
                                Values(@Session_ID,@Email,@TimeStamp)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@Session_ID", session.Session_ID);
                    cmd.Parameters.AddWithValue("@Email", cust.Email);
                    cmd.Parameters.AddWithValue("@TimeStamp", session.TimeStamp);
                    cmd.ExecuteNonQuery();

                    Response.Cookies.Append("sessionId", session.Session_ID);
                }
                ViewData["sessionId"] = Request.Cookies["sessionId"];

            }
            return RedirectToAction("Index", "Gallery");

        }
    }
}
