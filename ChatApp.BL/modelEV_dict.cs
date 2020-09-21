using ChatApp.DL;
using ChatApp.Shared.MessagePackObjects;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ChatApp.BL
{
    public class modelEV_dict
    {
        // name of table
        const string TBL_EVDICT = "ev_dict";

        /// <summary>
        /// load dictionary English-Vietnamese
        /// </summary>
        public static List<E2V> loadDictionary()
        {
            Provider dl = new Provider();
            List<E2V> rs = new List<E2V>();
            
            try
            {
                string sql = string.Format("select * from {0}", TBL_EVDICT);
                dl.Connect();

                CommandType type = CommandType.Text;
                DataTable dataTable = dl.Select(type, sql);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string word = dataTable.Rows[i]["word"].ToString();
                    string pronounce = dataTable.Rows[i]["pronounce"].ToString();
                    string explain = dataTable.Rows[i]["explain"].ToString();

                    E2V item = new E2V(word, pronounce, explain);
                    rs.Add(item);
                }

                return rs;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                dl.DisConnect();
            }
        }
    }
}
