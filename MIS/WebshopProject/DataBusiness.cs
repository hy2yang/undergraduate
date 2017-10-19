using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace 电商
{
  
    public class DataBusiness
    {
        public static string user;
        public static string userid;
        public static string balance;

        public static string connStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=电商数据库;Data Source=YANG-PC\SQLEXPRESS";/////根据自己的电脑，进行相应的修改，同Formlogin里面的。
        public DataBusiness()
        { }  
        /// 数据查询处理      
        public static DataSet Query(string cmdText, string tableName)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, tableName);
            conn.Close();
            return dataSet;
        }

        /// <summary>
        /// 数据处理(增删改)
        /// </summary>
        /// <param name="cmdText"></param>
        public static void NonQuery(string cmdText, SqlConnection conn)
        {
            //MySqlConnection conn = new MySqlConnection(connStr);
            //conn.Open();
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.ExecuteNonQuery();
            //conn.Close();

        }

        public static void NonQuery(string cmdText)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

    }
    
        
    

    }
