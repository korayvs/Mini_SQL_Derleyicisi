using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sorgu_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void DataB()
        {
            SqlConnection baglantiDB = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=master;Integrated Security=True");
            baglantiDB.Open();
            SqlCommand komut = new SqlCommand("Select name from sys.databases", baglantiDB);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                CmbDataB.Items.Add(dr[0].ToString());
            }
            baglantiDB.Close();
        }

        void Tbls()
        {
            SqlConnection baglantiTbl = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=" + CmbDataB.Text + ";Integrated Security=True");
            baglantiTbl.Open();
            SqlCommand komut1 = new SqlCommand("Select * From sys.Tables", baglantiTbl);
            SqlDataReader dr1 = komut1.ExecuteReader();

            while (dr1.Read())
            {
                CmbTablo.Items.Add(dr1[0].ToString());
            }
            baglantiTbl.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=" + CmbDataB.Text + ";Integrated Security=True");
                baglanti.Open();
                SqlCommand komut = new SqlCommand(richTextBox1.Text, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();

                SqlDataAdapter da = new SqlDataAdapter("Select * From" + " " + CmbTablo.Text, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Sorgunuzu Kontrol Edin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string sorgu = richTextBox1.Text;
            //try
            //{                
            //    baglanti.Open();
            //    SqlCommand komut = new SqlCommand(sorgu, baglanti);
            //    komut.ExecuteNonQuery();
            //    baglanti.Close();

            //    SqlDataAdapter da = new SqlDataAdapter("Select * From TBLKISILER", baglanti);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    dataGridView1.DataSource = dt;
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Sorgunuzu Kontrol Edin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //} 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataB();
            SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-F1A12T8\KORAY;Initial Catalog=" + CmbDataB.Text + ";Integrated Security=True");
        }

        private void CmbDataB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbTablo.Items.Clear();
            Tbls();
        }

        private void CmbTablo_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "Select * From" + " " + CmbTablo.Text;
        }
    }
}
