using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace quanlyhopdong
{
    public class SqlFunction
    {
        SqlConnection con; //Dùng để kết nối vào cơ sở dữ liệu
        SqlDataAdapter da;  //Là đối tượng trung gian lấy dữ liệu FIll vào trong các đối tương Data
        SqlCommand cmd;// Các xử lý truy vấn SQL thêm, xóa, sửa
        string _vConnectString = ConfigurationManager.ConnectionStrings["StringConnect"].ToString();
        public static string _err = "";

        public SqlFunction()
        {
            
        }

        private void OpenConnect()
        {
            if (con == null)
                con = new SqlConnection(_vConnectString);
            con.ConnectionString = _vConnectString;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseConnect()
        {
            try
            {
                con.Close();
                con.Dispose();
            }
            catch { }
        }

        public DataTable GetData(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnect();
                da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            catch { }
            finally { CloseConnect(); }
            return dt;
        }

        public DataTable GetData(string sql, string tableName)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnect();
                da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
                dt.TableName = tableName;
            }
            catch { }
            finally { CloseConnect(); }
            return dt;
        }

        public bool GetOneBoolField(string sql)
        {
            bool t = false;
            try
            {
                OpenConnect();
                cmd = new SqlCommand(sql, con);
                t = (bool)cmd.ExecuteScalar();
            }
            catch { t = false; }
            finally { CloseConnect(); }
            return t;
        }

        public decimal GetOneDecimalField(string sql)
        {
            decimal t = 0;
            try
            {
                OpenConnect();
                cmd = new SqlCommand(sql, con);
                t = (decimal)cmd.ExecuteScalar();
            }
            catch { t = 0; }
            finally { CloseConnect(); }
            return t;
        }

        public string GetOneStringField(string sql)
        {
            string t = "";
            try
            {
                OpenConnect();
                cmd = new SqlCommand(sql, con);
                t = (string)cmd.ExecuteScalar();
                if (t == null) t = "";
            }
            catch { t = ""; }
            finally { CloseConnect(); }
            return t;
        }

        public DateTime GetOneDateTimeField(string sql)
        {
            DateTime t;
            try
            {
                OpenConnect();
                cmd = new SqlCommand(sql, con);
                t = (DateTime)cmd.ExecuteScalar();
            }
            catch { t = DateTime.Now; }
            finally { CloseConnect(); }
            return t;
        }

        public bool ExeCuteNonQuery(string sql)
        {
            try
            {
                OpenConnect();
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch { return false; }
            finally { CloseConnect(); }
            return true;
        }

        public bool CheckHasRecord(string sql)
        {
            DataTable dt = new DataTable();
            dt = GetData(sql);
            return (dt != null && dt.Rows.Count > 0 ? true : false);
        }
    }
}