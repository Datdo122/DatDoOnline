using DatDoOnline.Admin;
using DatDoOnline.Login;
using DatDoOnline.Users;
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

namespace DatDoOnline
{
    public partial class DangNhap : Form
    {
        string connect = "Data Source=DODAT\\DODAT;Initial Catalog=BTL;Integrated Security=True";
        SqlConnection conn = null;
        SqlCommand cmd = null;

        public static User userLogin;
        public DangNhap()
        {
            InitializeComponent();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connect);
            conn.Open();
        }


        private void label5_Click(object sender, EventArgs e)
        {
            DangKi dangKi = new DangKi();
            dangKi.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = conn.CreateCommand();
            cmd.CommandText = "select dbo.ktradangnhap('" + textBox1.Text + "', '" + textBox2.Text + "') as TB";
            var ktra = cmd.ExecuteScalar();
            var kt = (int)ktra;
            if (kt == 0)
            {
                label6.Visible = true;
                textBox2.Clear();
            }
            else
            {
                cmd.CommandText = "select dbo.ktrarole('" + textBox1.Text + "') as Role";
                var role = cmd.ExecuteScalar();
                var rl = (int)role;
                if (rl == 0)
                {
                    DateTime dateTime = DateTime.Now;
                    string day = dateTime.ToString("yyyy-MM-dd");
                    string time = dateTime.ToString("HH:mm:ss");
                    cmd.CommandText = "select mahoadon from Users where tennguoidung = '" + textBox1.Text + "' ";
                    var mahoaddon = cmd.ExecuteScalar();
                    var mhd = (int)mahoaddon;
                    userLogin = new User
                    {
                        username = textBox1.Text,
                        mahoadon = mhd,
                        ngayvao = day,
                        giovao = time,
                    };
                    TrangChu trangChu = new TrangChu(userLogin);
                    trangChu.Show();
                    this.Hide();
                }
                else
                {
                    TrangChu_Admin trangChu_Admin = new TrangChu_Admin();
                    trangChu_Admin.Show();
                    this.Hide();
                }
            }

        }
    }
}
