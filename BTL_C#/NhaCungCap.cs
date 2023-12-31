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
    public partial class NhaCungCap : Form
    {
        DBConnection DBConn = new DBConnection();
        int chucVu = 0;

        public NhaCungCap(int ChucVu,string tenNV)
        {
            InitializeComponent();
            this.ForeColor = Color.Black;
            chucVu = ChucVu;
            if(chucVu != 1)
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            LoadDanhSachNhaCC();
        }

        private void LoadDanhSachNhaCC()
        {
            lsvDanhSach.Items.Clear();
            DBConn.GetConnection();

            string query = "SELECT * FROM tblNCC";
            using(SQLiteCommand cmd  = new SQLiteCommand(query,DBConn.conn)) 
            { 
                using(SQLiteDataReader reader =  cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string maNCC = reader.GetString(0);
                        string tenNCC = reader.GetString(1);
                        string SDT = reader.GetString(2);
                        string diaChi = reader.GetString(3);
                        string ngayCapNhat = reader.GetString(4);

                        ListViewItem lvi = new ListViewItem(maNCC);
                        lvi.SubItems.Add(tenNCC);
                        lvi.SubItems.Add(SDT);
                        lvi.SubItems.Add(diaChi);
                        lvi.SubItems.Add(ngayCapNhat);

                        lsvDanhSach.Items.Add(lvi);
                    }
                }
            }

            DBConn.CloseConnection();
        }
    }
}
