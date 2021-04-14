using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1_ESNET_CA.Models
{
    public class Product
    {
        public int Product_ID { get; set; }
        
        public string Product_Name { get; set; }

        public string Product_Image { get; set; }
        
        public string Product_Description { get; set; }

        public double Unit_Price { get; set; }

        public string Download_Link { get; set; }

        public List<string> Tags { get; set; }
    }
}
