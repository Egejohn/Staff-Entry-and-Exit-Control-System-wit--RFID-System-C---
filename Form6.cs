using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AForge.Video;  //Referansları ekliyoruz
using AForge.Video.DirectShow; //Referansları ekliyoruz
using System.Drawing.Imaging;
using System.Media;

namespace DarkDemo
{
    public partial class Form6 : Form
    {
        public static string kullaniciadi;
        MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=pdks;Uid=root;Pwd=''");

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

            SoundPlayer player = new SoundPlayer();
            string path = Application.StartupPath + "\\Sesler\\yönetici.wav";
            player.SoundLocation = path;
            player.Play();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult cikis = new DialogResult();
            cikis = MessageBox.Show("Çıkmak istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
                Application.Exit();
            }
            if (cikis == DialogResult.No)
            {
                MessageBox.Show("Program kapatılmadı.");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            try
            {
                baglan.Open();
                string sorgu = "select * from kullanicilar where kullaniciadi='" + textBox3.Text + "' and sifre='" + textBox1.Text + "' and goster='1'";
                MySqlCommand komut = new MySqlCommand(sorgu, baglan);
                int count = Convert.ToInt32(komut.ExecuteScalar());
                if (count > 0)
                {
                    Form3 frm3 = new Form3();
                    frm3.Show();
                    this.Hide();
                    baglan.Close();
                }
                else
                {
                    SoundPlayer player = new SoundPlayer();
                    string path = Application.StartupPath + "\\Sesler\\hata.wav";
                    player.SoundLocation = path;
                    player.Play();
                    MessageBox.Show("Kullanıcı Adı Veya Şifre Hatalı", "Uyarı...");
                    baglan.Close();

                }
            }
            catch
            {


                baglan.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            button26.Text = DateTime.Now.ToLongTimeString();
            button27.Text = DateTime.Now.ToShortDateString();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }
    }
}
