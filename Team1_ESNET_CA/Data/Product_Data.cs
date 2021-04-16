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
     
    }

}

   