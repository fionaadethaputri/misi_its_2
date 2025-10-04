using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace masterdataproduk
{
    public partial class FormProdukUtama : Form
    {
        public FormProdukUtama()
        {
            InitializeComponent();
        }

        private void dgvProduk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadDataProduk()
        {
            dgvProduk.Rows.Clear();
            dgvProduk.Columns.Clear();

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT p.Id, p.NamaProduk, p.Harga, p.Stok, p.Deskripsi, k.NamaKategori 
                 FROM Produk p 
                 LEFT JOIN Kategori k ON p.Kategorild = k.Id";


                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // urutan kolom HARUS sesuai data
                    dgvProduk.Columns.Add("Id", "ID");
                    dgvProduk.Columns.Add("NamaProduk", "Nama Produk");
                    dgvProduk.Columns.Add("Harga", "Harga");
                    dgvProduk.Columns.Add("Stok", "Stok");
                    dgvProduk.Columns.Add("Deskripsi", "Deskripsi");
                    dgvProduk.Columns.Add("Kategori", "Kategori");

                    while (reader.Read())
                    {
                        dgvProduk.Rows.Add(
                            reader["Id"],
                            reader["NamaProduk"],
                            reader["Harga"],
                            reader["Stok"],
                            reader["Deskripsi"],
                            reader["NamaKategori"]
                        );
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menampilkan data: " + ex.Message);
                }
            }
        }


        private void FormProdukUtama_Load(object sender, EventArgs e)
        {
            LoadDataProduk();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            FormProdukDetail frm = new FormProdukDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataProduk(); 
            }
        }


        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvProduk.CurrentRow == null)
            {
                MessageBox.Show("Pilih baris produk terlebih dahulu!");
                return;
            }

            var value = dgvProduk.CurrentRow.Cells["Id"].Value;
            if (value == null || !int.TryParse(value.ToString(), out int id))
            {
                MessageBox.Show("ID produk tidak valid!");
                return;
            }

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Produk WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Produk berhasil dihapus!");
            LoadDataProduk();
        }




        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataProduk();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
          
        }




    }
        }



