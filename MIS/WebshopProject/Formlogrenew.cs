using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 电商
{
    public partial class Formlogrenew : Form
    {
        string constr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=电商数据库;Data Source=YANG-PC\SQLEXPRESS";
        string str1;
        string str2;

        public Formlogrenew()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")////判断textbox1是否为空
            {
                MessageBox.Show("请输入订单ID", "提示");/////messagebox为弹出框控件
                textBox1.Focus();///////光标落在textbox1里面
            }
            else if (textBox2.Text == "")////判断textbox2是否为空
            {
                MessageBox.Show("请输入物流状态", "提示");
                textBox2.Focus();
            }
            else
            {
                str1 = "'" + textBox1.Text.ToString() + "'";
                str2 = "'" + textBox2.Text.ToString() + "'";
                string sql = string.Format("insert into 物流状态 values({0},GETDATE(),{1});", str1, str2);
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataSet sourceDataSet = new DataSet();
                adapter.Fill(sourceDataSet);
                get_log();
            }
        }
                
         private void get_log()
        {
            string sql = string.Format("SELECT 订单ID,时间,物流状态 FROM 物流状态 where 订单ID={0}", str1);
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];//////给datagridview控件绑定数据源
        }
       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Formlogrenew_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBusiness.user = "";
            DataBusiness.userid = "";
            DataBusiness.balance = "";
            var frm =FormLogin.f;
            frm.Show();
        }
    }
}
