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

                //SQL string
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
using Team1_ESNET_CA.Data;
using Team1_ESNET_CA.Models;
using System.Data;
using System.Diagnostics;

namespace Team1_ESNET_CA.Data
{
    public class OrderData : DataConnection
    {
        public static List<Order> addToOrder()
        {
            Cart cartId = new Cart();
            List<Order> addToCarts = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader reader = null;
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("", conn, trans);
                try
                {
                    cmd.CommandText = @"SELECT Product_ID, Email FROM Cart WHERE Email = 'janedoe@gmail.com'";
                    //cmd.Parameters.AddWithValue("@Cart_ID", cartId.Session_Cart_ID);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Order addToCart = new Order()
                        {
                            Product_ID = (int)reader["Product_ID"],
                            Email = (string)reader["Email"]
                        };
                        addToCarts.Add(addToCart);
                    }

                    cmd.CommandText = @"DELETE FROM Cart WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("@Email", cartId.Email);
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Some error with DB: " + ex.Message);
                    trans.Rollback();
                }
                return addToCarts;
            }
        } //Cart to Order
        public static void generateActCode(List<Order> orderdetail) // replace product Id model from Order to Cart
        {
            //Need do validation on current Order_Id with session_ID
            List<Order> orderdetails = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var o in orderdetail)
                {
                    var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
                    string sql1 = @"INSERT INTO Order_Details (Activation_Code,Order_ID,Product_ID)
                                VALUES ( @Activation_Code, @Order_ID, @Product_ID)";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@Activation_Code", uid);

                    cmd1.Parameters.Add("@Order_ID", SqlDbType.Int);
                    cmd1.Parameters["@Order_ID"].Value = o.Session_ID;

                    cmd1.Parameters.Add("@Product_ID", SqlDbType.Int);
                    cmd1.Parameters["@Product_ID"].Value = o.Product_ID;
                    //cmd1.Parameters.AddWithValue("@Order_ID", Order_ID);
                    //cmd1.Parameters.AddWithValue("@Product_ID", Product_ID);
                    cmd1.ExecuteNonQuery();
                };
                conn.Close();
            }
        }
        public static List<Order> getPdtInfo()
        {
            List<Order> orderInfos = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //SQL string
                string sql = @"SELECT p.Product_Name, p.Product_Image, p.Product_Description, o.Order_Date,od.Product_ID , od.Activation_Code
                                FROM Product AS p, [Order] AS o, Order_Details AS od
                                WHERE p.Product_ID = od.Product_ID
                                AND o.Order_ID = od.Order_ID
                                and od.Order_ID = '1'"; //OrderId should be replaced with email (New Database)
                //SQLCommand
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order orderInfo = new Order()
                    {
                        //link to model var name
                        Product_Name = (string)reader["Product_Name"],
                        Product_Img = (string)reader["Product_Image"],
                        Product_Desc = (string)reader["Product_Description"],
                        Order_Date = (DateTime)reader["Order_Date"],
                        Order_Quantity = (int)reader["Product_ID"],
                        Activation_Code = (string)reader["Activation_Code"],
                    };
                    orderInfos.Add(orderInfo);
                }
                conn.Close();
            }
            return orderInfos;
        }
    }
}