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
        protected static readonly string connectionString = "Server=DESKTOP-SMR4OLJ; Database=NewDatabase; Integrated Security=true";

        public static List<ViewCartProduct> GetAllProduct()
        {
            List<ViewCartProduct> products = new List<ViewCartProduct>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT Q.[Product_ID], Q.[Product_Name], Q.[Product_Image], Q.[Product_Description], Q.[Unit_Price], C.[Product_ID], C.Quantity
                                FROM [Quantity] AS Q,[Cart]
                                WHERE Q.[Product_ID] =C.[Product_ID]";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ViewCartProduct product = new ViewCartProduct()
                    {
                        productNmae = (string)reader["Product_Name"],
                        productImage = (string)reader["Product_Image"],
                        productDescription = (string)reader["Product_Description"],
                        unitPrice = (double)reader["Unit_Price"],
                        Quantity = (int)reader["Quantity"]
                    };
                    products.Add(product);
                }
            }

            return products;

        }


    }
}

