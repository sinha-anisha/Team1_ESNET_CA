using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Data
{
    public class CustomerData : DataConnection
    {

        public CustomerData()
        {
        }

        public static AppData InitAppData()
        {
            AppData appData = new AppData();

            GetAllCustomers(appData.Customers);

            return appData;
        }

        public static List<Customer> GetAllCustomers(List<Customer> customers)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT Customer.Email,Customer.Password FROM Customer";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer cust = new Customer()
                    {
                       Username = (string)reader["Email"],
                        Password = (string)reader["Password"],
                    };
                    customers.Add(cust);
                }
            }
            return customers;
        }

        /*public static void InsertRecord(Customer cust)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"insert into Customer (Email,First_Name,Last_Name,Password,Mobile)
                                Values(@Email,@First_Name,@Last_Name,@Password,@Mobile)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Customer cust = new Customer();

                cmd.Parameters.AddWithValue("@Email", cust.Username);
                cmd.Parameters.AddWithValue("@First_Name", cust.FirstName);
                cmd.Parameters.AddWithValue("@Last_Name", cust.LastName);
                cmd.Parameters.AddWithValue("@Password", cust.Password);
                cmd.Parameters.AddWithValue("@Mobile", cust.Mobile);

                cmd.ExecuteNonQuery();
            }
            
        }*/



    }
}
