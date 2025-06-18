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

namespace DatDoOnline.Users
{
    public partial class Order : Form
    {
        private User _usreTT;

        string constr = "Data Source=DODAT\\DODAT;Initial Catalog=BTL;Integrated Security=True";
        SqlConnection conn = null;
        public string pttt;
        
        public Order(User UserTT)
        {
            InitializeComponent();
            _usreTT = UserTT;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Order_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(constr);
            conn.Open();
            loadMenu();
            loadtongtien();
        }


        public void loadMenu()
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = conn.CreateCommand();
            cmd.CommandText = "select tenmon, dongia from menu";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void loadHoaDon()
        {
            SqlCommand cmd1= null;
            SqlDataAdapter adapter1= new SqlDataAdapter();
            DataTable dt1= new DataTable();
            cmd1= conn.CreateCommand();
            cmd1.CommandText = "select idnguoidung from Users where tennguoidung = '" + _usreTT.username + "' ";
            var idnguoidung = cmd1.ExecuteScalar();
            var id = (int)idnguoidung;
            cmd1 = conn.CreateCommand();
            cmd1.CommandText = "exec XemHoaDon @idnguoidung = '" + id + "', @idhoadon = '" + _usreTT.mahoadon + "' ";
            adapter1.SelectCommand = cmd1;
            dt1.Clear();
            adapter1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }

        public void loadtongtien()
        {
            SqlCommand cmd2 = null;
            cmd2 = conn.CreateCommand();
            cmd2.CommandText = "select idnguoidung from Users where tennguoidung = '" + _usreTT.username + "' ";
            var idnguoidung = cmd2.ExecuteScalar();
            var id = (int)idnguoidung;
            cmd2.CommandText = "select dbo.fn_Tonghoadon (" + id + ", " + _usreTT.mahoadon + ") as tb";
            var tongtien = cmd2.ExecuteScalar();
            var tt = tongtien.ToString();
            textBox4.Text = tt;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select idnguoidung from Users where tennguoidung = '" + _usreTT.username + "' ";
            var idnguoidung = cmd.ExecuteScalar();
            var id = (int)idnguoidung;
            cmd.CommandText = "exec orderbill @idnguoidung = '" + id + "',@idhoadon = '" + _usreTT.mahoadon + "', @tenmon = N'" + textBox1.Text + "', @soluong ='" + numericUpDown1.Value + "'";
            cmd.ExecuteNonQuery();
            loadHoaDon();
            loadtongtien();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            cmd = conn.CreateCommand();
            cmd.CommandText = "exec pr_luuthanhtoan @idhoadon = '"+_usreTT.mahoadon+"', @pttt = '"+pttt+"', @tongtien = '"+textBox4.Text+"'  ";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Thanh toán thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                pttt = listBox2.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục trong ListBox1 trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from dbo.timkiemmon('" + textBox5.Text + "')";
            cmd.ExecuteNonQuery();
            loadMenu();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var danhmuc = listBox1.SelectedItem.ToString();
            textBox2.Text = danhmuc;
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from dbo.DanhSachMonAnTheoDanhMuc(N'" + danhmuc + "') ";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
