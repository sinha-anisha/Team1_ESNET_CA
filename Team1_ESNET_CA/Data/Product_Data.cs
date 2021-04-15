using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Data
{
    public class Product_Data : DataConnection
    {
        public Product_Data()
        {
        }

        public static AppData InitAppData()
        {
            AppData appData = new AppData();

            //GetProducts(appData.Product);

            return appData;
        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT Product.Product_ID, Product.Product_Name,Product.Product_Image,Product.Product_Description,Product.Unit_Price, Product.Download_Link FROM Product";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Product_ID = (int)reader["Product_ID"],
                        Product_Name = (string)reader["Product_Name"],
                        Product_Image = (string)reader["Product_Image"],
                        Product_Description = (string)reader["Product_Description"],
                        Unit_Price = (double)reader["Unit_Price"],
                        Download_Link = (string)reader["Download_Link"]
                    };
                    products.Add(product);
                }
            }
            return products;
        }
       /* public static List<Product> AddProducts(Product pdt, int qty)
        {
            List<Product> products = new List<Product>();



            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"insert into Cart (Cart_ID,Product_ID,Email,Quantity)
                                Values(@Cart_ID,@Product_ID,@Email,@Quantity)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                Product product = new Product()
                {
                    cmd.Parameters.AddWithValue("@Cart_ID", ),
                    cmd.Parameters.AddWithValue("@Poduct_ID", pdt.Product_ID),
                    cmd.Parameters.AddWithValue("@Email", ),
                    cmd.Parameters.AddWithValue("@Quantity",qty)
                
                };
                products.Add(product);


                return products;
            }


        }*/
    }

}

   /* 
    cmd.Parameters.AddWithValue("@First_Name", cust.First_Name);
    cmd.Parameters.AddWithValue("@Last_Name", cust.Last_Name);
    cmd.Parameters.AddWithValue("@Password", cust.Password);
    cmd.Parameters.AddWithValue("@Mobile", cust.Mobile);

    int i = cmd.ExecuteNonQuery();
    if (i > 0)
    {
        ViewData["errMsg"] = "Registration Completed.";
        return RedirectToAction("Index", "Home");
    }
    else
    {
        ViewData["errMsg"] = "Registration NOT Completed.";
        return View();
    }
}*/