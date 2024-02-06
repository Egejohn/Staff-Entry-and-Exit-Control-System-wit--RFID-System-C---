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
    public partial class Form2 : Form
    {
        MySqlDataAdapter da_malzeme;
        DataTable dt_malzeme;
        MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=pdks;Uid=root;Pwd=''");


        public Form2()
        {
            InitializeComponent();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button26.Text = DateTime.Now.ToLongTimeString();
            button27.Text = DateTime.Now.ToShortDateString(); 
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            timer1.Start(); 
        }

        private void button8_Click(object sender, EventArgs e)
        {
             
        }

        private void timer2_Tick(object sender, EventArgs e)
        { 
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            button26.Text = DateTime.Now.ToLongTimeString();
            button27.Text = DateTime.Now.ToShortDateString();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {

                baglan.Open();
                string sorgu = "SELECT personel_bilgileri.`adi`, personel_bilgileri.`soyadi`, personel_bilgileri.`kartid`, personel_bilgileri.`birimi`,personel_bilgileri.`gorevi`, personel_bilgileri.`gorev_yeri`, personel_kart_okuma.`durumu`, personel_kart_okuma.`kart_okuma_tarihi`,personel_kart_okuma.`kart_okuma_saati`, personel_kart_okuma.`fazla_mesai` FROM `personel_kart_okuma`INNER JOIN `personel_bilgileri` ON personel_kart_okuma.`kartid`= personel_bilgileri.`kartid` WHERE personel_kart_okuma.`kart_okuma_tarihi` BETWEEN '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "' ORDER BY personel_kart_okuma.`id` ASC";
              
                MySqlDataAdapter da = new MySqlDataAdapter(sorgu, baglan);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                baglan.Close();
            }
            catch
            {

                MessageBox.Show("Personnel Information List Error.");
            }


            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            PersonelListesi prslst = new PersonelListesi();
            prslst.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OkutulanKartListesi okl = new OkutulanKartListesi();
            okl.Show();
            this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }
    }
}
