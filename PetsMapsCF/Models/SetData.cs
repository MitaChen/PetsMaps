using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PetsMapsCF.Models
{
    public class SetData
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PetsMapsCFConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand("", conn);
        public void executeSql(string sql)
        {
            cmd.CommandText = sql;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void executeSql(string sql, List<SqlParameter> para)
        {
            cmd.CommandText = sql;
            foreach (var item in para)
            {
                cmd.Parameters.Add(item);
            }
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void executeSqlBySP(string SPName)
        {
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void executeSqlBySP(string SPName, List<SqlParameter> para)
        {
            cmd.CommandText = SPName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var item in para)
            {
                cmd.Parameters.Add(item);
            }
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { }
            conn.Close();
        }
    }
}