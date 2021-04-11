using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Util
{
    public class Helper
    {

        public Helper()
        {
        }

        public static AppData InitAppData()
        {
            AppData appData = new AppData();

            AddCustomers(appData.Customers);
            
            return appData;
        }

        public static void AddCustomers(List<Customer> cust)
        {
            string[] names = { "Ani", "Sam" };

            if (cust == null)
                return;

            foreach (string name in names)
            {
                Customer customer = new Customer()
                {
                    UserId = Guid.NewGuid().ToString(),
                    Username = name,
                    Password = name
                };

                
                cust.Add(customer);
            }
        }

       
    }
}

