using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace 电商
{
    public partial class FormEvaluate : Form
    {
        string constr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=电商数据库;Data Source=YANG-PC\SQLEXPRESS";

        private void get_evaluates()
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT 订单ID,数量,金额,购买时间,评价等级,评价内容 FROM 购买ID综合 where (评价等级=0 and 用户ID="+DataBusiness.userid+")", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];//////给datagridview控件绑定数据源
        }

        private void get_evaluated()
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT 订单ID,数量,金额,购买时间,评价等级,评价内容 FROM 购买ID综合 where (评价等级<>0 and 用户ID="+DataBusiness.userid+")", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);
            dataGridView1.DataSource = sourceDataSet.Tables[0];
        }
        public FormEvaluate()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            get_evaluates();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            get_evaluated();
        
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
                string str3, str4, str5;
                str3 = "'" + textBox1.Text.ToString() + "'";
                str4 = "'" + textBox2.Text.ToString() + "'";
                str5 = "'" + comboBox1.Text.ToString() + "'";

                if (textBox1.Text == "" || comboBox1.Text == "")
                { MessageBox.Show("您输入的信息不完全！", "提示"); }

                else
                {
                    string sql = string.Format("update 购买ID综合 set 评价等级={0},评价内容={1} where 订单ID={2}", str5,str4,str3);
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataSet sourceDataSet = new DataSet();
                    adapter.Fill(sourceDataSet);
                    get_evaluates();
                    get_evaluated();
                }
            }

        private void FormEvaluate_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new FormCustomer();
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    }
