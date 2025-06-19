using DatDoAnOnline.HoaDon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DatDoAnOnline
{
    public partial class ThanhToan: Form
    {
        string connect = "Data Source=DODAT\\DODAT;Initial Catalog=datdoan;Integrated Security=True";
        SqlConnection conn = null;
        private MaHoaDon _maHoaDon;
        public int mahoadon;
        public ThanhToan(MaHoaDon maHoa)
        {
            InitializeComponent();
            _maHoaDon = maHoa;
            mahoadon = _maHoaDon.mahoadon;
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connect);
            conn.Open();
            textBox1.ForeColor = Color.Gray;
            loadHoaDon();
            loadtongtien();
        }

        public void loadHoaDon()
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = conn.CreateCommand();
            cmd.CommandText = "exec XemGioHang @iddonhang = '" + mahoadon + "' ";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public void loadtongtien()
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = conn.CreateCommand();
            cmd.CommandText = "exec TinhTongTien @iddonhang = '" + mahoadon + "' ";
            var tongtien = cmd.ExecuteScalar();
            var tt = tongtien.ToString();
            label6.Text = tt;
            label9.Text = tt;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                pictureBox1.Visible = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                pictureBox1.Visible = true;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Nhập địa chỉ nhận hàng...")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán!!", "Lỗi!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox1.Text == "Nhập địa chỉ nhận hàng...")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ nhận hàng!!", "Lỗi!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Đặt hàng thành công!!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                doitrangthai(mahoadon);
                this.Close();
            }
        }
        public void doitrangthai(int madonhang)
        {
            SqlCommand cmd = null;
            cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE DonHang SET trangthai = N'Đã Đặt' WHERE iddonhang = '"+madonhang+"' ";
            cmd.ExecuteNonQuery();
        }
    }
}
