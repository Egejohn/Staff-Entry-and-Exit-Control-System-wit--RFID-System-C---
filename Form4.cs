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
    public partial class Form4 : Form
    {
        public static string kullaniciadi;
        MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=pdks;Uid=root;Pwd=''");

        public Form4()
        {
            InitializeComponent();
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
            cikis = MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
                Application.Exit();
            }
            if (cikis == DialogResult.No)
            {
                MessageBox.Show("The program is not closed.");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            button26.Text = DateTime.Now.ToLongTimeString();
            button27.Text = DateTime.Now.ToShortDateString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            timer1.Start();


            SoundPlayer player = new SoundPlayer();
            string path = Application.StartupPath + "\\Sesler\\yönetici.wav";
            player.SoundLocation = path;
            player.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void button1_Click_2(object sender, EventArgs e)
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
                    MessageBox.Show("Username or password is wrong", "Warning...");
                    baglan.Close();

                }
            }
            catch
            {


                baglan.Close();

            }
        }
    }
}
