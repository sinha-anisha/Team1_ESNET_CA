using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;


namespace Team1_ESNET_CA.Data
{
    public class OrderData : DataConnection
    {
        protected static new readonly string connectionString = "Server=(local);Database=Necrosoft_14_04_21; Integrated Security=true";
        public static List<Order> getOrderInfo(Order Order_ID)
        {
            List<Order> orderDetails = new List<Order>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //SQL string
                string sql = @"SELECT o.Order_Date, o.Quantity, od.Activation_Code
                               FROM [Product] AS p , [Order] AS O , [Order_Details] as OD
                                WHERE O.Order_ID = '124'";

                //SQLCommand
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order orderinfo = new Order()
                    {
                        OrderDate = (DateTime)reader["Order_Date"],
                        OrderQuantity = (int)reader["Quantity"]
                    };
                    orderDetails.Add(orderinfo);
                }
                conn.Close();
            }
            return orderDetails;
        }
        public static List<Product> getPdtInfo(Product Product_ID)
        {
            List<Product> pdtInfos = new List<Product>();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                //SQL string
                string sql = @"SELECT p.Product_Name, p.Product_Image, P.Product_Description
                               FROM [Product] AS p , [Order_Details] as OD
                                WHERE [OD].Product_ID = '100'"; 

                //SQLCommand
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product pdtinfo = new Product()
                    {
                        //link to model var name
                        Product_Name = (string)reader["Product_Name"],
                        Product_Image = (string)reader["Product_Image"],
                        Product_Description = (string)reader["Product_Description"]
                    };
                    pdtInfos.Add(pdtinfo);
                }

                conn.Close();
            }
            return pdtInfos;
        }


        public static List<string> getActCode(Order orderIden)
        {

            List<string> actcode = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();


                string sql = @"SELECT COUNT(ProductID) FROM [Order_Details] 
                            WHERE Order_ID = " + orderIden;

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                        for (int i = 0; i < (int)reader["ProductID"]; i++)
                        {
                            var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", ""); 
                            
                            actcode.Add(uid);
                        }
                };
                conn.Close();
            }
             return actcode;
        }
    } 
}
