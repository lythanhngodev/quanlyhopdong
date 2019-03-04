using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace trungtamtinhoc
{
    public class clsDatabase
    {
        public string strconnect = @"Server=127.0.0.1;Port=3309;Database=ttth;Uid=root;Pwd=1234:)))";
        public MySqlConnection conn;
        public clsDatabase()
        {
            conn = new MySqlConnection(strconnect);
            conn.Open();
        }
        public DataTable GetData(string str)
        {
            DataTable dt = new DataTable();
            clsDatabase db = new clsDatabase();
            MySqlCommand comm = db.conn.CreateCommand();
            comm.CommandText = str;
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(dt);
            return dt;
        }
        public string ChuoiNgauNhien(int sokytu)
        {
                string bangchucai = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                char[] matkhauduoctao=new char[sokytu];
                int chieudaimang = bangchucai.Length;
                for (int i = 0; i < sokytu; i++) {
                    Random rdm = new Random();
                    int n = 0;
                    n=rdm.Next(0, chieudaimang);
                    matkhauduoctao[i] = bangchucai[n];
                }
                return string.Join("",matkhauduoctao);
        }
        ~clsDatabase()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }
}