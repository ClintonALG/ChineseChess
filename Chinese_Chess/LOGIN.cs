using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chinese_Chess
{
    public partial class LOGIN : Form
    {
        public LOGIN()
        {
            InitializeComponent();
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            MyDB db = new MyDB();
            string query = "SELECT * FROM ketqua ORDER BY lv_depth DESC";
            DataTable dt = db.getData_Table(query);

            dataGridView1.DataSource = dt;

            for (int i = 1; i <= 2; i++)
            {
                cbLevel.Items.Add(i);
            }

            string link_1 = "SELECT CASE  WHEN (SUM(win) + SUM(draw) + SUM(lose)) = 0 THEN 0  ELSE SUM(lose) * 100 / (SUM(win) + SUM(draw) + SUM(lose))  END AS Percentage FROM ketqua WHERE lv_depth = 1;";
            lv1.Text = "lv1: " + db.getScalar(link_1).ToString() + "%";
            string link_2 = "SELECT CASE  WHEN (SUM(win) + SUM(draw) + SUM(lose)) = 0 THEN 0  ELSE SUM(lose) * 100 / (SUM(win) + SUM(draw) + SUM(lose))  END AS Percentage FROM ketqua WHERE lv_depth = 2;";
            lv2.Text = "lv2: " + db.getScalar(link_2).ToString() + "%";
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string AI_mau;
            if (radioButton2.Checked) { AI_mau = "Đỏ"; }
            else AI_mau = "Đen";
            int level = 1;
            if (cbLevel.Text == "1") { level = 1; }
            if (cbLevel.Text == "2") { level = 2; }
            MyDB db = new MyDB();
            string query = "SELECT pass_word FROM taikhoan WHERE username = '" + txtUSER.Text + "'";
            if (txtPASS.Text == db.getScalarString(query))
            {
                COMPUTER Computer_Form = new COMPUTER(txtUSER.Text, level, AI_mau);
                this.Hide();
                Computer_Form.Show();
                Computer_Form.FormClosed += (s, args) => this.Show();
            }
            else MessageBox.Show("Không tồn tại Username / Sai mật khẩu !!!!!");

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            string AI_mau;
            if (radioButton1.Checked) { AI_mau = "Đỏ"; }
            else AI_mau = "Đen";
            MyDB db = new MyDB();
            string query = "SELECT username FROM taikhoan WHERE username = '" + txtUSER.Text + "'";
            if (txtUSER.Text != db.getScalarString(query))
            {
                string query_1 = "INSERT INTO taikhoan VALUES('" + txtUSER.Text + "','" + txtPASS.Text + "')";
                int add = db.getNonQuery(query_1);
                int level =1;
                if (cbLevel.Text == "1") { level = 1; }
                if(cbLevel.Text == "2") { level = 2; }
                MessageBox.Show("Chào người chơi mới <3");
                COMPUTER Computer_Form = new COMPUTER(txtUSER.Text, level,AI_mau);
                this.Hide();
                Computer_Form.Show();
                Computer_Form.FormClosed += (s, args) => this.Show();
            }
            else MessageBox.Show("Đã tồn tại Username !!!!!");
        }

        private void btnUPDATE_Click(object sender, EventArgs e)
        {
            MyDB db = new MyDB();
            string query = "SELECT * FROM ketqua ORDER BY lv_depth DESC";
            DataTable dt = db.getData_Table(query);

            dataGridView1.DataSource = dt;
        }
    }
}
