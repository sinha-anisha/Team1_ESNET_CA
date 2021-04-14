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
        public static List<Order> getPdtInfo(Product Product_ID)
        {
            List<Order> orderInfos = new List<Order>();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                //SQL string
                string sql = @"SELECT p.Product_Name, p.Product_Image, p.Product_Description, o.Order_Date, o.Quantity, od.Activation_Code
                                FROM Product AS p, [Order] AS o, Order_Details AS od
                                WHERE p.Product_ID = od.Product_ID
                                AND o.Order_ID = od.Order_ID
                                AND o.Order_ID = '124'"; 

                //SQLCommand
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order orderInfo = new Order()
                    {
                        //link to model var name
                        ProductName = (string)reader["Product_Name"],
                        ProductImg = (string)reader["Product_Image"],
                        ProductDesc = (string)reader["Product_Description"],
                        OrderDate = (DateTime)reader["Order_Date"],
                        OrderQuantity = (int)reader["Quantity"],
                        ActivationCode = (string)reader["Activation_Code"]
                    };
                    orderInfos.Add(orderInfo);
                }

                conn.Close();
            }
            return orderInfos;
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
