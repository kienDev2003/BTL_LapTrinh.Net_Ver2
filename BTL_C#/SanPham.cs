using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_C_
{
    public partial class SanPham : Form
    {
        DBConnection DBConn = new DBConnection();

        public SanPham(int ChucVu)
        {
            InitializeComponent();
            this.ForeColor = Color.Black;

            if (ChucVu != 1)
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            LoadDanhSachSanPham();
        }

        private void LoadDanhSachSanPham()
        {
            lsvDanhSach.Items.Clear();
            DBConn.GetConnection();

            string query = "SELECT * FROM tblSanPham";
            using (SQLiteCommand cmd = new SQLiteCommand(query, DBConn.conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string maSP = reader.GetString(0);
                        string tenSP = reader.GetString(1);
                        string hang = reader.GetString(2);
                        string mauSac = reader.GetString(3);
                        string dungLuong = reader.GetInt32(4).ToString();
                        string soLuong = reader.GetInt32(5).ToString();
                        string giaBan = ChenDauCham(reader.GetDouble(6).ToString()) + " VNĐ";
                        string ngayCapNhat = reader.GetString(7);

                        ListViewItem lvi = new ListViewItem(maSP);
                        lvi.SubItems.Add(tenSP);
                        lvi.SubItems.Add(hang);
                        lvi.SubItems.Add(mauSac);
                        lvi.SubItems.Add(dungLuong);
                        lvi.SubItems.Add(soLuong);
                        lvi.SubItems.Add(giaBan);
                        lvi.SubItems.Add(ngayCapNhat);

                        lsvDanhSach.Items.Add(lvi);
                    }
                }
            }

            DBConn.CloseConnection();
        }

        private string ChenDauCham(string so)
        {
            int doDai = so.Length;
            int soDauCham = doDai / 3;

            for (int i = 1; i <= soDauCham; i++)
            {
                so = so.Insert(doDai - i * 3, ".");
            }

            return so;
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text;
            string tenSP = txtTenSP.Text;
            string hangSP = txtHang.Text;
            string mauSac = txtMauSac.Text;
            int dungLuong = 0;
            int soLuong = 0;
            double giaBan = 0;
            try
            {
                dungLuong = Convert.ToInt32(txtDungLuong.Text);
                soLuong = Convert.ToInt32(txtSoLuong.Text);
                giaBan = Convert.ToDouble(txtGiaBan.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng của 'Dung lượng','Số lượng','Giá bán'");
                return;
            }

            DateTime date = DateTime.Now;
            string ngayCapNhat = $"{date.ToString("hh:mm (dd-MM-yy)")}";

            if (maSP == "" || tenSP == "" || hangSP == "" || mauSac == "" || dungLuong == 0 || soLuong == 0 || giaBan == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DBConn.GetConnection();

            string query = "INSERT INTO tblSanPham (MaSP,TenSP,Hang,MauSac,DungLuong,SoLuong,GiaBan,NgayCapNhat)" +
                        " VALUES (@maSP,@tenSP,@hangSP,@mauSac,@dungLuong,@soLuong,@giaBan,@ngayCapNhat)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, DBConn.conn))
            {
                cmd.Parameters.AddWithValue("@maSP", maSP);
                cmd.Parameters.AddWithValue("@tenSP", tenSP);
                cmd.Parameters.AddWithValue("@hangSP", hangSP);
                cmd.Parameters.AddWithValue("@mauSac", mauSac);
                cmd.Parameters.AddWithValue("@dungLuong", dungLuong);
                cmd.Parameters.AddWithValue("@soLuong", soLuong);
                cmd.Parameters.AddWithValue("@giaBan", giaBan);
                cmd.Parameters.AddWithValue("@ngayCapNhat", ngayCapNhat);

                int check = cmd.ExecuteNonQuery();
                if (check > 0)
                {
                    MessageBox.Show("Thêm sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachSanPham();
                }
                else MessageBox.Show("Thêm sản phẩm KHÔNG thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DBConn.CloseConnection();
        }

        private void lsvDanhSach_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lsvDanhSach.SelectedItems[0];

                txtMaSP.Text = lvi.SubItems[0].Text;
                txtTenSP.Text = lvi.SubItems[1].Text;
                txtHang.Text = lvi.SubItems[2].Text;
                txtMauSac.Text = lvi.SubItems[3].Text;
                txtDungLuong.Text = lvi.SubItems[4].Text;
                txtSoLuong.Text = lvi.SubItems[5].Text;
                txtGiaBan.Text = lvi.SubItems[6].Text;
            }
        }
    }
}
