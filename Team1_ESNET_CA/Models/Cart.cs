using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1_ESNET_CA.Models
{
    public class Cart
    {
        public string Session_Cart_ID { get; set; }
        public int Product_ID { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }
        public int Total_Quantity { get; set; }
        public double Checkout_total { get; set; }
      

    }
}
