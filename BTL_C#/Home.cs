using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_C_
{
    public partial class Home : Form
    {
        Form formCon = null;

        public Home(int ChucVu, string tenNV)
        {
            InitializeComponent();

            lbTenNV.Text = tenNV;
            if (ChucVu != 1)
            {
                lbChucVu.Text = "Chức Vụ: Nhân Viên";
                lbNhaCungCap.Enabled = false;
                lbNhanVien.Enabled = false;
                lbThongKe.Enabled = false;
            }
            else lbChucVu.Text = "Chức Vụ: Quản Lý";
            lbNgayGio.Text = $"{DateTime.Now.DayOfWeek} ({DateTime.Now.ToString("dd-MM-yyyy")})";
            lbCopyright.Text = $"Copyright © {DateTime.Now.Year} | By Nhóm 4";
        }

        private void LoadFormCon(Form form)
        {
            if (formCon != null)
            {
                formCon.Close();
            }
            formCon = form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            content_right.Controls.Add(form);
            content_right.Tag = form;
            form.BringToFront();
            form.Show();
        }

        private void lbTrangChu_MouseHover(object sender, EventArgs e)
        {
            lbTrangChu.ForeColor = Color.Yellow;
            lbTrangChu.Cursor = Cursors.Hand;
        }

        private void lbTrangChu_MouseLeave(object sender, EventArgs e)
        {
            lbTrangChu.ForeColor = Color.White;
        }

        private void lbSanPham_MouseHover(object sender, EventArgs e)
        {
            lbSanPham.ForeColor = Color.Yellow;
            lbSanPham.Cursor = Cursors.Hand;
        }

        private void lbSanPham_MouseLeave(object sender, EventArgs e)
        {
            lbSanPham.ForeColor = Color.White;
        }

        private void lbXuatHang_MouseHover(object sender, EventArgs e)
        {
            lbXuatHang.ForeColor = Color.Yellow;
            lbXuatHang.Cursor = Cursors.Hand;
        }

        private void lbXuatHang_MouseLeave(object sender, EventArgs e)
        {
            lbXuatHang.ForeColor = Color.White;
        }

        private void lbNhapHang_MouseHover(object sender, EventArgs e)
        {
            lbNhapHang.ForeColor = Color.Yellow;
            lbNhapHang.Cursor = Cursors.Hand;
        }

        private void lbNhapHang_MouseLeave(object sender, EventArgs e)
        {
            lbNhapHang.ForeColor = Color.White;
        }

        private void lbNhaCungCap_MouseHover(object sender, EventArgs e)
        {
            lbNhaCungCap.ForeColor = Color.Yellow;
            lbNhaCungCap.Cursor = Cursors.Hand;
        }

        private void lbNhaCungCap_MouseLeave(object sender, EventArgs e)
        {
            lbNhaCungCap.ForeColor = Color.White;
        }

        private void lbNhanVien_MouseHover(object sender, EventArgs e)
        {
            lbNhanVien.ForeColor = Color.Yellow;
            lbNhanVien.Cursor = Cursors.Hand;
        }

        private void lbNhanVien_MouseLeave(object sender, EventArgs e)
        {
            lbNhanVien.ForeColor = Color.White;
        }

        private void lbThongKe_MouseHover(object sender, EventArgs e)
        {
            lbThongKe.ForeColor = Color.Yellow;
            lbThongKe.Cursor = Cursors.Hand;
        }

        private void lbThongKe_MouseLeave(object sender, EventArgs e)
        {
            lbThongKe.ForeColor = Color.White;
        }

        private void lbSanPham_Click(object sender, EventArgs e)
        {
            Form sanPham = new SanPham();
            LoadFormCon(sanPham);
        }

        private void lbTrangChu_Click(object sender, EventArgs e)
        {
            if (formCon != null)
            {
                formCon.Close();
            }
        }
    }
}
