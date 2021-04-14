using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1_ESNET_CA.Models
{
    public class Cart
    {
        public string Cart_ID { get; set; }
        public int Product_ID { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }
    }
}
