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
        public static List<string> generateActCode(shoppingCartItem shoppingCartID, shoppingCartItem shoppingProductID)
        {

            List<string> actcodes = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();


                string sql = @"SELECT COUNT(Product_ID) FROM [Cart_Product] 
                            WHERE Order_ID = " + shoppingCartID;

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < (int)reader["Product_ID"]; i++)
                    {
                        var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

                        string sql1 = @"INSERT INTO Order_Details (@Activation_Code,@Order_ID,@Product_ID)
                                VALUES ( @Activation_Code, @Order_ID, @Product_ID)";

                        SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            cmd1.Parameters.AddWithValue("@Activation_Code", uid);
                            cmd1.Parameters.AddWithValue("@Order_ID", shoppingCartID);
                            cmd1.Parameters.AddWithValue("@Product_ID", shoppingProductID);
                    }
                };
                conn.Close();
            }
            return actcodes;
        }
        public static List<Order> getPdtInfo(session Email )
        {
            List<Order> orderInfos = new List<Order>();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                //SQL string
                string sql = @"SELECT p.Product_Name, p.Product_Image, p.Product_Description, o.Order_Date,od.OrderID,od.Product_ID , od.Activation_Code
                                FROM Product AS p, [Order] AS o, Order_Details AS od
                                WHERE p.Product_ID = od.Product_ID
                                AND o.Order_ID = od.Order_ID
                                GROUP BY od.OrderID
                                HAVING o.Email = " + Email; 
               

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
                        OrderQuantity = (int)reader["Product_ID"],
                        ActivationCode = (string)reader["Activation_Code"],
                    };
                    orderInfos.Add(orderInfo);
                }

                conn.Close();
            }
            return orderInfos;
        }
    } 
}
