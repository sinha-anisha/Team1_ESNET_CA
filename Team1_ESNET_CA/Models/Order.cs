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
        //Model get;set; for Order Controller/View
        //Parameters we need
        public string ProductImg { get; set;}
        public string ProductName { get; set;}
        public string ProductDesc { get; set;}
        public string OrderDate { get;set;}
        public int OrderQuantity { get; set;}
        public string ActivationCode { get; set;}
        public string SessionId { get;set;}
        public string Email { get;set;}
        public string ProductID { get;set;}
    }
}
