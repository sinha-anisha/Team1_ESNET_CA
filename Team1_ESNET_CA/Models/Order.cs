using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;

namespace Team1_ESNET_CA.Models
{
    public class Order
    {
        //Parameters for Product Table
        public string Product_Img { get; set; }
        public string Product_Name { get; set; }
        public string Product_Desc { get; set; }
        public string Product_Summ { get; set; }

        //Paramteres for Order Table
        public DateTime Order_Date { get; set; }
        public int Order_Quantity { get; set; }
        public string Session_ID { get; set; }
        public string Email { get; set; }

        //Parameters for Order_Details Table
        public int Product_ID { get; set; }
        public string Activation_Code { get; set; }
        public string Order_ID { get; set; }
    }
}
