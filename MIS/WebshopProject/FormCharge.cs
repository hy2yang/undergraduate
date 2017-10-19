using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 电商
{
    public partial class FormCharge : Form
    {
        string constr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=电商数据库;Data Source=YANG-PC\SQLEXPRESS";

        public FormCharge()
        {
            InitializeComponent();
            label1.Text = "您的余额为"+DataBusiness.balance;
        }

        private void checkbalance()
        {
            SqlConnection conn = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
            conn.Open();//////用conn打开数据库连接
            string sql1 = string.Format("SELECT 账户余额 FROM 用户信息 where 用户ID={0}", DataBusiness.userid);
            SqlCommand comtext = new SqlCommand(sql1, conn);///////初始化数据库操作，SqlCommand(sql, conn)中sql为要执行的数据库操作代码，conn为数据库连接
            SqlDataReader dr;//////定义读取数据的对象

            dr = comtext.ExecuteReader();///////给数据读取对象初始化

            dr.Read();///////开始读取
            DataBusiness.balance = dr["账户余额"].ToString();


        }

        private void FormCharge_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string charge = "'" + textBox1.Text.ToString() + "'";

            string sql = string.Format("update 用户信息 set 账户余额=账户余额+{0} where 用户ID={1}", charge,DataBusiness.userid);
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            checkbalance();
            label1.Text = "您的余额为" + DataBusiness.balance;
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new FormCustomer();
            frm.ShowDialog();
        }
    }
}
