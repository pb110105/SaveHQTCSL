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

namespace QuanLyThuCungDBMS
{
    public partial class StaffForm : Form
    {
        string connectionString = @"Data Source=BARRY;Initial Catalog=ThuCungDB;User ID=sa;Password=Bao11012005!";
        private int maTK;
        private string hoTen;
        public StaffForm(int maTK, string hoTen)
        {
            InitializeComponent();
            this.maTK = maTK;
            this.hoTen = hoTen;
            LoadMaDichVu();
            LoadTaiKhoanKhachHang();
            LoadMaThuCungDV();
            LoadMaTaiKhoanHSYT();
            LoadMaThuCungHSYT();
            LoadLoaiTC();

            // gắn sự kiện ở code để chắc chắn nó được gọi

            // load dữ liệu khi form mở
            LoadLoaiTC();
        }
        //Xem thú cưng chưa bán
        private void LoadThuCungChuaBan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM V_ThuCungChuaBan"; // lấy từ view
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvThuCung.DataSource = dt; // dgvThuCung là DataGridView trên form
            }
        }
        //Xem thú cưng đã bán
        private void LoadThuCungDaBan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM V_ThuCungDaBan";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvThuCung.DataSource = dt;
            }
        }

        //Load mã dịch vụ
        private void LoadMaDichVu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaDV, TenDV FROM DichVuThuCung";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboDichVu.DataSource = dt;
                cboDichVu.DisplayMember = "TenDV"; // hiện tên dịch vụ
                cboDichVu.ValueMember = "MaDV";   // lấy mã dịch vụ
            }
        }
        //nút thú cưng đã bán
        private void btnThuCungDaBan_Click_1(object sender, EventArgs e)
        {
            LoadThuCungDaBan();
        }
        //nút thú cưng chưa bán
        private void btnThuCungChuaBan_Click(object sender, EventArgs e)
        {
            LoadThuCungChuaBan();
        }


        //Load Tài khoản khách hàng
        private void LoadTaiKhoanKhachHang()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaTK, HoTen FROM TaiKhoan WHERE VaiTro = N'KhachHang' AND TrangThai = N'HoatDong'";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboTaiKhoan.DataSource = dt;
                cboTaiKhoan.DisplayMember = "HoTen";   // hiển thị tên khách hàng
                cboTaiKhoan.ValueMember = "MaTK";      // lấy mã để truyền cho SP
                cboTaiKhoan.SelectedIndex = -1;        // chưa chọn gì
            }
        }
        //Data sử dụng dịch vụ
        private void LoadDataSDDV()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MaSD, MaDV, MaTK, MaTC, NgaySuDung, GhiChu FROM SuDungDichVu";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSuDungDV.DataSource = dt;
            }
        }
        //Load mã Thú cưng
        private void LoadMaThuCungDV()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaTC, TenTC FROM ThuCung";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboThuCung.DataSource = dt;
                cboThuCung.DisplayMember = "TenTC"; // hiện tên thú cưng
                cboThuCung.ValueMember = "MaTC";    // lấy mã thú cưng
            }
        }
        //Thêm thông tin sử dụng dịch vụ
        private void btnThemSDDV_Click(object sender, EventArgs e)
        {
            if (cboDichVu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ!");
                return;
            }
            if (cboTaiKhoan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!");
                return;
            }
            if (cboThuCung.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn thú cưng!");
                return;
            }

            int maDV = (int)cboDichVu.SelectedValue;
            int maTK = (int)cboTaiKhoan.SelectedValue;
            int maTC = (int)cboThuCung.SelectedValue;
            DateTime ngaySuDung = dtpNgaySuDung.Value;
            string ghiChu = txtGhiChu.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ThemSuDungDichVu", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaTC", maTC);
                    cmd.Parameters.AddWithValue("@MaDV", maDV);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@NgaySuDung", ngaySuDung);
                    cmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sử dụng dịch vụ thành công!");
                    LoadDataSDDV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sử dụng dịch vụ: " + ex.Message);
                }
            }
        }

        //Sửa thông tin sử dụng dịch vụ
        private void btnSuaSDDV_Click(object sender, EventArgs e)
        {
            if (dgvSuDungDV.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hồ sơ cần sửa!");
                return;
            }

            int maSD = (int)dgvSuDungDV.CurrentRow.Cells["MaSD"].Value;

            if (cboDichVu.SelectedValue == null || cboTaiKhoan.SelectedValue == null || cboThuCung.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!");
                return;
            }

            int maDV = (int)cboDichVu.SelectedValue;
            int maTK = (int)cboTaiKhoan.SelectedValue;
            int maTC = (int)cboThuCung.SelectedValue;
            DateTime ngaySuDung = dtpNgaySuDung.Value;
            string ghiChu = txtGhiChu.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_SuaSuDungDichVu", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaSD", maSD);
                    cmd.Parameters.AddWithValue("@MaDV", maDV);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@MaTC", maTC);
                    cmd.Parameters.AddWithValue("@NgaySuDung", ngaySuDung);
                    cmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa hồ sơ sử dụng dịch vụ thành công!");

                    LoadDataSDDV();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        //Hiển thhij thông tin sử dụng dịch vụ
        private void btnXemSDDV_Click(object sender, EventArgs e)
        {
            LoadDataSDDV();
        }
        //Xoá thông tin sử dụng dịch vụ

        private void btnXoaSDDV_Click(object sender, EventArgs e)
        {
            if (dgvSuDungDV.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hồ sơ cần xóa!");
                return;
            }

            int maSD = (int)dgvSuDungDV.CurrentRow.Cells["MaSD"].Value;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_XoaSuDungDichVu", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaSD", maSD);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa hồ sơ thành công!");

                    LoadDataSDDV();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        //Xem lịch sử dịch vụ
        private void btnLichSuSDDV_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM V_SuDungDichVu";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSuDungDV.DataSource = dt;
                dgvSuDungDV.ReadOnly = true;
                dgvSuDungDV.AllowUserToAddRows = false;
                dgvSuDungDV.AllowUserToDeleteRows = false;
                dgvSuDungDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        }
        //Xem hồ sơ y tế
        private void btnXemHSYT_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM V_HoSoYTe";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvHSYT.DataSource = dt;
                dgvHSYT.ReadOnly = true;
                dgvHSYT.AllowUserToAddRows = false;
                dgvHSYT.AllowUserToDeleteRows = false;
                dgvHSYT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        //Load mã thú cưng trong hồ sơ y tế
        private void LoadMaThuCungHSYT()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaTC, TenTC FROM ThuCung";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboMaTCHSYT.DataSource = dt;
                cboMaTCHSYT.DisplayMember = "TenTC";
                cboMaTCHSYT.ValueMember = "MaTC";
            }
        }
        //Load mã tài khoản trong hồ sơ y tế
        private void LoadMaTaiKhoanHSYT()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaTK, HoTen FROM TaiKhoan WHERE VaiTro = N'KhachHang' AND TrangThai = N'HoatDong'";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboMaTKHSYT.DataSource = dt;
                cboMaTKHSYT.DisplayMember = "HoTen";
                cboMaTKHSYT.ValueMember = "MaTK";
                cboMaTKHSYT.SelectedIndex = -1;
            }
        }
        //Thêm hồ sơ y tế
        private void btnThemHSYT_Click(object sender, EventArgs e)
        {
            if (cboMaTCHSYT.SelectedValue == null || cboMaTKHSYT.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn thú cưng và nhân viên.");
                return;
            }

            int maTC = Convert.ToInt32(cboMaTCHSYT.SelectedValue);
            int maTK = Convert.ToInt32(cboMaTKHSYT.SelectedValue);
            DateTime ngayKham = dtpNgayKham.Value;
            string noiDung = txtNoiDung.Text.Trim();
            string bacSiThuY = txtBSTY.Text.Trim();

            if (string.IsNullOrEmpty(noiDung) || string.IsNullOrEmpty(bacSiThuY))
            {
                MessageBox.Show("Nội dung và bác sĩ thú y không được để trống.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("ThemHoSoYTe", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MaTC", maTC);
                        cmd.Parameters.AddWithValue("@MaTK", maTK);
                        cmd.Parameters.AddWithValue("@NgayKham", ngayKham);
                        cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                        cmd.Parameters.AddWithValue("@BacSiThuY", bacSiThuY);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm hồ sơ y tế thành công!");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Number + " - " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        //Xoá hồ sơ y tế
        private void btnXoaHSYT_Click(object sender, EventArgs e)
        {
            if (dgvHSYT.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hồ sơ y tế để xóa.");
                return;
            }

            int maHS = Convert.ToInt32(dgvHSYT.SelectedRows[0].Cells["MaHS"].Value);

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa hồ sơ này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dr != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_XoaHoSoYTe", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaHS", maHS);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa hồ sơ y tế thành công!");
                    }

                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM V_HoSoYTe", conn)) 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    dgvHSYT.DataSource = dt;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Number + " - " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        //Sửa hồ sơ y tế
        private void btnSuaHSYT_Click(object sender, EventArgs e)
        {
            if (dgvHSYT.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hồ sơ cần sửa!");
                return;
            }

            int maHS = Convert.ToInt32(dgvHSYT.CurrentRow.Cells["MaHS"].Value);

            int maTC = Convert.ToInt32(cboMaTCHSYT.SelectedValue);
            int maTK = Convert.ToInt32(cboMaTKHSYT.SelectedValue);
            DateTime ngayKham = dtpNgayKham.Value;
            string noiDung = txtNoiDung.Text.Trim();
            string bacSiThuY = txtBSTY.Text.Trim();

            if (string.IsNullOrEmpty(noiDung) || string.IsNullOrEmpty(bacSiThuY))
            {
                MessageBox.Show("Nội dung và bác sĩ thú y không được để trống.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SuaHoSoYTe", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaHS", maHS);
                        cmd.Parameters.AddWithValue("@MaTC", maTC);
                        cmd.Parameters.AddWithValue("@MaTK", maTK);
                        cmd.Parameters.AddWithValue("@NgayKham", ngayKham);
                        cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                        cmd.Parameters.AddWithValue("@BacSiThuY", bacSiThuY);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật hồ sơ y tế thành công!");
                    }

                    DataTable dt = new DataTable();
                    using (SqlCommand reload = new SqlCommand("SELECT * FROM V_HoSoYTe", conn))
                    using (SqlDataReader reader = reload.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    dgvHSYT.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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

        // Load giong theo maLoai
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
            if (cboLoai.SelectedValue == null || cboLoai.SelectedValue is DataRowView)
                return;

            int maLoai = Convert.ToInt32(cboLoai.SelectedValue);
            LoadGiongTheoLoai(maLoai);
        }
        private void btnTimKiemTC_Click(object sender, EventArgs e)
        {
            int? loai = cboLoai.SelectedIndex >= 0 ? (int?)Convert.ToInt32(cboLoai.SelectedValue) : null;
            int? giong = cboGiong.SelectedIndex >= 0 ? (int?)Convert.ToInt32(cboGiong.SelectedValue) : null;
            int? giaMin = int.TryParse(txtGiaMin.Text, out int min) ? min : (int?)null;
            int? giaMax = int.TryParse(txtGiaMax.Text, out int max) ? max : (int?)null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand("sp_TimKiemThuCung", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Loai", (object)loai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Giong", (object)giong ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GiaMin", (object)giaMin ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GiaMax", (object)giaMax ?? DBNull.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                dgvThuCung.DataSource = dt;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
