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
    public partial class AdminForm : Form
    {
        string connectionString = @"Data Source=BARRY;Initial Catalog=ThuCungDB;User ID=sa;Password=Bao11012005!";
        private int maTK;
        private string hoTen;
        public AdminForm(int maTK, string hoTen)
        {
            InitializeComponent();
            this.maTK = maTK;
            this.hoTen = hoTen;
            LoadLoai();
            LoadTaiKhoan();
            LoadGioiTinh();
            LoadLoaiComboBox();
            LoadTrangThai(cboTrangThai);
            LoadVaiTro();
            LoadTrangThaiTK();

        }

        private void btnXemLoai_Click_1(object sender, EventArgs e)
        {
            LoadData("SELECT * FROM LoaiThuCung", dgvLoai);
        }
        private void btnXemGiong_Click_1(object sender, EventArgs e)
        {
            LoadData("SELECT * FROM GiongThuCung", dgvGiong);
        }
        private void btnXemTC_Click(object sender, EventArgs e)
        {
            LoadData("SELECT * FROM V_DanhSachThuCung", dgvTC);
        }
        private void btnXemTK_Click(object sender, EventArgs e)
        {
            LoadData("SELECT * FROM TaiKhoan", dgvTaiKhoan);
        }
        //view
        private void LoadData(string query, DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;  // dùng chung 1 DataGridView
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dgv.ScrollBars = ScrollBars.Both;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        //Combobox dành cho giới tính
        private void LoadLoai()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaLoai, TenLoai FROM LoaiThuCung", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboLoai.DisplayMember = "TenLoai"; // Hiển thị tên loài
                cboLoai.ValueMember = "MaLoai";     // Giá trị thực tế là MaLoai
                cboLoai.DataSource = dt;
                cboLoai.SelectedIndex = -1; // Không chọn mục nào ban đầu
            }
        }
        //Kiểm tra loài

        private bool CheckLoai(string tenLoai)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM LoaiThuCung WHERE TenLoai = @tenLoai";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenLoai", tenLoai);

                int count = (int)cmd.ExecuteScalar();
                return count > 0; // true = đã tồn tại
            }
        }
        //Thêm loài thú cưng
        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            string tenLoai = txtTenLoai.Text.Trim();
            string moTa = txtMoTaLoai.Text.Trim();
            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Tên loài không được để trống!");
                return;
            }
            if (CheckLoai(tenLoai))
            {
                MessageBox.Show("Loài này đã tồn tại!");
                return;
            }


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ThemLoaiThuCung", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                    cmd.Parameters.AddWithValue("@MoTa", moTa);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm loài thành công!");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khác: " + ex.Message);
                }
            }
        }
        //Hiển thị thông tin loài khi chọn dòng
        private void dgvLoai_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoai.CurrentRow != null)
            {
                txtTenLoai.Text = dgvLoai.CurrentRow.Cells["TenLoai"].Value.ToString();
                txtMoTaLoai.Text = dgvLoai.CurrentRow.Cells["MoTa"].Value.ToString();
            }
        }
        //Sửa loài
        private void btnSuaLoai_Click(object sender, EventArgs e)
        {
            if (dgvLoai.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một loài để sửa!");
                return;
            }

            string tenLoai = txtTenLoai.Text.Trim();
            string moTa = txtMoTaLoai.Text.Trim();

            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Tên loài không được để trống!");
                return;
            }

            // Lấy MaLoai từ dòng được chọn
            int maLoai = (int)dgvLoai.CurrentRow.Cells["MaLoai"].Value;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_SuaLoaiThuCung", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                    cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                    cmd.Parameters.AddWithValue("@MoTa", moTa);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa loài thành công!");

                    LoadData("SELECT * FROM LoaiThuCung", dgvLoai);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khác: " + ex.Message);
                }
            }
        }
        //Xoá loài
        private void btnXoaLoai_Click(object sender, EventArgs e)
        {
            if (dgvLoai.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một loài để xóa!");
                return;
            }

            int maLoai = (int)dgvLoai.CurrentRow.Cells["MaLoai"].Value;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa loài này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_XoaLoaiThuCung", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaLoai", maLoai);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa loài thành công!");

                    // Load lại DataGridView
                    LoadData("SELECT * FROM LoaiThuCung", dgvLoai);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khác: " + ex.Message);
                }
            }
        }

        //Kiểm tra tên giống đã tồn tại chưa
        private bool CheckGiong(int maLoai, string tenGiong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM GiongThuCung WHERE MaLoai = @maLoai AND TenGiong = @tenGiong";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@maLoai", maLoai);
                cmd.Parameters.AddWithValue("@tenGiong", tenGiong);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        //Thêm giống
        private void btnThemGiong_Click(object sender, EventArgs e)
        {

            int maLoai;
            if (cboMaLoai.SelectedValue != null)
            {
                maLoai = (int)cboMaLoai.SelectedValue;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Loài!");
                return;
            }
            string tenGiong = txtTenGiong.Text.Trim();
            string moTa = txtMoTaGiong.Text.Trim();

            if (string.IsNullOrEmpty(tenGiong))
            {
                MessageBox.Show("Tên giống không được để trống!");
                return;
            }

            if (CheckGiong(maLoai, tenGiong))
            {
                MessageBox.Show("Tên giống này đã tồn tại. Vui lòng nhập giống khác");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ThemGiongThuCung", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                    cmd.Parameters.AddWithValue("@TenGiong", tenGiong);
                    cmd.Parameters.AddWithValue("@MoTa", moTa);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm giống thành công!");

                    LoadData("SELECT * FROM GiongThuCung", dgvGiong);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khác: " + ex.Message);
                }
            }
        }
        //Load Loài Combobox
        private void LoadLoaiComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaLoai, TenLoai FROM LoaiThuCung";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboMaLoai.DataSource = dt;
                    cboMaLoai.DisplayMember = "TenLoai"; // Hiển thị tên loài
                    cboMaLoai.ValueMember = "MaLoai";    // Giá trị thực
                    cboMaLoai.SelectedIndex = -1;        // Không chọn mặc định
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load ComboBox: " + ex.Message);
                }
            }
        }
        //Load giống comboBox
        private void LoadGiongComboBox(int maLoai)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaGiong, TenGiong FROM GiongThuCung WHERE MaLoai=@MaLoai", conn);
                da.SelectCommand.Parameters.AddWithValue("@MaLoai", maLoai);
                da.Fill(dt);
                cboGiong.DataSource = dt;
                cboGiong.DisplayMember = "TenGiong";
                cboGiong.ValueMember = "MaGiong";
                cboGiong.SelectedIndex = -1;
            }
        }
        //Nút sửa giống
        private void btnSuaGiong_Click(object sender, EventArgs e)
        {
            if (dgvGiong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một giống để sửa!");
                return;
            }

            int maGiong = (int)dgvGiong.CurrentRow.Cells["MaGiong"].Value;

            int maLoai;
            if (cboMaLoai.SelectedValue != null)
            {
                maLoai = (int)cboMaLoai.SelectedValue;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Loài!");
                return;
            }
            string tenGiong = txtTenGiong.Text.Trim();
            string moTa = txtMoTaGiong.Text.Trim();

            if (string.IsNullOrEmpty(tenGiong))
            {
                MessageBox.Show("Tên giống không được để trống!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Kiểm tra trùng tên giống trong cùng loài (bỏ dòng hiện tại)
                    SqlCommand cmdCheck = new SqlCommand(
                        "SELECT COUNT(*) FROM GiongThuCung WHERE TenGiong=@tenGiong AND MaLoai=@maLoai AND MaGiong<>@maGiong", conn);
                    cmdCheck.Parameters.AddWithValue("@tenGiong", tenGiong);
                    cmdCheck.Parameters.AddWithValue("@maLoai", maLoai);
                    cmdCheck.Parameters.AddWithValue("@maGiong", maGiong);

                    if ((int)cmdCheck.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Tên giống đã tồn tại trong loài này!");
                        return;
                    }

                    // Gọi procedure sửa giống
                    SqlCommand cmd = new SqlCommand("sp_SuaGiongThuCung", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaGiong", maGiong);
                    cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                    cmd.Parameters.AddWithValue("@TenGiong", tenGiong);
                    cmd.Parameters.AddWithValue("@MoTa", moTa);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa giống thành công!");

                    // Load lại DataGridView
                    LoadData("SELECT * FROM GiongThuCung", dgvGiong);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khác: " + ex.Message);
                }
            }
        }
        //Xoá giống
        private void btnXoaGiong_Click(object sender, EventArgs e)
        {
            if (dgvGiong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một giống để xóa!");
                return;
            }

            int maGiong = (int)dgvGiong.CurrentRow.Cells["MaGiong"].Value;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa giống này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_XoaGiongThuCung", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaGiong", maGiong);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa giống thành công!");

                    // Load lại DataGridView
                    LoadData("SELECT * FROM GiongThuCung", dgvGiong);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khác: " + ex.Message);
                }
            }
        }
        //Combobox dành cho giới tính
        private void LoadGioiTinh()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Đực");
            cboGioiTinh.Items.Add("Cái");
            cboGioiTinh.SelectedIndex = -1; // Không chọn mặc định
        }
        //combobox dành cho tài khoản
        private void LoadTaiKhoan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT MaTK, HoTen + ' (' + TenDangNhap + ')' AS TenHienThi FROM TaiKhoan WHERE VaiTro = N'KhachHang' AND TrangThai = N'HoatDong'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboTaiKhoan.DisplayMember = "TenHienThi";
                cboTaiKhoan.ValueMember = "MaTK";
                cboTaiKhoan.DataSource = dt;
                cboTaiKhoan.SelectedIndex = -1;
            }
        }
        // Chọn loài
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoai.SelectedValue == null) return;

            int maLoai = (int)cboLoai.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT MaGiong, TenGiong FROM GiongThuCung WHERE MaLoai=@MaLoai", conn);
                da.SelectCommand.Parameters.AddWithValue("@MaLoai", maLoai);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cboGiong.DisplayMember = "TenGiong";
                cboGiong.ValueMember = "MaGiong";
                cboGiong.DataSource = dt;
                cboGiong.SelectedIndex = -1; // Không chọn mặc định
            }
        }


        //Thêm thú cưng
        private void btnThemTC_Click(object sender, EventArgs e)
        {
            string tenTC = txtTenThuCung.Text.Trim();
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString();
            DateTime ngaySinh = dtpNgaySinh.Value;
            string mauSac = txtMauSac.Text.Trim();
            string tinhTrangSK = txtTinhTrang.Text.Trim();

            decimal giaBan;

            if (string.IsNullOrEmpty(tenTC))
            {
                MessageBox.Show("Tên thú cưng không được để trống!");
                return;
            }

            if (cboLoai.SelectedValue == null || cboGiong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Loài và Giống!");
                return;
            }

            if (!decimal.TryParse(txtGiaBan.Text.Trim(), out giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ!");
                return;
            }

            int maLoai = (int)cboLoai.SelectedValue;
            int maGiong = (int)cboGiong.SelectedValue;
            int maTK;
            if (cboTaiKhoan.SelectedValue != null)
            {
                maTK = (int)cboTaiKhoan.SelectedValue; // Lấy MaTK từ ComboBox
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Kiểm tra loài tồn tại
                    SqlCommand cmdCheckLoai = new SqlCommand(
                        "SELECT COUNT(*) FROM LoaiThuCung WHERE MaLoai=@maLoai", conn);
                    cmdCheckLoai.Parameters.AddWithValue("@maLoai", maLoai);
                    if ((int)cmdCheckLoai.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("Loài không tồn tại!");
                        return;
                    }

                    // Kiểm tra giống tồn tại và thuộc loài
                    SqlCommand cmdCheckGiong = new SqlCommand(
                        "SELECT COUNT(*) FROM GiongThuCung WHERE MaGiong=@maGiong AND MaLoai=@maLoai", conn);
                    cmdCheckGiong.Parameters.AddWithValue("@maGiong", maGiong);
                    cmdCheckGiong.Parameters.AddWithValue("@maLoai", maLoai);
                    if ((int)cmdCheckGiong.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("Giống không tồn tại hoặc không thuộc loài!");
                        return;
                    }

                    // Gọi procedure thêm thú cưng
                    SqlCommand cmd = new SqlCommand("sp_ThemThuCung", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TenTC", tenTC);
                    cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                    cmd.Parameters.AddWithValue("@MaGiong", maGiong);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@MauSac", mauSac);
                    cmd.Parameters.AddWithValue("@TinhTrangSK", tinhTrangSK);
                    cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thú cưng thành công!");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khác: " + ex.Message);
                }
            }
        }
        private void LoadTrangThai(ComboBox cboTrangThai)
        {
            // Danh sách trạng thái cố định
            List<string> dsTrangThai = new List<string>() { "Chưa bán", "Đã bán" };

            cboTrangThai.DataSource = dsTrangThai;
            cboTrangThai.SelectedIndex = 0; // mặc định chọn "Chưa bán"
        }

        //Sửa thú cưng
        private void btnSuaTC_Click(object sender, EventArgs e)
        {
            if (dgvTC.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thú cưng cần sửa!");
                return;
            }

            int maTC = (int)dgvTC.CurrentRow.Cells["MaTC"].Value;

            if (cboLoai.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Loài!");
                return;
            }
            int maLoai = (int)cboLoai.SelectedValue;
            if (cboGiong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Giống!");
                return;
            }
            int maGiong = (int)cboGiong.SelectedValue;

            // Các thông tin khác
            string tenTC = txtTenThuCung.Text.Trim();
            if (string.IsNullOrEmpty(tenTC))
            {
                MessageBox.Show("Tên thú cưng không được để trống!");
                return;
            }

            if (cboGioiTinh.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Giới tính!");
                return;
            }
            string gioiTinh = cboGioiTinh.SelectedItem.ToString();

            DateTime ngaySinh = dtpNgaySinh.Value;
            string mauSac = txtMauSac.Text.Trim();
            string tinhTrangSK = txtTinhTrang.Text.Trim();

            decimal giaBan;
            if (!decimal.TryParse(txtGiaBan.Text.Trim(), out giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ!");
                return;
            }
            string trangThai = (cboTrangThai.SelectedItem == null) ? "Chưa bán" : cboTrangThai.SelectedItem.ToString();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_SuaThuCung", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaTC", maTC);
                    cmd.Parameters.AddWithValue("@TenTC", tenTC);
                    cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                    cmd.Parameters.AddWithValue("@MaGiong", maGiong);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@MauSac", mauSac);
                    cmd.Parameters.AddWithValue("@TinhTrangSK", tinhTrangSK);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    cmd.Parameters.AddWithValue("@GiaBan", giaBan);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thú cưng thành công!");

                    LoadData("SELECT * FROM V_DanhSachThuCung", dgvTC);
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

        private void btnXoaTC_Click(object sender, EventArgs e)
        {
            if (dgvTC.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thú cưng cần xoá!");
                return;
            }

            int maTC = Convert.ToInt32(dgvTC.CurrentRow.Cells["MaTC"].Value);

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xoá thú cưng có mã " + maTC + " không?",
                "Xác nhận xoá",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("sp_XoaThuCung", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaTC", maTC);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Xoá thú cưng thành công!");
                            LoadData("SELECT * FROM V_DanhSachThuCung", dgvTC);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thú cưng để xoá!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                    }
                }
            }
        }
        //Load vài trò
        private void LoadVaiTro()
        {
            List<string> roles = new List<string> { "KhachHang", "NhanVien", "ChuCuaHang" };
            cboVaiTro.DataSource = roles;
            cboVaiTro.SelectedIndex = 1;
        }
        //Load trạng thái
        private void LoadTrangThaiTK()
        {
            List<string> statuses = new List<string> { "HoatDong", "BiKhoa" };
            cboTrangThaiTK.DataSource = statuses;
            cboTrangThaiTK.SelectedIndex = 0;
        }
        //Thêm tài khoản
        private void btnThemTK_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu trống
                if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
                {
                    MessageBox.Show("Tên đăng nhập không được để trống!");
                    txtTenDangNhap.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
                {
                    MessageBox.Show("Mật khẩu không được để trống!");
                    txtMatKhau.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Họ tên không được để trống!");
                    txtHoTen.Focus();
                    return;
                }
                if (cboVaiTro.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn vai trò!");
                    cboVaiTro.Focus();
                    return;
                }
                if (cboTrangThaiTK.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái!");
                    cboTrangThaiTK.Focus();
                    return;
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ThemTaiKhoan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                    cmd.Parameters.AddWithValue("@VaiTro", cboVaiTro.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@TrangThai", cboTrangThaiTK.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm tài khoản thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm tài khoản: " + ex.Message);
            }
        }
        //Sửa tài khoản
        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!");
                return;
            }

            int maTK = (int)dgvTaiKhoan.CurrentRow.Cells["MaTK"].Value;

            // Các thông tin khác
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Tên đăng nhập không được để trống!");
                return;
            }

            string matKhau = txtMatKhau.Text.Trim();
            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Mật khẩu không được để trống!");
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên không được để trống!");
                return;
            }

            string email = txtEmail.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();

            if (cboVaiTro.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn vai trò!");
                return;
            }
            string vaiTro = cboVaiTro.SelectedItem.ToString();

            if (cboTrangThaiTK.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!");
                return;
            }
            string trangThai = cboTrangThaiTK.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_SuaTaiKhoan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                    cmd.Parameters.AddWithValue("@SoDienThoai", string.IsNullOrEmpty(soDienThoai) ? (object)DBNull.Value : soDienThoai);
                    cmd.Parameters.AddWithValue("@VaiTro", vaiTro);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa tài khoản thành công!");

                    LoadData("SELECT * FROM TaiKhoan", dgvTaiKhoan);
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

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!");
                return;
            }
            int maTK = (int)dgvTaiKhoan.CurrentRow.Cells["MaTK"].Value;
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tài khoản này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_XoaTaiKhoan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaTK", maTK);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa tài khoản thành công!");
                    LoadData("SELECT * FROM TaiKhoan", dgvTaiKhoan);
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
        //View tài khoản nhân viên
        private void btnTKNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM V_NhanVien", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    dgvThongKe.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        //View tài khoản khách hàng
        private void btnTKKH_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM V_KhachHang", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    dgvThongKe.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        //Doanh thu thú cưng
        private void btnDTBTC_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM V_DoanhThuThuCung", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    dgvThongKe.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnDTDV_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM V_DoanhThuDichVu", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    dgvThongKe.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnThuCungChuaBan_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ThongKeThuCungTonKho", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                dgvThongKe.DataSource = dt;
                if (dgvThongKe.Columns.Contains("TenLoai"))
                    dgvThongKe.Columns["TenLoai"].HeaderText = "Loài";
                if (dgvThongKe.Columns.Contains("TenGiong"))
                    dgvThongKe.Columns["TenGiong"].HeaderText = "Giống";
                if (dgvThongKe.Columns.Contains("GioiTinh"))
                    dgvThongKe.Columns["GioiTinh"].HeaderText = "Giới tính";
                if (dgvThongKe.Columns.Contains("SoLuongCon"))
                    dgvThongKe.Columns["SoLuongCon"].HeaderText = "Số lượng còn";
            }
        }

        private void btnThuCungBanChay_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("sp_ThongKeLoaiGiongBanChay", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvThongKe.DataSource = dt;

                if (dgvThongKe.Columns.Contains("TenLoai")) dgvThongKe.Columns["TenLoai"].HeaderText = "Loài";
                if (dgvThongKe.Columns.Contains("TenGiong")) dgvThongKe.Columns["TenGiong"].HeaderText = "Giống";
                if (dgvThongKe.Columns.Contains("SoLuongBan")) dgvThongKe.Columns["SoLuongBan"].HeaderText = "Số lượng bán";
            }
        }
    }
}
