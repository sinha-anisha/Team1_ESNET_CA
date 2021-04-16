using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Data
{
    public class ViewCartData
    {
        protected static readonly string connectionString = "Server=(local);Database=Necrosoft_14_04_21; Integrated Security=true";

        public static List<ViewCartProduct> GetAllProduct()
        {
            List<ViewCartProduct> products = new List<ViewCartProduct>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT p.[Product_ID], p.[Product_Name], p.[Product_Image], p.[Product_Description], p.[Unit_Price], c.[Product_ID], c.[Quantity], c.[Cart_ID]
                                FROM [Product] AS p,[Cart_Product] AS c
                                WHERE p.[Product_ID] =c.[Product_ID]" ;

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ViewCartProduct product = new ViewCartProduct()
                    {
                        productId = (int)reader["Product_ID"],
                        productNmae = (string)reader["Product_Name"],
                        productImage = (string)reader["Product_Image"],
                        productDescription = (string)reader["Product_Description"],
                        unitPrice = (double)reader["Unit_Price"],
                        Quantity = (int)reader["Quantity"],
                        CartId = (string)reader["Cart_ID"]
                    };
                    products.Add(product);
                }
            }

            return products;

        }


    }
}

