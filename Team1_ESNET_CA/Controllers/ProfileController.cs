using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Controllers
{
    public class ProfileController : Controller
    {
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
        
    }
    
}
