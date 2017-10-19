using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 电商
{
    public partial class Formlogshow : Form
    {
        string constr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=电商数据库;Data Source=YANG-PC\SQLEXPRESS";
        string str1;

        private void get_order()
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT 订单ID,商品名称,数量 FROM 购买ID综合 where (签收状态 is NULL and 用户ID = "+DataBusiness.userid+")", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];//////给datagridview控件绑定数据源
        }

        public Formlogshow()
        {
            InitializeComponent();
            get_order();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")////判断textbox1是否为空
            {
                MessageBox.Show("请输入订单ID", "提示");/////messagebox为弹出框控件
                textBox1.Focus();///////光标落在textbox1里面
            }
            else
            {
                str1 = "'" + textBox1.Text.ToString() + "'";
                string sql = string.Format("select 订单ID,时间,物流状态 FROM 物流状态 where 订单ID={0}", str1);
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataSet sourceDataSet = new DataSet();
                adapter.Fill(sourceDataSet);
                dataGridView2.DataSource = sourceDataSet.Tables[0];//////给datagridview控件绑定数据源
              
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Formlogshow_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = string.Format("update 购买ID综合 set 签收状态=1 where 订单ID={0}",str1);
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBusiness.user = "";
            DataBusiness.userid = "";
            DataBusiness.balance = "";
            var frm = new FormCustomer();
            frm.ShowDialog();
        }
    }
}
