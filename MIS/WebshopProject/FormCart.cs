using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace 电商
{
    public partial class FormCart : Form
    {
        string constr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=电商数据库;Data Source=YANG-PC\SQLEXPRESS";
        double sum = 0;

        private void get_goods() //刷新datagridview1控件 商品表
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM 商品信息", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];//////给datagridview控件绑定数据源
        }

        private void checkcart()
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select 商品信息.商品ID,商品信息.名称,商品信息.现价,购物车.数量,数量*现价 as 总价 from 商品信息, 购物车 where 商品信息.商品ID = 购物车.商品ID and  商品信息.商品ID in  (SELECT  商品ID FROM 购物车 where 用户ID ='" + DataBusiness.userid+"')", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView2.DataSource = sourceDataSet.Tables[0];//////给datagridview控件绑定数据源
            sum = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dataGridView2.Rows[i].Cells["总价"].Value);
             }
            label6.Text ="总金额为"+sum.ToString()+"元";
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


        public FormCart()
        {
            InitializeComponent();
            label1.Text = DataBusiness.user+"的购物车";
            get_goods();
            checkcart();
            checkbalance();
            label7.Text = DataBusiness.balance;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormCart_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            get_goods();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkcart();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id= "'" + textBox1.Text.ToString() + "'";
            string num = "'" + numericUpDown1.Value.ToString() + "'";
            string sql = string.Format("insert into 购物车 values({0},{1},{2});", DataBusiness.userid, id, num);
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            checkcart();

            textBox1.Text = "";
            numericUpDown1.Value= 0;
            
  
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                string str = dataGridView2.CurrentRow.Cells["商品ID"].Value.ToString();/////获取datagridview特定行列的数值
                string cmdText = "Delete from 购物车 where 用户ID='"+DataBusiness.userid+"' and 商品ID='" + str + "'";
                DataBusiness.NonQuery(cmdText);
               
                checkcart();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new FormCustomer();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < (dataGridView2.Rows.Count) - 1; i++)
            {
                string userid, goodid, num, amount;
                userid = DataBusiness.userid;
                goodid = dataGridView2.Rows[i].Cells["商品ID"].Value.ToString();
                num = dataGridView2.Rows[i].Cells["数量"].Value.ToString();
                amount = dataGridView2.Rows[i].Cells["总价"].Value.ToString();
               
                string sql = string.Format("insert into 购买ID综合 values ('{0}','{1}','{2}','{3}',getdate(),'','',null,null)", userid, goodid, num, amount);
                DataBusiness.NonQuery(sql);

                string balance = string.Format("update 用户信息 set 账户余额=账户余额-{0} where 用户ID={1}", amount, DataBusiness.userid);
                DataBusiness.NonQuery(balance);

                string inven = string.Format("update 库存提醒 set 实时库存=实时库存-{0} where 商品ID={1}", num, goodid);
                DataBusiness.NonQuery(inven);

            }

            MessageBox.Show("下单成功");
            string delete = string.Format("delete from 购物车 where 用户ID={0}", DataBusiness.userid);
            DataBusiness.NonQuery(delete);
            checkcart();
            checkbalance();
            label7.Text = DataBusiness.balance;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string str1, str2;
            str1 = "'%" + textBox2.Text.ToString() + "%'";
            str2 = "'" + textBox3.Text.ToString() + "'";
            if (textBox2.Text == "")
            { str1 = "名称"; }
            if (textBox3.Text == "")
            { str2 = "分类"; }
            string sql = string.Format("select * from 商品信息 where 名称 like {0} and 分类={1}", str1, str2);
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];
        }
    }
}
