using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuCungDBMS
{
    public partial class CustomerForm : Form
    {
        string connectionString = @"Data Source=BARRY;Initial Catalog=ThuCungDB;User ID=sa;Password=Bao11012005!";
        private int maTK;
        private string hoTen;
        public CustomerForm(int maTK, string hoTen)
        {
            InitializeComponent();
            this.maTK = maTK;
            this.hoTen = hoTen;
            LoadGioiTinh();   // gọi hàm nạp giới tính
            LoadLoaiTC();
        }

       
        //Xem dịch vụ thú cưng
        private void btnXemDVTC_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM V_DichVu";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTC.DataSource = dt;
                dgvTC.ReadOnly = true;
                dgvTC.AllowUserToAddRows = false;
                dgvTC.AllowUserToDeleteRows = false;
                dgvTC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        private void LoadLoaiTC()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaLoai, TenLoai FROM LoaiThuCung";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboLoai.DataSource = dt;
                cboLoai.DisplayMember = "TenLoai";
                cboLoai.ValueMember = "MaLoai";
                cboLoai.SelectedIndex = -1;
            }
        }
        private void LoadGioiTinh()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Đực");
            cboGioiTinh.Items.Add("Cái");
            cboGioiTinh.SelectedIndex = 0;
        }
        private void LoadGiongTheoLoai(int maLoai)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaGiong, TenGiong FROM GiongThuCung WHERE MaLoai = @MaLoai";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@MaLoai", maLoai);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cboGiong.DataSource = dt;
                cboGiong.DisplayMember = "TenGiong";
                cboGiong.ValueMember = "MaGiong";
                cboGiong.SelectedIndex = -1;
            }
        }

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoai.SelectedValue != null && cboLoai.SelectedValue is int)
            {
                int maLoai = (int)cboLoai.SelectedValue;
                LoadGiongTheoLoai(maLoai);
            }
        }

        private void btnTimKiemTC_Click(object sender, EventArgs e)
        {
            int? loai = cboLoai.SelectedIndex >= 0 ? (int?)Convert.ToInt32(cboLoai.SelectedValue) : null;
            int? giong = cboGiong.SelectedIndex >= 0 ? (int?)Convert.ToInt32(cboGiong.SelectedValue) : null;
            string gioiTinh = cboGioiTinh.SelectedIndex >= 0 ? cboGioiTinh.SelectedItem.ToString() : null;

            decimal? giaMin = decimal.TryParse(txtGiaMin.Text, out decimal min) ? min : (decimal?)null;
            decimal? giaMax = decimal.TryParse(txtGiaMax.Text, out decimal max) ? max : (decimal?)null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                DataTable dt = new DataTable();

                using (SqlCommand cmd = new SqlCommand("sp_TimKiemThuCungKhachHang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Loai", (object)loai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Giong", (object)giong ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", (object)gioiTinh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GiaMin", (object)giaMin ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GiaMax", (object)giaMax ?? DBNull.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

                dgvTC.AutoGenerateColumns = true;   
                dgvTC.DataSource = null;           
                dgvTC.DataSource = dt;             
                dgvTC.Refresh();                   
                if (dgvTC.Columns.Contains("MaTC")) dgvTC.Columns["MaTC"].HeaderText = "Mã Thú Cưng";
                if (dgvTC.Columns.Contains("TenTC")) dgvTC.Columns["TenTC"].HeaderText = "Tên Thú Cưng";
                if (dgvTC.Columns.Contains("TenLoai")) dgvTC.Columns["TenLoai"].HeaderText = "Loài";
                if (dgvTC.Columns.Contains("TenGiong")) dgvTC.Columns["TenGiong"].HeaderText = "Giống";
                if (dgvTC.Columns.Contains("GioiTinh")) dgvTC.Columns["GioiTinh"].HeaderText = "Giới Tính";
                if (dgvTC.Columns.Contains("NgaySinh")) dgvTC.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                if (dgvTC.Columns.Contains("MauSac")) dgvTC.Columns["MauSac"].HeaderText = "Màu Sắc";
                if (dgvTC.Columns.Contains("GiaBan")) dgvTC.Columns["GiaBan"].HeaderText = "Giá Bán";
                if (dgvTC.Columns.Contains("TrangThai")) dgvTC.Columns["TrangThai"].HeaderText = "Trạng Thái";
            }
        }
        //Đặt mua thú cưng
        private void btnDatMua_Click(object sender, EventArgs e)
        {
            if (dgvTC.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thú cưng muốn mua.");
                return;
            }

            int maTC = Convert.ToInt32(dgvTC.CurrentRow.Cells["MaTC"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_DatMuaThuCung", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaTC", maTC);
                    cmd.Parameters.AddWithValue("@MaTK", maTK); 

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Đặt mua thành công!");
                    else
                        MessageBox.Show("Thú cưng này đã được bán trước đó.");
                }
            }

            // Refresh danh sách
            btnTimKiemTC.PerformClick();
        }
        //Xem thú cưng chưa bán
        private void btnXemTC_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_XemThuCungChuaBan", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }

                dgvTC.AutoGenerateColumns = true;
                dgvTC.DataSource = dt;

                // Đặt lại tên cột cho đẹp
                if (dgvTC.Columns.Contains("MaTC")) dgvTC.Columns["MaTC"].HeaderText = "Mã Thú Cưng";
                if (dgvTC.Columns.Contains("TenTC")) dgvTC.Columns["TenTC"].HeaderText = "Tên Thú Cưng";
                if (dgvTC.Columns.Contains("TenLoai")) dgvTC.Columns["TenLoai"].HeaderText = "Loài";
                if (dgvTC.Columns.Contains("TenGiong")) dgvTC.Columns["TenGiong"].HeaderText = "Giống";
                if (dgvTC.Columns.Contains("GioiTinh")) dgvTC.Columns["GioiTinh"].HeaderText = "Giới Tính";
                if (dgvTC.Columns.Contains("NgaySinh")) dgvTC.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                if (dgvTC.Columns.Contains("MauSac")) dgvTC.Columns["MauSac"].HeaderText = "Màu Sắc";
                if (dgvTC.Columns.Contains("GiaBan")) dgvTC.Columns["GiaBan"].HeaderText = "Giá Bán";
                if (dgvTC.Columns.Contains("TrangThai")) dgvTC.Columns["TrangThai"].HeaderText = "Trạng Thái";
            }
        }
        //Đặt mua
        private void btnLichSuMua_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_XemLichSuThuCung", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTK", maTK);

                DataTable dt = new DataTable();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }

                dgvTC.AutoGenerateColumns = true;
                dgvTC.DataSource = dt;
            }
        }
        //Đăng ký dịch vụ
        private void btnDatDichVu_Click(object sender, EventArgs e)
        {
            if (dgvTC.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để đặt.");
                return;
            }
            int maDV = Convert.ToInt32(dgvTC.CurrentRow.Cells["MaDV"].Value);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaTC, TenTC FROM ThuCung WHERE MaTK = @MaTK AND TrangThai = N'Đã bán'";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@MaTK", maTK); 
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn chưa có thú cưng nào để đăng ký dịch vụ.");
                    return;
                }
                int maTC = Convert.ToInt32(dt.Rows[0]["MaTC"]);
                DateTime ngaySuDung = DateTime.Now;
                string ghiChu = "Đặt trực tiếp từ DataGridView";

                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_DangKyDichVu", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaTC", maTC);
                    cmd.Parameters.AddWithValue("@MaDV", maDV);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@NgaySuDung", ngaySuDung);
                    cmd.Parameters.AddWithValue("@GhiChu", (object)ghiChu ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Đặt dịch vụ thành công!");
        }

        private void btnLichSuDichVu_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_XemDichVuDaDangKy", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTK", maTK); 
                DataTable dt = new DataTable();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }

                dgvTC.AutoGenerateColumns = true;
                dgvTC.DataSource = dt;
            }
        }
        //huỷ dịch vụ
        private void btnHuyDichVu_Click(object sender, EventArgs e)
        {
            if (dgvTC.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để hủy.");
                return;
            }
            int maSD = Convert.ToInt32(dgvTC.CurrentRow.Cells["MaSD"].Value);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_HuyDichVu", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaSD", maSD);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Hủy dịch vụ thành công!");
                    else
                        MessageBox.Show("Hủy dịch vụ thất bại. Vui lòng thử lại.");
                }
            }
            btnLichSuDichVu.PerformClick();

        }
    }
}
