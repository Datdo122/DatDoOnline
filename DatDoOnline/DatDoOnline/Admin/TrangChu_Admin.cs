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

namespace DatDoOnline.Admin
{
    public partial class TrangChu_Admin : Form
    {
        string constr = "Data Source=DODAT\\DODAT;Initial Catalog=BTL;Integrated Security=True";
        SqlConnection conn = null;
        public TrangChu_Admin()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void TrangChu_Admin_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(constr);
            conn.Open();
            loadMenu();
            loadKho();
            loadFullLuong();
            loadDthuMon();
        }

        public void loadMenu()
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = conn.CreateCommand();
            cmd.CommandText = "select a.id, a.tenmon, a.dongia, b.tendanhmuc from menu a, danhmuc_menu b where a.iddanhmuc = b.id";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
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

        private void button12_Click(object sender, EventArgs e)
        {
            if (button12.Tag == "0")
            {
                button12.ForeColor = Color.White;
                button12.BackColor = Color.Green;
                button12.Text = "xác nhận thêm";
                label1.Visible = true;
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
                label1.Visible = false;
                label8.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                button12.Tag = "0";
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            
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

        private void button11_Click(object sender, EventArgs e)
        {
            if (button11.Tag == "0")
            {
                label24.Visible = true;
                textBox13.Visible = true;
                button11.Text = "xác nhận thêm món";
                button11.ForeColor = Color.White;
                button11.BackColor = Color.Green;
                button11.Tag = "1";
            }
            else
            {
                if (textBox13.Text == "")
                {
                    var danhmuc = listBox1.SelectedItem.ToString();
                    SqlCommand cmd = null;
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "exec themon @tenmon = '" + textBox1.Text + "', @dongia = '" + textBox2.Text + "', @tendanhmuc = '" + danhmuc + "' ";
                    var thongbao = cmd.ExecuteScalar();
                    var tb = (int)thongbao;
                    if (tb == 0)
                    {
                        label25.Visible = true;
                        textBox1.Clear();
                    }
                    else
                    {
                        label25.Visible = false;
                        MessageBox.Show("Thêm món thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {

                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        public void loadFullLuong()
        {
            SqlCommand cmd3 = null;
            SqlDataAdapter adapter3 = new SqlDataAdapter();
            DataTable dt3 = new DataTable();
            cmd3 = conn.CreateCommand();
            cmd3.CommandText = "select * from v_TongLuong";
            adapter3.SelectCommand = cmd3;
            dt3.Clear();
            adapter3.Fill(dt3);
            dataGridView3.DataSource = dt3;
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

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                textBox10.Text = row.Cells[0].Value.ToString();
                textBox12.Text = row.Cells[4].Value.ToString();
            }
            SqlCommand cmd = null;
            cmd = conn.CreateCommand();
            cmd.CommandText = "select dbo.fn_TongGioLam_2('" + textBox10.Text + "') as tb";
            var tb = cmd.ExecuteScalar();
            textBox11.Text = tb.ToString();
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
    }
}
