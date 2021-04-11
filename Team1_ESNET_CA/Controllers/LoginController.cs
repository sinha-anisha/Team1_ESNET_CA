using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Controllers
{
    public class LoginController : Controller
    {
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
            Customer cust = appData.Customers.Find(x => x.Username == username &&
                x.Password == password);

            if (cust == null)
            {
                ViewData["username"] = username;
                ViewData["errMsg"] = "No such user or incorrect password.";

                return View("Index");
            }
            else
            {
                cust.SessionId = Guid.NewGuid().ToString();
                Response.Cookies.Append("sessionId",cust.SessionId);

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
