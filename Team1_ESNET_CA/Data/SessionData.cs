using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Team1_ESNET_CA.Models;

namespace Team1_ESNET_CA.Data
{
    public class SessionData:DataConnection
    {

        public static List<Session> GetAllSessions()
        {
            List<Session> sessions = new List<Session>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT Session.[Session_ID],Session.Email,Session.TimeStamp FROM Session";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Session sess = new Session()
                    {
                        Session_ID = (string)reader["Session_ID"],
                        Email = (string)reader["Email"],
                        TimeStamp = (long)reader["Timestamp"],
                    };
                    sessions.Add(sess);
                }
            }
            return sessions;
        }




    }
}
