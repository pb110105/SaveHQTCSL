namespace QuanLyThuCungDBMS
{
    partial class CustomerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnLichSuMua = new System.Windows.Forms.Button();
            this.btnXemTC = new System.Windows.Forms.Button();
            this.btnDatMua = new System.Windows.Forms.Button();
            this.dgvTC = new System.Windows.Forms.DataGridView();
            this.txtGiaMax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGiaMin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.cboGiong = new System.Windows.Forms.ComboBox();
            this.btnTimKiemTC = new System.Windows.Forms.Button();
            this.btnXemDVTC = new System.Windows.Forms.Button();
            this.btnDatDichVu = new System.Windows.Forms.Button();
            this.btnLichSuDichVu = new System.Windows.Forms.Button();
            this.btnHuyDichVu = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTC)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(-7, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1135, 685);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.btnHuyDichVu);
            this.tabPage1.Controls.Add(this.btnLichSuDichVu);
            this.tabPage1.Controls.Add(this.btnDatDichVu);
            this.tabPage1.Controls.Add(this.btnLichSuMua);
            this.tabPage1.Controls.Add(this.btnXemTC);
            this.tabPage1.Controls.Add(this.btnDatMua);
            this.tabPage1.Controls.Add(this.dgvTC);
            this.tabPage1.Controls.Add(this.txtGiaMax);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtGiaMin);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cboGioiTinh);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cboLoai);
            this.tabPage1.Controls.Add(this.cboGiong);
            this.tabPage1.Controls.Add(this.btnTimKiemTC);
            this.tabPage1.Controls.Add(this.btnXemDVTC);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1127, 643);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thú Cưng";
            // 
            // btnLichSuMua
            // 
            this.btnLichSuMua.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLichSuMua.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLichSuMua.Location = new System.Drawing.Point(336, 455);
            this.btnLichSuMua.Name = "btnLichSuMua";
            this.btnLichSuMua.Size = new System.Drawing.Size(189, 40);
            this.btnLichSuMua.TabIndex = 17;
            this.btnLichSuMua.Text = "Lịch Sử Mua";
            this.btnLichSuMua.UseVisualStyleBackColor = false;
            this.btnLichSuMua.Click += new System.EventHandler(this.btnLichSuMua_Click);
            // 
            // btnXemTC
            // 
            this.btnXemTC.BackColor = System.Drawing.Color.RosyBrown;
            this.btnXemTC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnXemTC.Location = new System.Drawing.Point(336, 409);
            this.btnXemTC.Name = "btnXemTC";
            this.btnXemTC.Size = new System.Drawing.Size(189, 40);
            this.btnXemTC.TabIndex = 16;
            this.btnXemTC.Text = "Xem Thú Cưng";
            this.btnXemTC.UseVisualStyleBackColor = false;
            this.btnXemTC.Click += new System.EventHandler(this.btnXemTC_Click);
            // 
            // btnDatMua
            // 
            this.btnDatMua.BackColor = System.Drawing.Color.RosyBrown;
            this.btnDatMua.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDatMua.Location = new System.Drawing.Point(531, 455);
            this.btnDatMua.Name = "btnDatMua";
            this.btnDatMua.Size = new System.Drawing.Size(147, 40);
            this.btnDatMua.TabIndex = 15;
            this.btnDatMua.Text = "Đặt Mua";
            this.btnDatMua.UseVisualStyleBackColor = false;
            this.btnDatMua.Click += new System.EventHandler(this.btnDatMua_Click);
            // 
            // dgvTC
            // 
            this.dgvTC.BackgroundColor = System.Drawing.Color.PaleTurquoise;
            this.dgvTC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTC.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvTC.Location = new System.Drawing.Point(3, 0);
            this.dgvTC.Name = "dgvTC";
            this.dgvTC.RowHeadersWidth = 62;
            this.dgvTC.RowTemplate.Height = 28;
            this.dgvTC.Size = new System.Drawing.Size(1124, 367);
            this.dgvTC.TabIndex = 14;
            // 
            // txtGiaMax
            // 
            this.txtGiaMax.Location = new System.Drawing.Point(456, 546);
            this.txtGiaMax.Name = "txtGiaMax";
            this.txtGiaMax.Size = new System.Drawing.Size(222, 35);
            this.txtGiaMax.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(393, 552);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "Đến";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(49, 546);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 29);
            this.label4.TabIndex = 11;
            this.label4.Text = "Giá Từ";
            // 
            // txtGiaMin
            // 
            this.txtGiaMin.Location = new System.Drawing.Point(165, 546);
            this.txtGiaMin.Name = "txtGiaMin";
            this.txtGiaMin.Size = new System.Drawing.Size(222, 35);
            this.txtGiaMin.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(48, 506);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "Giới Tính";
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Location = new System.Drawing.Point(165, 503);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(148, 37);
            this.cboGioiTinh.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(49, 463);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Giống";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel1.Location = new System.Drawing.Point(0, 361);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1131, 31);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(49, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Loài";
            // 
            // cboLoai
            // 
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Location = new System.Drawing.Point(165, 412);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(148, 37);
            this.cboLoai.TabIndex = 4;
            this.cboLoai.SelectedIndexChanged += new System.EventHandler(this.cboLoai_SelectedIndexChanged);
            // 
            // cboGiong
            // 
            this.cboGiong.FormattingEnabled = true;
            this.cboGiong.Location = new System.Drawing.Point(165, 455);
            this.cboGiong.Name = "cboGiong";
            this.cboGiong.Size = new System.Drawing.Size(148, 37);
            this.cboGiong.TabIndex = 3;
            // 
            // btnTimKiemTC
            // 
            this.btnTimKiemTC.BackColor = System.Drawing.Color.RosyBrown;
            this.btnTimKiemTC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTimKiemTC.Location = new System.Drawing.Point(531, 409);
            this.btnTimKiemTC.Name = "btnTimKiemTC";
            this.btnTimKiemTC.Size = new System.Drawing.Size(147, 40);
            this.btnTimKiemTC.TabIndex = 2;
            this.btnTimKiemTC.Text = "Tìm Kiếm";
            this.btnTimKiemTC.UseVisualStyleBackColor = false;
            this.btnTimKiemTC.Click += new System.EventHandler(this.btnTimKiemTC_Click);
            // 
            // btnXemDVTC
            // 
            this.btnXemDVTC.BackColor = System.Drawing.Color.RosyBrown;
            this.btnXemDVTC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnXemDVTC.Location = new System.Drawing.Point(803, 409);
            this.btnXemDVTC.Name = "btnXemDVTC";
            this.btnXemDVTC.Size = new System.Drawing.Size(312, 40);
            this.btnXemDVTC.TabIndex = 1;
            this.btnXemDVTC.Text = "Xem Dịch Vụ Thú Cưng";
            this.btnXemDVTC.UseVisualStyleBackColor = false;
            this.btnXemDVTC.Click += new System.EventHandler(this.btnXemDVTC_Click);
            // 
            // btnDatDichVu
            // 
            this.btnDatDichVu.BackColor = System.Drawing.Color.RosyBrown;
            this.btnDatDichVu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDatDichVu.Location = new System.Drawing.Point(803, 452);
            this.btnDatDichVu.Name = "btnDatDichVu";
            this.btnDatDichVu.Size = new System.Drawing.Size(312, 40);
            this.btnDatDichVu.TabIndex = 18;
            this.btnDatDichVu.Text = "Đặt Dịch Vụ";
            this.btnDatDichVu.UseVisualStyleBackColor = false;
            this.btnDatDichVu.Click += new System.EventHandler(this.btnDatDichVu_Click);
            // 
            // btnLichSuDichVu
            // 
            this.btnLichSuDichVu.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLichSuDichVu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLichSuDichVu.Location = new System.Drawing.Point(803, 498);
            this.btnLichSuDichVu.Name = "btnLichSuDichVu";
            this.btnLichSuDichVu.Size = new System.Drawing.Size(312, 40);
            this.btnLichSuDichVu.TabIndex = 19;
            this.btnLichSuDichVu.Text = "Dịch Vụ Đã Đặt";
            this.btnLichSuDichVu.UseVisualStyleBackColor = false;
            this.btnLichSuDichVu.Click += new System.EventHandler(this.btnLichSuDichVu_Click);
            // 
            // btnHuyDichVu
            // 
            this.btnHuyDichVu.BackColor = System.Drawing.Color.RosyBrown;
            this.btnHuyDichVu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnHuyDichVu.Location = new System.Drawing.Point(803, 541);
            this.btnHuyDichVu.Name = "btnHuyDichVu";
            this.btnHuyDichVu.Size = new System.Drawing.Size(312, 40);
            this.btnHuyDichVu.TabIndex = 20;
            this.btnHuyDichVu.Text = "Huỷ Dịch Vụ";
            this.btnHuyDichVu.UseVisualStyleBackColor = false;
            this.btnHuyDichVu.Click += new System.EventHandler(this.btnHuyDichVu_Click);
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 676);
            this.Controls.Add(this.tabControl1);
            this.Name = "CustomerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnXemDVTC;
        private System.Windows.Forms.Button btnTimKiemTC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.ComboBox cboGiong;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.TextBox txtGiaMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGiaMin;
        private System.Windows.Forms.DataGridView dgvTC;
        private System.Windows.Forms.Button btnDatMua;
        private System.Windows.Forms.Button btnXemTC;
        private System.Windows.Forms.Button btnLichSuMua;
        private System.Windows.Forms.Button btnDatDichVu;
        private System.Windows.Forms.Button btnLichSuDichVu;
        private System.Windows.Forms.Button btnHuyDichVu;
    }
}