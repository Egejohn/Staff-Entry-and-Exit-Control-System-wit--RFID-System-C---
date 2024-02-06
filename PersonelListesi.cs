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
    public partial class PersonelListesi : Form
    {
        MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=pdks;Uid=root;Pwd=''");


       
        public PersonelListesi()
        {
            InitializeComponent();
        }


 private FilterInfoCollection webcam; //webcam isminde tanımladığımız değişken bilgisayara kaç kamera bağlıysa onları tutan bir dizi.

        private VideoCaptureDevice cam; //cam ise bizim kullanacağımız aygıt.
        private object pcbVideo;

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void PersonelListesi_Load(object sender, EventArgs e)
        {
            timer1.Start();
            button6.Visible = true;
            button11.Visible = false;
            button14.Visible = false;
            button3.Visible = false;
            timer2.Start();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            try
            {

                // Kamerayı Durduruyoruz

                baglan.Open();
                string sorgu2 = "INSERT INTO personel_bilgileri (kartid,adi,soyadi,sicil_no,birimi,gorevi,gorev_yeri,maas,resim) values('" + textBox3.Text + "','" + textBox1.Text + "','" + textBox5.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + comboBox2.Text + "','" + comboBox1.Text + "','" + textBox4.Text + "', 'bos.jpg')";
                MySqlCommand upd = new MySqlCommand(sorgu2, baglan);
                upd.ExecuteNonQuery();

                SoundPlayer player = new SoundPlayer();
                string path = Application.StartupPath + "\\Sesler\\GirisYapildi.wav";
                player.SoundLocation = path;
                player.Play();
                baglan.Close();
                MessageBox.Show("Personnel Data Created");
                timer1.Start();
            }
            catch (Exception)
            {

                baglan.Close();

            } 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {

                baglan.Open();
                string sorgu = "SELECT * FROM `personel_bilgileri` WHERE goster='1' order by `adi` ASC";

                MySqlDataAdapter da = new MySqlDataAdapter(sorgu, baglan);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                baglan.Close();

                button6.Visible = true;
                button11.Visible = false;
                button14.Visible = false;
                button3.Visible = false;
            }
            catch
            {

                MessageBox.Show("Personel Bilgileri Liste Hatası.");
            }
            timer1.Stop();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button6.Visible = true;
            button11.Visible = false;
            button14.Visible = true;
            button3.Visible = true;

            pictureBox4.Visible = true;
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();  
            comboBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();

            //textBox6.Text = "ProfilResimleri//" + read["resim"].ToString();
            pictureBox4.ImageLocation = "ProfilResimleri//" + dataGridView1.CurrentRow.Cells[8].Value.ToString();
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.Focus();
            textBox3.Text = "";
            textBox1.Text = "";
            textBox5.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            comboBox3.Text = "";
            comboBox2.Text = "";
            comboBox1.Text = "";


            button6.Visible = true;
            button11.Visible = true;
            button14.Visible = false;
            button3.Visible = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {

            OkutulanKartListesi okl = new OkutulanKartListesi();
            okl.Show();
            this.Hide();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            button26.Text = DateTime.Now.ToLongTimeString();
            button27.Text = DateTime.Now.ToShortDateString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PersonelListesi prslst = new PersonelListesi();
            prslst.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            

            baglan.Open();
            string sorgu2 = "UPDATE personel_bilgileri SET kartid='" + textBox3.Text + "',adi='" + textBox1.Text + "',soyadi='" + textBox5.Text + "',sicil_no='" + textBox2.Text + "',birimi='" + comboBox3.Text + "',gorevi='" + comboBox2.Text + "',gorev_yeri='" + comboBox1.Text + "',maas='" + textBox4.Text + "' WHERE kartid='" + textBox3.Text + "'";
            MySqlCommand upd = new MySqlCommand(sorgu2, baglan);
            upd.ExecuteNonQuery();

            SoundPlayer player = new SoundPlayer();
            string path = Application.StartupPath + "\\Sesler\\GirisYapildi.wav";
            player.SoundLocation = path;
            player.Play();
            baglan.Close();
            MessageBox.Show("Personnel Data Updated.");
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Kamerayı Durduruyoruz

            baglan.Open();
            string sorgu2 = "UPDATE personel_bilgileri SET goster='2' WHERE kartid='" + textBox3.Text + "'";
            MySqlCommand upd = new MySqlCommand(sorgu2, baglan);
            upd.ExecuteNonQuery();

            SoundPlayer player = new SoundPlayer();
            string path = Application.StartupPath + "\\Sesler\\GirisYapildi.wav";
            player.SoundLocation = path;
            player.Play();
            baglan.Close();
            MessageBox.Show("Personnel Data Deleted.");
            timer1.Start();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox1.Visible = true;
            webcam = new

           FilterInfoCollection(FilterCategory.VideoInputDevice); //webcam dizisine mevcut kameraları dolduruyoruz.

            foreach (FilterInfo item in webcam)

            {

                comboBox5.Items.Add(item.Name); //kameraları combobox a dolduruyoruz.

            }

            comboBox5.SelectedIndex = 0;
            cam = new

           VideoCaptureDevice(webcam[comboBox5.SelectedIndex].MonikerString); //başlaya basıldığıdnda yukarda tanımladığımız cam değişkenine comboboxta seçilmş olan kamerayı atıyoruz.

            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);

            cam.Start(); //kamerayı başlatıyoruz.
        }

        private void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone(); //kısaca bu eventta kameradan alınan görüntüyü picturebox a atıyoruz.

            pictureBox1.Image = bmp;

        }

        private void button18_Click(object sender, EventArgs e)
        {

            // Kamerayı Durduruyoruz
            if (cam.IsRunning)

            {
                cam.Stop(); // kamerayı durduruyoruz. 
            }


            // kameradan verileri alıyoruz
            Random rnd = new Random();
            int sayi1 = rnd.Next(1, 100000);
            textBox6.Text = Convert.ToString(sayi1);
            pictureBox1.Image.Save(Application.StartupPath + "\\ProfilResimleri\\" + textBox3.Text + textBox6.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            textBox7.Text = textBox3.Text + textBox6.Text + ".jpg";



            baglan.Open();
            string sorgu2 = "UPDATE personel_bilgileri SET resim = '" + textBox7.Text + "' WHERE kartid='" + textBox3.Text + "'";
            MySqlCommand upd = new MySqlCommand(sorgu2, baglan);
            upd.ExecuteNonQuery();

            SoundPlayer player = new SoundPlayer();
            string path = Application.StartupPath + "\\Sesler\\GirisYapildi.wav";
            player.SoundLocation = path;
            player.Play();
            baglan.Close();
            MessageBox.Show("Personnel Picture Updated.");
            timer1.Start();

           

        }

        private void button19_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }
    }
}
