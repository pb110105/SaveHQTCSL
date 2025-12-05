using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyThuCungDBMS
{
    public partial class SignUp : Form
    {
        string connectionString = @"Data Source=BARRY;Initial Catalog=ThuCungDB;User ID=sa;Password=Bao11012005!";
        public SignUp()
        {
            InitializeComponent();
        }
        private int TaoMaTaiKhoanMoi(SqlConnection conn)
        {
            string query = "SELECT ISNULL(MAX(MaTK), 0) + 1 FROM TaiKhoan";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text;
            string password = txtMatKhau.Text;
            string fullname = txtHoVaTen.Text;
            string email = txtEmail.Text;
            string phone = txtSDT.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Sinh MaTK mới
                    int maTK = TaoMaTaiKhoanMoi(conn);

                    string query = @"INSERT INTO TaiKhoan (MaTK, TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, VaiTro, NgayTao, TrangThai)
                                 VALUES (@MaTK, @TenDangNhap, @MatKhau, @HoTen, @Email, @SoDienThoai, N'KhachHang', GETDATE(), N'HoatDong')";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTK", maTK);
                        cmd.Parameters.AddWithValue("@TenDangNhap", username);
                        cmd.Parameters.AddWithValue("@MatKhau", password);
                        cmd.Parameters.AddWithValue("@HoTen", fullname);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@SoDienThoai", phone);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Đăng ký tài khoản thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Đăng ký thất bại!");
                        }
                    }
               
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Lỗi khác
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }
    }
}
