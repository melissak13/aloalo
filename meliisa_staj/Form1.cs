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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.Data.Common;


namespace meliisa_staj
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
                


            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = false;
            listView1.Columns.Add("Üretilen Ürün Sayısı");
            listView1.Columns.Add("Kusur Sayısı");
            listView1.Columns.Add("Kusur Oranı (%)");
            label2.Text = "0";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }


            double toplamUretilen = Convert.ToDouble(textBox1.Text);
            double kusurluSayisi = Convert.ToDouble(textBox2.Text);

            if (toplamUretilen == 0)
            {
                MessageBox.Show("Üretilen ürün sayısı 0 olamaz.");
                return;
            }


            double kusurOrani = (kusurluSayisi / toplamUretilen) * 100;


            string query = "INSERT INTO KusurOranlari (UretilenUrunSayisi, KusurSayisi, KusurOrani) VALUES (@UretilenUrunSayisi, @KusurSayisi, @KusurOrani)"; 
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@UretilenUrunSayisi", toplamUretilen);
            sqlCommand.Parameters.AddWithValue("@KusurSayisi", kusurluSayisi);
            sqlCommand.Parameters.AddWithValue("@KusurOrani", kusurOrani);

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();


            string[] row = { textBox1.Text, textBox2.Text, kusurOrani.ToString("F2") };
            var satir = new ListViewItem(row);
            listView1.Items.Add(satir);
            label2.Text = listView1.Items.Count.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                double kusurOraniToplam = 0;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    string deger = listView1.Items[i].SubItems[2].Text;
                    kusurOraniToplam += Convert.ToDouble(deger);
                }
                double kusurOrani = kusurOraniToplam / listView1.Items.Count;

                if (kusurOrani > 10)
                {
                    MessageBox.Show("Kusur Oranı: %" + kusurOrani.ToString("F2") + "\n" + "Makineyi kontrol ediniz."); 
                }

                else
                {
                    MessageBox.Show("Kusur Oranı: %" + kusurOrani.ToString("F2") + "\n" + "Üretime devam ediniz."); 
                }
            }
        }
    }
}
