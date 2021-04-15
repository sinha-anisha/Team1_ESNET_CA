using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1_ESNET_CA.Models
{
    public class ViewCartProduct
    {   
        public int productId { get; set; }
        public string productNmae { get; set; }
        public string productImage { get; set; }
        public string productDescription { get; set; }
        public double unitPrice { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
