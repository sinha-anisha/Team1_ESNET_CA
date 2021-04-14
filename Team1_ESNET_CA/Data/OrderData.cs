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
        protected static new readonly string connectionString = "Server=(local);Database=Necrosoft_Gen_Edited; Integrated Security=true";

        public static List<Order> getPdtInfo(Order ProductName)
        {
            List<Order> pdtInfos = new List<Order>();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //SQL string
                string sql = @"SELECT  Product_Name, Product_Image, Product_Description 
                                FROM Product WHERE Product_Name = " + ProductName; 

                //SQLCommand
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order pdtinfo = new Order()
                    {
                        //link to model var name
                        ProductName = (string)reader["Product_Name"],
                        ProductImg = (string)reader["Product_Image"],
                        ProductDesc = (string)reader["Product_Description"]
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


                string sql = @"SELECT COUNT(ProductID) FROM Order_Details 
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
