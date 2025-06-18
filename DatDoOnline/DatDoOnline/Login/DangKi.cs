using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatDoOnline.Login
{
    public partial class DangKi : Form
    {
        string connect = "Data Source=DODAT\\DODAT;Initial Catalog=BTL;Integrated Security=True";
        SqlConnection conn = null;
        SqlCommand cmd = null;
        public DangKi()
        {
            InitializeComponent();
        }

        private void DangKi_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connect);
            conn.Open();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            DangNhap dangNhap = new DangNhap();
            dangNhap.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" | textBox2.Text == "" | textBox3.Text == "")
            {
                label9.Text = "Vui lòng nhập thông tin!!";
                label9.Visible = true;
                label9.ForeColor = Color.Red;
            }
            else 
            {
                label11.Visible = false;
                if (textBox2.Text != textBox3.Text)
                {
                    label9.Visible = false;
                    label11.Text = "Mật khẩu chưa trùng khớp!!";
                    label11.Visible = true;
                    label11.ForeColor = Color.Red;
                }
                else
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "exec luuUsers @ten = '"+textBox1.Text+"', @pw = '"+textBox2.Text+"'";
                    var checktk = cmd.ExecuteScalar();
                    var check = (int)checktk;
                    if (check == 1)
                    {
                        label9.Text = "Tên đăng nhập đã tồn tại!!";
                        label9.Visible = true;
                        label9.ForeColor = Color.Red;
                    }
                    else
                    {
                        MessageBox.Show("Đăng kí thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DangNhap dangNhap = new DangNhap();
                        dangNhap.Show();
                        this.Close();
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        public void chinhtext()
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
