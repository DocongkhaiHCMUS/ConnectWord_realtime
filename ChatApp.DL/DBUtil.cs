using MySql.Data.MySqlClient;
using System;

namespace ChatApp.DL
{
    class DBUtil
    {
        /// <summary>
        /// get MySql Connection from config info
        /// </summary>
        public static MySqlConnection GetDBConnection(string host, int port, string database,
                                                        string username, string password)
        {
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}
