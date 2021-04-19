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




        
        public static List<Cart> GetQuantity()
        {
            List<Cart> products = new List<Cart>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"select Email,Product_ID,Quantity from Cart_After_Login";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cart product = new Cart()
                    {
                        Email= (string)reader["Email"],
                        Product_ID = (int)reader["Product_ID"],
                        Quantity = (int)reader["Quantity"],
                    };
                    products.Add(product);
                }
            }

            return products;

        }

        public static List<Cart> GetQuantityBeforeLogin()
        {
            List<Cart> products = new List<Cart>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"select Session_Cart_ID,Product_ID,Quantity from Cart_Before_Login";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cart product = new Cart()
                    {
                        Session_Cart_ID= (string)reader["Session_Cart_ID"],
                        Product_ID = (int)reader["Product_ID"],
                        Quantity = (int)reader["Quantity"],
                    };
                    products.Add(product);
                }
            }

            return products;

        }
    }
}


