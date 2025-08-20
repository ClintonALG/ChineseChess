using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Chinese_Chess
{
    internal class MyDB
    {
        // dô database ở App_Data lấy connectstring dán vào
        public static string link_ketnoi = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\clint\\source\\repos\\zz.xong deadline\\Chinese_Chess\\Chinese_Chess\\App_Data\\Database1.mdf\";Integrated Security=True";

        public SqlConnection connect = new SqlConnection();

        public MyDB()
        {
            connect = new SqlConnection(link_ketnoi);
        }

        public void Open()
        {
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        public void Close()
        {
            if (connect.State == ConnectionState.Open)
            {
                connect.Close();
            }
        }

        public int getNonQuery(string sqlquery)
        {
            Open();

            SqlCommand cmd = new SqlCommand(sqlquery, connect);

            int kq = cmd.ExecuteNonQuery();

            Close();

            return kq;
        }

        public DataTable getData_Table(string sqlquery)
        {
            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(sqlquery, connect);

            da.Fill(ds);

            return ds.Tables[0];
        }

        public int getScalar(string sqlquery)
        {
            Open();

            SqlCommand cmd = new SqlCommand(sqlquery, connect);

            object result = cmd.ExecuteScalar();
            int kq = (result != null && result != DBNull.Value) ? (int)result : 0;

            Close();

            return kq;
        }
        public string getScalarString(string sqlquery)
        {
            Open();

            SqlCommand cmd = new SqlCommand(sqlquery, connect);

            object result = cmd.ExecuteScalar();
            string value = (result != null && result != DBNull.Value) ? result.ToString() : "error";

            Close();

            return value;
        }

    }
}
