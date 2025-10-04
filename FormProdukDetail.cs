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

namespace masterdataproduk
{
    public partial class FormProdukDetail : Form
    {
        public FormProdukDetail()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormProdukDetail_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = Koneksi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Id, NamaKategori FROM Kategori";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    var kategoriList = new List<KeyValuePair<int, string>>();

                    while (reader.Read())
                    {
                        kategoriList.Add(new KeyValuePair<int, string>(
                            (int)reader["Id"],
                            reader["NamaKategori"].ToString()
                        ));
                    }

                    cmbKategori.DataSource = kategoriList;
                    cmbKategori.DisplayMember = "Value";   // tampilkan nama kategori
                    cmbKategori.ValueMember = "Key";       // ambil Id kategori

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat kategori: " + ex.Message);
                }
            }
        }

        private void cmbKategori_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtHarga.Text, out _) || !int.TryParse(txtStok.Text, out _))
            {
                MessageBox.Show("Harga dan Stok harus diisi angka!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = Koneksi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Produk (NamaProduk, Harga, Stok, Kategorild, Deskripsi)
                             VALUES (@nama, @harga, @stok, @kategori, @deskripsi)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@nama", txtNamaProduk.Text);
                    cmd.Parameters.AddWithValue("@harga", Convert.ToDecimal(txtHarga.Text));
                    cmd.Parameters.AddWithValue("@stok", Convert.ToInt32(txtStok.Text));
                    cmd.Parameters.AddWithValue("@kategori", ((KeyValuePair<int, string>)cmbKategori.SelectedItem).Key);
                    cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produk berhasil ditambahkan!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menambahkan produk: " + ex.Message);
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            Close();
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtNamaProduk_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHarga_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
