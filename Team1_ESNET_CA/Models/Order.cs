using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1_ESNET_CA.Models
{
    public class Order
    {
        //Model get;set; for Order Controller/View
        //Parameters we need
        //Check if anisha if we are doing LINQ or EntityFrameWork
        //Way of extracting data and creating model will be different. 
        public string ProductImg { get; set;}
        public string ProductName { get; set;}
        public string ProductDesc { get; set;}
        public string OrderDate { get;set;}
        public int OrderQuantity { get; set;}
        public string ActivationCode { get; set;}
        public string SessionId { get;set;}
        public string Email { get;set;}
    }
    }
}
