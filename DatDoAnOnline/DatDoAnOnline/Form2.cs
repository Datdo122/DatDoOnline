using DatDoAnOnline.HoaDon;
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

namespace DatDoAnOnline
{
    public partial class Form2: Form
    {
        string connect = "Data Source=DODAT\\DODAT;Initial Catalog=datdoan;Integrated Security=True";
        SqlConnection conn = null;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connect);
            conn.Open();
            LoadData();
        }
        public void LoadData()
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = conn.CreateCommand();
            cmd.CommandText = "select iddonhang, create_at, tongtien, trangthai from DonHang where trangthai = N'Đã Đặt' ";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
