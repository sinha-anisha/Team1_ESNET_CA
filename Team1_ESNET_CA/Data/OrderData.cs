using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;


namespace Team1_ESNET_CA.Data
{
    public class OrderData : DataConnection
    {
        protected static new readonly string connectionString = "Server=(local);Database=Necrosoft_Gen_Edited; Integrated Security=true";

        public List<Order> getPdtInfo(string ProductDesc)
        {
            List<Order> pdtInfos = new List<Order>();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //SQL string
                string sql = @"SELECT  Product_Name, Product_Image, Product_Description FROM Product";
                //WHERE Product_Name = " + pdtInfos.ProductName; //KIV

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


        public List<Order> GetActCode()
        {

            List<Order> actCodes = new List<Order>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();


                string sql = @"SELECT Quantity FROM Order ";
                //WHERE Product_ID = " + actCodes.ProductID;

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order actCode = new Order();

                    for (int i = 0; i < (int)reader["Quantity"]; i++)
                    {
                        string genActCode = CreateActivationKey();
                        var actKeyExist = GetActivationKeys().Any(key =>  genActCode.Equals(key));
                        if (actKeyExist)
                        {
                            genActCode = CreateActivationKey();
                        }
                    }
                    actCodes.Add(actCode);
                }
                conn.Close();
            }

            return actCodes;

        }


        private string CreateActivationKey()
        {
            var actKey = Guid.NewGuid().ToString();
            return actKey;
        }
        private string GetActivationKeys()
        {
            string valuekey = "";
           return valuekey;
        }
    } 
    }
