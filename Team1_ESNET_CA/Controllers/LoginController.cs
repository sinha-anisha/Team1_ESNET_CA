using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;

namespace Team1_ESNET_CA.Controllers
{
    public class LoginController : Controller
    {
       /* private List<CustomerData> customers;

        public LoginController()
        {

            customers = new List<CustomerData>();

            customers.Add(new CustomerData
            {
                Username = "abc",
                Password="abc"
            });

        }*/
        public IActionResult Index()
        {


           ViewData["Username"] = "abc";
           ViewData["Password"] = "abc";
          
            return View();
        }
    }
}
