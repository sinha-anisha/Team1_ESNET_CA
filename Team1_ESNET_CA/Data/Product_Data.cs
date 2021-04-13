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

            GetProducts(appData.Product);

            return appData;
        }

        public static List<Product> GetProducts(List<Product> products)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT Product.Product_Name,Product.Product_Image,Product.Product_Description,Product.Unit_Price, Product.Download_Link FROM Product";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Product_ID = (string)reader["Product_ID"],
                        Product_Name = (string)reader["Product_Name"],
                        Product_Image = (string)reader["Product_Image"],
                        Product_Description = (string)reader["Product_Description"],
                        Unit_Price = (float)reader["Unit_Price"],
                        Download_Link = (string)reader["Download_Link"]
                    };
                    products.Add(product);
                }
            }
            return products;
        }
        /*public static void AddGallery(List<Product> products, string filename)
        {
            if (products == null)
                return;

            string[] lines = File.ReadAllLines("SeedData" + "/" + filename);
            foreach (string line in lines)
            {
                string[] pair = line.Split(";");
                if (pair.Length != 2)
                    continue;   // not what we expected; skip

                Regex regex = new Regex("https://images.unsplash.com/photo-(.*)\\?w=350");
                Match match = regex.Match(pair[0]);
                string photoId = match.Groups[1].ToString();

                Photo photo = new Photo()
                {
                    Id = photoId,
                    Url = pair[0],
                    Tags = new List<string>(pair[1].Split(","))
                };

                photos.Add(photo);
            }*/
    }
}
