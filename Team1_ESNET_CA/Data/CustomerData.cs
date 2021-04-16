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

       

    }
}
