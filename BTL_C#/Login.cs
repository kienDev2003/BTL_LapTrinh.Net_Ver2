﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_C_
{
    public partial class Login : Form
    {
        DBConnection DBConn = new DBConnection();

        public Login()
        {
            InitializeComponent();
        }

        private void lbDangNhap_Click(object sender, EventArgs e)
        {
            DBConn.GetConnection();

            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (taiKhoan == "" || matKhau == "") MessageBox.Show("Vui lòng nhập đầy đủ thông tin","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            else DangNhap(taiKhoan, matKhau);

            DBConn.CloseConnection();
        }

        private void DangNhap(string taiKhoan, string matKhau)
        {
            string query = @"SELECT * FROM tblTaiKhoan WHERE TaiKhoan = @taiKhoan
                             AND MatKhau = @matKhau";
            using(SQLiteCommand cmd = new SQLiteCommand(query,DBConn.conn))
            {
                cmd.Parameters.AddWithValue("@taiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@matKhau", matKhau);

                using(SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string tenNV = reader.GetString(1);
                        int ChucVu = reader.GetInt32(2);

                        this.Hide();
                        Form homeForm = new Home(ChucVu,tenNV);
                        homeForm.ShowDialog();
                        txtMatKhau.Text = "";
                        this.Show();
                    }
                    else MessageBox.Show("Tài khoản/Mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lbDangNhap_MouseHover(object sender, EventArgs e)
        {
            lbDangNhap.Cursor = Cursors.Hand;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn Thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }
}
