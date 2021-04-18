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
            //List<Customer> customers = new List<Customer>();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT Email,First_Name,Last_Name,Password,Mobile FROM Customer";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer cust = new Customer()
                    {
                       Username = (string)reader["Email"],
                       First_Name=(string)reader["First_Name"],
                       Last_Name= (string)reader["Last_Name"],
                        Password = (string)reader["Password"],
                        Mobile=(int)reader["Mobile"]
                    };
                    customers.Add(cust);
                }
            }
            return customers;
        }

       

    }
}
