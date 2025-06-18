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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DatDoOnline.Users
{
    public partial class TrangChu : Form
    {
        string connect = "Data Source=DODAT\\DODAT;Initial Catalog=BTL;Integrated Security=True";
        SqlConnection conn = null;
        
        private User _userlogin;
        public static User UserTT;

        public int mahoadon;

        public TrangChu(User userlogin)
        {
            InitializeComponent();
            _userlogin = userlogin;
            mahoadon = _userlogin.mahoadon;
        }
        private void TrangChu_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connect);
            conn.Open();
            loadtopbanchay();
            loadKho();
            loadDthuMon();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            mahoadon += 1;
            cmd = conn.CreateCommand();
            cmd.CommandText = "update Users set mahoadon = '"+mahoadon+"' where tennguoidung = '"+_userlogin.username+"'";
            cmd.ExecuteNonQuery();
            UserTT = new User()
            {
                username = _userlogin.username,
                mahoadon = mahoadon
            };
            Order order = new Order(UserTT);
            order.Show();
        }

        public void loadtopbanchay()
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from TopMonBanChay";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }
        public void loadKho()
        {
            SqlCommand cmd1 = null;
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            cmd1 = conn.CreateCommand();
            cmd1.CommandText = "select * from nguyenlieu";
            adapter1.SelectCommand = cmd1;
            dt1.Clear();
            adapter1.Fill(dt1);
            dataGridView2.DataSource = dt1;

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == 0)
            {
                loadDthuNgay();
            }
            else if (listBox2.SelectedIndex == 1) 
            {
                loadDthuMon();
            }
        }

        public void loadDthuNgay()
        {
            SqlCommand cmd2 = null;
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            DataTable dt2 = new DataTable();
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "select * from vw_DoanhThuTheoNgay";
            adapter2.SelectCommand = cmd2;
            dt2.Clear();
            adapter2.Fill(dt2);
            dataGridView4.DataSource = dt2;
        }
        public void loadDthuMon()
        {
            SqlCommand cmd2 = null;
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            DataTable dt2 = new DataTable();
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "select * from vw_DoanhThuTheoDanhMuc";
            adapter2.SelectCommand = cmd2;
            dt2.Clear();
            adapter2.Fill(dt2);
            dataGridView4.DataSource = dt2;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox3.SelectedIndex == 0)
            {
                label18.Visible = true;
                label19.Visible = true;
                listBox4.Visible = true;
                textBox9.Visible = true;
                label15.Visible = false;
                label16.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;

            }
            else if (listBox3.SelectedIndex == 1)
            {
                label18.Visible = false;
                label19.Visible = false;
                listBox4.Visible = false;
                textBox9.Visible = false;
                label15.Visible = true;
                label16.Visible = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
            }
            else if (listBox3.SelectedIndex == 2)
            {
                label18.Visible = false;
                label19.Visible = false;
                listBox4.Visible = false;
                textBox9.Visible = false;
                label15.Visible = true;
                label16.Visible = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
            }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {

            if (button12.Tag == "0")
            {
                button12.ForeColor = Color.White;
                button12.BackColor = Color.Green;
                button12.Text = "xác nhận thêm";
                label4.Visible = true;
                label8.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                button12.Tag = "1";
            }
            else if (button12.Tag == "1")
            {
                button12.ForeColor = Color.Black;
                button12.BackColor = Color.White;
                button12.Text = "Thêm vào kho";
                label4.Visible = false;
                label8.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                button12.Tag = "0";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            var giora = date.ToString("HH:mm:ss");
            SqlCommand cmd = null;
            cmd = conn.CreateCommand();
            cmd.CommandText = "exec pr_ChamCong @tennguoidung = '"+_userlogin.username+"', @ngaylam = '"+_userlogin.ngayvao+"', @giovao ='"+_userlogin.giovao+"', @giora = '"+giora+"'";
            cmd.ExecuteNonQuery();
            DangNhap dangNhap = new DangNhap();
            dangNhap.Show();
            this.Close();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                textBox8.Text = row.Cells[1].Value.ToString();
                textBox7.Text = row.Cells[2].Value.ToString();
                textBox6.Text = row.Cells[3].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex == 0)
            {
                var thang = listBox4.SelectedIndex +1;
                //textBox9.Text
                SqlCommand cmd = null;
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT dbo.fn_TinhDoanhThuTheoThang('" + thang + "', '" + textBox9.Text + "') AS tb";
                var tongloinhuan = cmd.ExecuteScalar();
                textBox1.Text = tongloinhuan.ToString();

            }
            else if (listBox3.SelectedIndex == 1)
            {
                SqlCommand cmd = null;
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT dbo.fn_TinhLoiNhuanTheoKhoangThoiGian('" + dateTimePicker1.Text + "', '" + dateTimePicker2.Text + "') AS tb";
                var tongloinhuan = cmd.ExecuteScalar();
                textBox1.Text = tongloinhuan.ToString();
            }
        }
    }
}
