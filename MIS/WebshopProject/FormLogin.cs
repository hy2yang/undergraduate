using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 电商
{
   
    public partial class FormLogin : Form
    {
        string constr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=电商数据库;Data Source=YANG-PC\SQLEXPRESS";/////数据库的连接字符串，如果要运行的话，需要改成自己数据库的连接字符。一般情况下，将“DataBase1”改成你数据库系统里面的数据库名称，CHAI-PC改成你的电脑名称就可以。
        string str1;
        string str2;
        public static FormLogin f;

        public FormLogin()
        {
            InitializeComponent();
            f = this;
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {


        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")////判断textbox1是否为空
            {
                MessageBox.Show("请填写用户名", "提示");/////messagebox为弹出框控件
                textBox1.Focus();///////光标落在textbox1里面
            }
            else if (textBox2.Text == "")////判断textbox2是否为空
            {
                MessageBox.Show("请填写密码", "提示");
                textBox2.Focus();
            }
            else
            {
                DataBusiness.user = textBox1.Text.ToString();

                str1 = textBox1.Text.ToString();///////因为上面已经定了str1,所以这里不需要再定义，可以直接赋值；不然要写成string str1=.....形式
                str2 = textBox2.Text.ToString();
                SqlConnection conn = new SqlConnection(constr);//////初始化一个新的sql数据库连接conn，constr为数据库连接字符串，上面已定义
                conn.Open();//////用conn打开数据库连接
                string sql1 = string.Format("select * from 用户信息 where 用户名 ='{0}'and 密码='{1}'", str1, str2); //////定义要执行的数据库操作，注意：因为我的电脑里面database1数据库里面有users数据表，所以，这段代码不会报错，如果在你们的电脑上运行的话，则要么改成你的数据表名，要么新建一个users数据表。我的users数据表里面，有ID， username 以及userpwd三个字段。                                                                                                              
                SqlCommand comtext = new SqlCommand(sql1, conn);///////初始化数据库操作，SqlCommand(sql, conn)中sql为要执行的数据库操作代码，conn为数据库连接
                SqlDataReader dr;//////定义读取数据的对象

                dr = comtext.ExecuteReader();///////给数据读取对象初始化
                
                dr.Read();///////开始读取
                

                if (dr.HasRows)///////如果dr读取到了数据，即：数据库的执行语句里面有返回值，说明账号与密码匹配
                {
                    string gettype = dr["用户分组"].ToString();

                    DataBusiness.userid = dr["用户ID"].ToString();
                    DataBusiness.user = dr["用户名"].ToString();
                    DataBusiness.balance = dr["账户余额"].ToString();

                    if (MessageBox.Show("登录成功，您是" + gettype + "人员", "提示", MessageBoxButtons.OK) == DialogResult.OK) ;
                    {
                        if (gettype == "客户")
                        { var frm2 = new FormCustomer();
                            frm2.ShowDialog();
                        }
                        if (gettype == "商品信息")
                        {
                            var frm3 = new FormGoodInf();
                            frm3.ShowDialog();
                        }
                        if (gettype == "用户信息")
                        {
                            var frm4 = new FormUserInf();
                            frm4.ShowDialog();
                        }
                        if (gettype == "物流信息")
                        {
                            var frm4 = new Formlogrenew();
                            frm4.ShowDialog();
                        }
                        if (gettype == "库存信息")
                        {
                            var frm4 = new FormInventory();
                            frm4.ShowDialog();
                        }
                    }

                    f.Hide();
                }
                else///////如果没有读取到数据，即：数据库的执行语句里面无返回值，说明账号与密码不匹配
                {
                    MessageBox.Show("登录失败,账户或者密码错误！", "提示");///////提示登陆失败
                    textBox1.Text = "";/////将textbox内的取值清空
                    textBox2.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)////////定义单击“取消”按钮的事件
        {
            if (MessageBox.Show("确定退出", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)/////////定义messagebox的格式，如果选中的是确定，则关闭当前窗体
                this.Close();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
