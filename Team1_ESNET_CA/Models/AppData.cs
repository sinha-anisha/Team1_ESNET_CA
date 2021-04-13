using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1_ESNET_CA.Models
{
    public class AppData
    {

        public AppData()
        {
            Customers = new List<Customer>();
            Product = new List<Product>();

        }

        public List<Customer> Customers { get; set; }
        public List<Product> Product { get; set; }
    }
}
