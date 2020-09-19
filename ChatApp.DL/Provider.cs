using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;

namespace ChatApp.DL
{
    public class Provider
    {
        MySqlConnection Connection { get; set; }

        public static MySqlConnection GetConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "db_noi-tu";
            string username = "root";
            string password = "";

            return DBUtil.GetDBConnection(host, port, database, username, password);
        }

        public void Connect()
        {
            try
            {
                if (Connection == null)
                    Connection = GetConnection();
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
                Connection.Open();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void DisConnect()
        {
            if (Connection != null && Connection.State != ConnectionState.Closed)
                Connection.Close();
        }

        /// <summary>
        /// Use to select data from DB
        /// </summary>
        /// <param name="type">text or procedure</param>
        /// <param name="sql">query string</param>
        /// <param name="param">parameters for procedure</param>
        /// <returns>DataTable</returns>

        public Task<DataTable> Select(CommandType type, string sql, params MySqlParameter[] param)
        {
            return Task.Run(() =>
            {
                try
                {
                    MySqlCommand sqlCommand = Connection.CreateCommand();
                    sqlCommand.CommandType = type;
                    sqlCommand.CommandText = sql;
                    if (param != null && param.Length > 0)
                        sqlCommand.Parameters.AddRange(param);

                    MySqlDataAdapter sqlData = new MySqlDataAdapter(sqlCommand);
                    DataTable table = new DataTable();
                    sqlData.Fill(table);
                    return table;
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
            });
        }

        /// <summary>
        /// use to Insert, Update or Delete data from DB
        /// </summary>
        /// <param name="type">text or procedure</param>
        /// <param name="sql">query string</param>
        /// <param name="param">parameters for procedure</param>
        /// <returns>int</returns>
        public Task<int> ExeCuteNonQuery(CommandType type, string sql, params MySqlParameter[] param)
        {
            return Task.Run(() =>
            {
                try
                {
                    MySqlCommand sqlCommand = Connection.CreateCommand();
                    sqlCommand.CommandType = type;
                    sqlCommand.CommandText = sql;

                    if (param != null && param.Length > 0)
                        sqlCommand.Parameters.AddRange(param);

                    int nRow = sqlCommand.ExecuteNonQuery();
                    return nRow;
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
            });
        }
    }
}
