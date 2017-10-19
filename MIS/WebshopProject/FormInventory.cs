using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace 电商
{
    public partial class FormInventory : Form
    {
        string constr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=电商数据库;Data Source=YANG-PC\SQLEXPRESS";/////数据库的连接字符串，如果要运行的话，需要改成自己数据
        private void get_users()
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM 进货量和价格", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];//////给datagridview控件绑定数据源
        }
        public FormInventory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
            conn.Open();//////用conn打开数据库连接
            string sql = string.Format("select * from 库存提醒");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str1, str2, str3;
            str1 = "'" + textBox1.Text.ToString() + "'";
            str2 = "'" + textBox2.Text.ToString() + "'";
            str3 = "'" + textBox3.Text.ToString() + "'";
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("您输入的信息不完全！", "提示");
            }

            else
            {
                SqlConnection conn = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
                conn.Open();//////用conn打开数据库连接
                string sql1 = string.Format("select * from 商品信息 where 商品ID={0}", str1); //////定义要执行的数据库操作，注意：因为我的电脑里面database1数据库里面有users数据表，所以，这段代码不会报错，如果在你们的电脑上运行的话，则要么改成你的数据表名，要么新建一个users数据表。我的users数据表里面，有ID， username 以及userpwd三个字段。                                                                                                              
                SqlCommand comtext = new SqlCommand(sql1, conn);///////初始化数据库操作，SqlCommand(sql, conn)中sql为要执行的数据库操作代码，conn为数据库连接
                SqlDataReader dr;//////定义读取数据的对象
                dr = comtext.ExecuteReader();///////给数据读取对象初始化
                dr.Read();
                if (dr.HasRows)
                {
                    SqlConnection conn2 = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
                    conn2.Open();
                    string sql = string.Format("insert into 进货量和价格 values({0},GETDATE(),{1},{2});", str1, str2, str3);
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn2);
                    DataSet sourceDataSet = new DataSet();
                    adapter.Fill(sourceDataSet);

                   //判断有则改之无则加之
                    string sql3 = string.Format("select * from 库存提醒 where 商品ID={0}", str1); //////定义要执行的数据库操作，注意：因为我的电脑里面database1数据库里面有users数据表，所以，这段代码不会报错，如果在你们的电脑上运行的话，则要么改成你的数据表名，要么新建一个users数据表。我的users数据表里面，有ID， username 以及userpwd三个字段。                                                                                                              
                    SqlCommand comtext2 = new SqlCommand(sql3, conn2);///////初始化数据库操作，SqlCommand(sql, conn)中sql为要执行的数据库操作代码，conn为数据库连接
                    SqlDataReader dr2;//////定义读取数据的对象
                    dr2 = comtext2.ExecuteReader();///////给数据读取对象初始化
                    dr2.Read();
                    if (dr2.HasRows)
                    {
                        MessageBox.Show("修改", "提示");
                        SqlConnection conn3 = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
                        conn3.Open();
                        string sql2 = string.Format("UPDATE 库存提醒 SET 实时库存 = 实时库存 +{0} WHERE 商品ID = {1}", str2, str1);
                        SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, conn3);
                        DataSet sourceDataSet2 = new DataSet();
                        adapter2.Fill(sourceDataSet2);
                    }
                    else
                    {
                        MessageBox.Show("新建", "提示");
                        SqlConnection conn3 = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
                        conn3.Open();
                        string sql2 = string.Format("insert into 库存提醒 values({0},{1});", str1, str2);
                        SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, conn3);
                        DataSet sourceDataSet2 = new DataSet();
                        adapter2.Fill(sourceDataSet2);
                    }
                    MessageBox.Show("库存信息已更新！", "提示");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    dr.Close();
                }
                    else
                {
                    MessageBox.Show("无该商品ID信息", "提示");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    dr.Close();
                }
            }
        }
        
        private void FormInventory_Load(object sender, EventArgs e)
        {

        }

        private void FormInventory_Load_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
            conn.Open();//////用conn打开数据库连接
            string sql = string.Format("select * from 进货量和价格");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
            conn.Open();//////用conn打开数据库连接
            string sql = string.Format("select * from 库存提醒 where 实时库存<10");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBusiness.user = "";
            DataBusiness.userid = "";
            DataBusiness.balance = "";
            var frm = new FormLogin();
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
