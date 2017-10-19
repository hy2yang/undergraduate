﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 电商
{
    public partial class FormUserInf : Form
    {
        string constr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=电商数据库;Data Source=YANG-PC\SQLEXPRESS";/////数据库的连接字符串，如果要运行的话，需要改成自己数据库的连接字符。一般情况下，将“DataBase1”改成你数据库系统里面的数据库名称，CHAI-PC改成你的电脑名称就可以。

        private void get_users()
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM 用户信息", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];//////给datagridview控件绑定数据源
        }

        public FormUserInf()
        {
            InitializeComponent();
        }

        private void FormUserInf_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            get_users();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str3, str4,str5, str6;
            str3 = "'" + textBox3.Text.ToString() + "'";
            str4 = "'" + textBox4.Text.ToString() + "'";
            str5 = "'" + textBox5.Text.ToString() + "'";
            str6 = "'" + comboBox1.Text.ToString() + "'";
            if (textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "")
            { MessageBox.Show("您输入的信息不完全！用户名、密码、用户分组为必填", "提示"); }

            else {

                SqlConnection conn = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
                conn.Open();//////用conn打开数据库连接
                string sql1 = string.Format("select * from 用户信息 where 用户名={0}", str3); //////定义要执行的数据库操作，注意：因为我的电脑里面database1数据库里面有users数据表，所以，这段代码不会报错，如果在你们的电脑上运行的话，则要么改成你的数据表名，要么新建一个users数据表。我的users数据表里面，有ID， username 以及userpwd三个字段。                                                                                                              
                SqlCommand comtext = new SqlCommand(sql1, conn);///////初始化数据库操作，SqlCommand(sql, conn)中sql为要执行的数据库操作代码，conn为数据库连接
                SqlDataReader dr;//////定义读取数据的对象
                dr = comtext.ExecuteReader();///////给数据读取对象初始化
                dr.Read();
                if (dr.HasRows)
                {
                    MessageBox.Show("添加失败,用户名已存在！", "提示");
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    comboBox1.Text = "";
                    dr.Close();
                }
            
                else
                {
                    SqlConnection conn2 = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
                    conn2.Open();
                    string sql = string.Format("insert into 用户信息 values({0},{1},{2},{3},null);", str3, str4, str5, str6);
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn2);
                    DataSet sourceDataSet = new DataSet();
                    adapter.Fill(sourceDataSet);
                    get_users();
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    comboBox1.Text = "";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                string str = dataGridView1.SelectedRows[0].Cells["用户ID"].Value.ToString();/////获取datagridview特定行列的数值
                string cmdText = "Delete from 用户信息 where 用户ID=" + str + "";
                DataBusiness.NonQuery(cmdText);
                get_users();
            }
        }

        
         
        private DataTable dbconn(string strSql)
        {
            SqlConnection conn = new SqlConnection(constr);//声明一个SqlConnection变量  
            SqlDataAdapter adapter;//声明一个SqlDataAdapter  
            conn.Open();//打开连接  
            adapter = new SqlDataAdapter(strSql, conn);//实例化对象  
            DataTable dtSelect = new DataTable();//  
            int rnt = adapter.Fill(dtSelect);//  
            conn.Close();//关闭连接  
            return dtSelect;//返回DataTable对象  
        }
        private Boolean dbUpdate()//  
        {
            SqlConnection conn = new SqlConnection(constr);//声明一个SqlConnection变量  
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter("select * from 用户信息", conn);
            string strSql = "select * from 用户信息";//声明Sql语句  
            DataTable dtUpdate = new DataTable();
            dtUpdate = this.dbconn(strSql);//实例化DataTable对象  
            dtUpdate.Rows.Clear();//调用Clear方法  
            DataTable dtShow = new DataTable();
            dtShow = (DataTable)this.dataGridView1.DataSource;
            for (int i = 0; i < dtShow.Rows.Count; i++)//循环遍历  
            {
                dtUpdate.ImportRow(dtShow.Rows[i]);//ImportRow方法填充值  
            }
            try
            {
                conn.Open();//打开连接  
                SqlCommandBuilder cb = new SqlCommandBuilder(adapter);//声明SqlCommandBuilder变量  
                adapter.Update(dtUpdate);//调用Update方法更新数据  
                conn.Close();//关闭连接  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());//出现异常弹出异常信息  
                return false;
            }
            dtUpdate.AcceptChanges();//向数据库调教更改  
            return true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dbUpdate())//判断返回值是否为true  
            {
                MessageBox.Show("修改成功！");//  
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string str1, str2;
            str1 = "'"+textBox1.Text.ToString()+"'";
            str2 = "'" + textBox2.Text.ToString() + "'";
            if (textBox1.Text == "")
            { str1 = "用户名"; }
            if (textBox2.Text == "")
            { str2 = "用户分组"; }
            string sql = string.Format("select * from 用户信息 where 用户名={0} and 用户分组={1}", str1, str2);
             SqlConnection conn = new SqlConnection(constr);
             conn.Open();
             SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
             DataSet sourceDataSet = new DataSet();
             adapter.Fill(sourceDataSet);
             dataGridView1.DataSource = sourceDataSet.Tables[0];
        
            }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBusiness.user = "";
            DataBusiness.userid = "";
            DataBusiness.balance = "";
            var frm = new FormLogin();
            frm.ShowDialog();
        }
    }
}
