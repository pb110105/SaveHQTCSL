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

    public partial class LoginForm : Form
    {
        string connectionString = @"Data Source=BARRY;Initial Catalog=ThuCungDB;User ID=sa;Password=Bao11012005!";

        public LoginForm()
        {
            InitializeComponent();
        }
        public bool KiemTraKetNoi(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); // Mở kết nối
                    MessageBox.Show("Kết nối thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra kết nối " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (!KiemTraKetNoi(connectionString))
            {
                return; // Không mở form nếu chưa kết nối
            }
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    
                    conn.Open();
                    string query = "SELECT MaTK, VaiTro, HoTen FROM TaiKhoan WHERE TenDangNhap=@username AND MatKhau=@password AND TrangThai=N'HoatDong'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int maTK = (int)reader["MaTK"];
                        string vaiTro = reader["VaiTro"].ToString();
                        string hoTen = reader["HoTen"].ToString();

                        this.Hide();

                        if (vaiTro == "ChuCuaHang")
                        {
                            var adminForm = new AdminForm(maTK, hoTen);
                            adminForm.ShowDialog();
                        }
                        else if (vaiTro == "NhanVien")
                        {
                            var staffForm = new StaffForm(maTK, hoTen);
                            staffForm.ShowDialog();
                        }
                        else // KhachHang
                        {
                            var customerForm = new CustomerForm(maTK, hoTen);
                            customerForm.ShowDialog();
                        }
                        this.Show();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(
                            "Tên đăng nhập hoặc mật khẩu sai, hoặc tài khoản chưa đăng ký!\nBạn có muốn đăng ký tài khoản mới không?",
                            "Đăng nhập thất bại",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            var registerForm = new SignUp();
                            registerForm.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message,
                           "Lỗi kết nối",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                }
            }
        }

        
    }
}