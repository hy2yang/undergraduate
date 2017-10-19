using System;
using System.Windows.Forms;

namespace 电商
{
    public partial class FormCustomer : Form
    {
        public FormCustomer()
        {
            InitializeComponent();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new FormCart();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new Formlogshow();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new FormEvaluate();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new FormCharge();
            frm.ShowDialog();
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
    }
}
