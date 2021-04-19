/*using Microsoft.AspNetCore.Mvc;
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
        public static List<string> generateActCode(Cart Order_ID, Cart Product_ID)
        {


            List<string> actcodes = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SQL string
                string sql = @"SELECT p.Product_Name, p.Product_Image, p.Product_Description, o.Order_Date, o.Quantity, od.Activation_Code
                                FROM Product AS p, [Order] AS o, Order_Details AS od
                                WHERE p.Product_ID = od.Product_ID
                                AND o.Order_ID = od.Order_ID
                                AND o.Order_ID = '124'";

                string sql = @"SELECT COUNT(Product_ID) FROM [Cart_Product] 
                            WHERE Order_ID = " + Order_ID;

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order orderInfo = new Order()
                    {
                        var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

                    string sql1 = @"INSERT INTO Order_Details (@Activation_Code,@Order_ID,@Product_ID)
                                VALUES ( @Activation_Code, @Order_ID, @Product_ID)";

                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@Activation_Code", uid);
                    cmd1.Parameters.AddWithValue("@Order_ID", Order_ID);
                    cmd1.Parameters.AddWithValue("@Product_ID", Product_ID);
                }
            };
            conn.Close();
        }
            return actcodes;
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
*/
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Diagnostics;
using Team1_ESNET_CA.Models;
using Team1_ESNET_CA.Data;

namespace Team1_ESNET_CA.Data
{
    public class OrderData : DataConnection
    {
        private static string orderid = null;
        private static string OEmail = null;
        public static List<Order> addToOrder(string email)
        {
            List<Order> addToCarts = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand("", conn);
                conn.Open();
                cmd.CommandText = @"SELECT Product_ID, Email,Quantity FROM Cart_After_Login WHERE Email = @Email";
                cmd.Parameters.AddWithValue("@Email", email);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order addToCart = new Order()
                    {
                        Product_ID = (int)reader["Product_ID"],
                        Email = (string)reader["Email"],
                        Order_Quantity = (int)reader["Quantity"],
                    };
                    addToCarts.Add(addToCart);
                }
                conn.Close();
                return addToCarts;
            }
        } //Cart to Order
        public static string generateActCode(List<Order> orderdetail) // Insert from Order to Order details
        {
            //Need do validation on current Order_Id with session_ID
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                conn.Open();
                cmd.CommandText = @"INSERT INTO [Order] (Order_ID,Order_Date,Email,Product_ID,Quantity)
                                VALUES (@Order_ID, @Order_Date, @Email,@Product_ID,@Quantity)";
                var createNewID = Guid.NewGuid().ToString();
                foreach (var o in orderdetail)
                {
                    o.Order_ID = createNewID;
                    o.Order_Date = DateTime.Now;
                    cmd.Parameters.AddWithValue("@Order_ID", o.Order_ID);
                    cmd.Parameters.Add("@Order_Date", SqlDbType.Date);
                    cmd.Parameters["@Order_Date"].Value = o.Order_Date;
                    cmd.Parameters.AddWithValue("@Email", o.Email);
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int);
                    cmd.Parameters["@Product_ID"].Value = o.Product_ID;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int);
                    cmd.Parameters["@Quantity"].Value = o.Order_Quantity;
                    cmd.ExecuteNonQuery();
                    OEmail = o.Email;
                    orderid = o.Order_ID;
                    cmd.Parameters.Clear();
                };
                conn.Close();
                conn.Open();
                try
                {
                    cmd.CommandText = @"DELETE FROM Cart_After_Login WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("@Email", OEmail);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error");
                }
                conn.Close();
                conn.Open();
                foreach (var o in orderdetail)
                {
                    for (int i = 0; i < o.Order_Quantity; i++)
                    {
                        var actCode = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
                        o.Activation_Code = actCode;
                        cmd.CommandText = @"INSERT INTO Order_Details (Activation_Code,Order_ID,Product_ID)
                                 VALUES ( @Activation_Code, @Order_ID, @Product_ID)";
                        cmd.Parameters.AddWithValue("@Activation_Code", o.Activation_Code);
                        cmd.Parameters.AddWithValue("Order_ID", o.Order_ID);
                        cmd.Parameters.Add("@Product_ID", SqlDbType.Int);
                        cmd.Parameters["@Product_ID"].Value = o.Product_ID;
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                };
                conn.Close();
            }
            return OEmail;
        }
        public static List<Order> getActCode(string email)
        {
            List<Order> actcodes = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                conn.Open();
                cmd.CommandText = @"SELECT DISTINCT(OD.Product_ID), OD.Activation_Code FROM Order_Details AS OD, [Order] AS O
                                    WHERE OD.Order_ID = o.Order_ID
                                    AND o.Email = @Email";
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order actcode = new Order()
                    {
                        Product_ID = (int)reader["Product_ID"],
                        Activation_Code = (string)reader["Activation_Code"]
                    };
                    actcodes.Add(actcode);
                }
            }
            return actcodes;
        }
        public static List<Order> getPdtInfo(string email)
        {
            List<Order> orderInfos = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //SQL string
                string sql = @"SELECT od.Product_ID,p.Product_Name,p.Product_Image,p.Product_Description,o.Order_Date,o.Quantity, p.Product_Summary
                                FROM [Order] AS o, Order_Details AS od, Product AS p
                                WHERE od.Product_ID = o.Product_ID
                                AND p.Product_ID = o.Product_ID
                                AND o.Order_ID = od.Order_ID
                                AND o.Email = @Email
                                GROUP BY od.Product_ID,p.Product_Name,p.Product_Image,p.Product_Description,o.Order_Date,o.Quantity, p.Product_Summary";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order orderInfo = new Order()
                    {
                        Product_ID = (int)reader["Product_ID"],
                        Product_Name = (string)reader["Product_Name"],
                        Product_Img = (string)reader["Product_Image"],
                        Product_Summ = (string)reader["Product_Summary"],
                        Product_Desc = (string)reader["Product_Description"],
                        Order_Date = (DateTime)reader["Order_Date"],
                        Order_Quantity = (int)reader["Quantity"],
                    };
                    orderInfos.Add(orderInfo);
                }
                conn.Close();
            }
            return orderInfos;
        }
    }
}