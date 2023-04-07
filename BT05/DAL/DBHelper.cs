using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT05.DAL
{
    internal class DBHelper
    {
        private static DBHelper _Instance;
        private SqlConnection _cnn;
        public static DBHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DBHelper("Data Source=LAPTOP-CICSVA36\\SQLEXPRESS;Initial Catalog=BT05;Integrated Security=True");
                }
                return _Instance;
            }
            private set { }
        }
        private DBHelper(string s)
        {
            _cnn = new SqlConnection(s);
        }
        public DataTable GetReords(string query)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, _cnn);
            _cnn.Open();
            da.Fill(dt);
            _cnn.Close();
            return dt;
        }
        public void ExecuteDB(string query)
        {
            SqlCommand cmd = new SqlCommand(query, _cnn);
            _cnn.Open();
            cmd.ExecuteNonQuery();
            _cnn.Close();
        }
       
        
    }
}
