using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;


namespace Team1_ESNET_CA.Data
{
    public class OrderData
    {
        //attribute 
        //link database to model
        public List<Order> getPdtInfo()
        {
            List<Order> pdtInfos = new List<Order>();
            //constructor
            string connectionstring = "Server = DESKTOP-S80ANN8" +
                "Database = Necrosoft1" + "Integrated Security = true ";
                //sqlconnection 
            Using (SQLConnection conn = new SQLConnection(connectionstring))
                {
                conn.Open();

                    //SQL string
                    string sql = "SELECT  Product_Name, Product_Image, Product_Description"
                    "FROM PRODUCT"
                    "WHERE Product_Name = " + pdtInfos.ProductName; //KIV

                    //SQLCommand
                    SQLCommand cmd = new SQLCommand(sql,conn);
                    SQLDataReader reader = cmd.ExecuteReader();
                        
                    while(reader.Read())
                    {
                        Order pdtinfo = new Order()
                        {
                            //link to model var name
                            ProductName = (string)reader["Product_Name"]
                            ProductImg = (string)reader["Product_Image"]
                            ProductDesc = (string)reader["Product_Description"]
                        };
                        pdtInfos.Add(pdtinfo);
                    }
                conn.Close();
                }
                        return pdtInfos;
        }
        public List<Order> getActCode()
        {
            //create a list to store the keys in Order.Models
            List<Order> actCodes = new List<Order>();
            //start SQL Search 
            string connectionstring = "Server = DESKTOP-S80ANN8"
                + "Database = Necrosoft1" + "Intergrated Security =True ";

            //Use SQL Connection
            using(SQLConnection conn = new SQLConnection(connectionstring))
            {
                conn.Open()

                    //SQL Line
                    string sql = "SELECT Quantity"
                    "FROM Order"
                    "WHERE Product_ID = " + actCodes.ProductID;
                
                    SQLCommand cmd = new SQLCommand(sql,conn);
                    SQLDataReader reader = (int32)cmd.ExecuteScalar();

                    while(reader.Read())
                    {
                        Order actCode = new Order()
                        {
                            for ( int i = 0; i < (int)reader["Quantity"]; i ++)
                            {
                                string genActCode = CreateActivationKey();

                                //Validate duplicate keys in system
                                var actKeyExist = GetActivationKeys().Any(key => key == genActCode);
                                if (actKeyExist)
                                {
                                    genActCode = CreateActivationKey();
                                }
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
            return valuekey;
        }
    }
}
