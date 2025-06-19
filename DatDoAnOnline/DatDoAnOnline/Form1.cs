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

namespace DatDoAnOnline
{
    public partial class Form1: Form
    {
        string connect = "Data Source=DODAT\\DODAT;Initial Catalog = datdoan; Integrated Security = True";
        SqlConnection conn = null;
        Random rnd = new Random();
        int maHoaDon;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void loadMenu()
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = conn.CreateCommand();
            cmd.CommandText = "select TenMon, Gia from MON_AN";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connect);
            conn.Open();
            loadMenu();
            maHoaDon = rnd.Next(100000, 999999);

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                label4.Text = row.Cells[0].Value.ToString();
                label13.Text = row.Cells[1].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select ID from MON_AN where TenMon = '"+label4.Text+"' ";
            var idmon = cmd.ExecuteScalar();
            var id = (int)idmon;
            SqlCommand cmd1 = null;
            cmd1 = conn.CreateCommand();
            cmd1.CommandText = "exec ThemOrCapNhatGioHang @iddonhang = '"+maHoaDon+"', @idmon = '" + id + "', @soluong = '"+ numericUpDown1.Value +"' ";
            cmd1.ExecuteNonQuery();
            loadHoaDon();
            loadtongtien();

        }

        public void loadHoaDon()
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = conn.CreateCommand();
            cmd.CommandText = "exec XemGioHang @iddonhang = '"+maHoaDon+"' ";
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
            cmd.CommandText = "exec TinhTongTien @iddonhang = '" + maHoaDon + "' ";
            var tongtien = cmd.ExecuteScalar();
            var tt = tongtien.ToString();
            textBox2.Text = tt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                label4.Text = row.Cells[0].Value.ToString();
                numericUpDown1.Text = row.Cells[1].Value.ToString();
            }
            SqlCommand cmd = null;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select Gia from MON_AN where TenMon = '" + label4.Text + "' ";
            var GiaMon = cmd.ExecuteScalar();
            var Gia = (string)GiaMon;
            label13.Text = Gia;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            cmd = conn.CreateCommand();
            cmd.CommandText = "insert into DonHang(iddonhang, tongtien, trangthai, create_at) values ('" + maHoaDon + "', '" + textBox2.Text + "', N'Đã Đặt', GETDATE())";
            cmd.ExecuteNonQuery();
            maHoaDon = rnd.Next(100000, 999999);
            ThanhToan thanhToan = new ThanhToan();
            thanhToan.Show();
        }
    }
}
