using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Models;
using System.IO;
using System.Text.RegularExpressions;


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

            //AddGallery(appData.Product);

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


        public static void AddGallery(List<Product> Product_Image, string filename)
        {
            if (Product_Image == null)
                return;

            
            string[] lines = File.ReadAllLines(Product_Image + "/" + filename);
            foreach (string line in lines)
            {
                string[] pair = line.Split(";");
                if (pair.Length != 2)
                    continue;   // not what we expected; skip

                Regex regex = new Regex("https://images.unsplash.com/photo-(.*)\\?w=350");
                Match match = regex.Match(pair[0]);
                string Product_ID = match.Groups[1].ToString();

                Product product = new Product()
                {
                    Product_ID = Product_ID,
                    Download_Link = pair[0],
                    
                    Tags = new List<string>(pair[1].Split(","))
                };
                Product_Image.Add(product);
            }
        }


    }
}

