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
    public partial class Form1 : Form
    {

        MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=pdks;Uid=root;Pwd=''");

        public Form1()
        {
            InitializeComponent();
        }


        private FilterInfoCollection webcam; //webcam isminde tanımladığımız değişken bilgisayara kaç kamera bağlıysa onları tutan bir dizi.

        private VideoCaptureDevice cam; //cam ise bizim kullanacağımız aygıt.
        private object pcbVideo;



        private void Form1_Load(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            string path = Application.StartupPath + "\\Sesler\\Kal.wav";
            player.SoundLocation = path;
            player.Play();

            textBox1.Focused.ToString();


            timer1.Start();
            timer2.Start();
            timer4.Start();

            webcam = new

          FilterInfoCollection(FilterCategory.VideoInputDevice); //webcam dizisine mevcut kameraları dolduruyoruz.

            foreach (FilterInfo item in webcam)

            {

                comboBox1.Items.Add(item.Name); //kameraları combobox a dolduruyoruz.

            }

            comboBox1.SelectedIndex = 0;
            cam = new

           VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString); //başlaya basıldığıdnda yukarda tanımladığımız cam değişkenine comboboxta seçilmş olan kamerayı atıyoruz.

            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);

            cam.Start(); //kamerayı başlatıyoruz.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button26.Text = DateTime.Now.ToLongTimeString();
            button27.Text = DateTime.Now.ToShortDateString();
      
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cikis = new DialogResult();
            cikis = MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
                cam.Stop(); // kamerayı durduruyoruz.
                Application.Exit();
            }
            if (cikis == DialogResult.No)
            {
                MessageBox.Show("The program is not closed.");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            cam.Stop(); // kamerayı durduruyoruz.
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cam.Stop(); // kamerayı durduruyoruz.
            Form6 frm6 = new Form6();
            frm6.Show();
            this.Hide();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    // Veritabanından Kartı Sorguluyooruz
                    baglan.Open();
                    string sorgu = "select * from personel_bilgileri where kartid='" + textBox1.Text + "'";
                    MySqlCommand cmd = new MySqlCommand(sorgu, baglan);
                    int Count = Convert.ToInt32(cmd.ExecuteScalar());
                    MySqlDataReader read = cmd.ExecuteReader();
                    read.Read();



                    if (Count != 0)
                    {
                        // Kartın son durumu alıp durumu='1' ise  durumu='2' yapacak.  durumu='2' ise  durumu='1' 

                        //string sorgu1 = "select durumu from personel_kart_okuma where kartid='" + textBox1.Text + "'  ORDER BY id DESC LIMIT 0, 1";
                        //MySqlCommand cmd1 = new MySqlCommand(sorgu1, baglan); 
                        //MySqlDataReader read1 = cmd1.ExecuteReader();
                        //read1.Read();

                        //textBox6.Text = read1["durumu"].ToString();

                        //if (textBox6.Text == "1")
                        //{
                        //textBox5.Text = "Çıkış Yaptı";
                        //}

                        //else if (textBox6.Text == "2")
                        //{
                        //textBox5.Text = "Giriş Yaptı";
                        //}



                        textBox5.Text = "1";

                        button21.Text = read["adi"].ToString();
                        button22.Text = read["soyadi"].ToString();
                        button23.Text = read["sicil_no"].ToString();
                        button24.Text = read["birimi"].ToString();
                        button25.Text = read["gorevi"].ToString();
                        button33.Text = read["gorev_yeri"].ToString();
                        textBox3.Text = "ProfilResimleri//" + read["resim"].ToString();


                        button32.Text = "*************";
                        pictureBox4.ImageLocation = textBox3.Text;

                        timer2.Start();
                        baglan.Close();


                        // Kamerayı Durduruyoruz
                        if (cam.IsRunning)

                        {
                            cam.Stop(); // kamerayı durduruyoruz. 
                        }


                        // kameradan verileri alıyoruz
                        Random rnd = new Random();
                        int sayi1 = rnd.Next(1, 100000);
                        label2.Text = Convert.ToString(sayi1);
                        pictureBox5.Image.Save(Application.StartupPath + "\\Resimler\\" + textBox1.Text + label2.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                        textBox4.Text = textBox1.Text + label2.Text + ".jpg";

                        // iki saat arasındaki farkı alıyoruz.
                        string girisZamani = "17:30"; 
                        string cikisZamani = DateTime.Now.ToLongTimeString();
                        TimeSpan girisCikisFarki = DateTime.Parse(cikisZamani).Subtract(DateTime.Parse(girisZamani));
                        string calismaSuresi = girisCikisFarki.ToString();
                        button4.Text = calismaSuresi.ToString();


                        // veri tabanına yazdırma işlemi yapıyoruz
                        baglan.Open();
                        string sorgu2 = "INSERT INTO personel_kart_okuma (kartid,durumu,kart_okuma_tarihi,kart_okuma_saati,resim,fazla_mesai) values('" + textBox1.Text + "','" + textBox5.Text + "','" + button27.Text + "','" + button26.Text + "','" + textBox4.Text + "','" + button4.Text + "')";
                        MySqlCommand upd = new MySqlCommand(sorgu2, baglan);
                        upd.ExecuteNonQuery();

                        SoundPlayer player = new SoundPlayer();
                        string path = Application.StartupPath + "\\Sesler\\GirisYapildi.wav";
                        player.SoundLocation = path;
                        player.Play();
                        baglan.Close();




                       
                        foreach (FilterInfo item in webcam)

                        {

                            comboBox1.Items.Add(item.Name); //kameraları combobox a dolduruyoruz.

                        }

                        comboBox1.SelectedIndex = 0;
                        cam = new

                       VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString); //başlaya basıldığıdnda yukarda tanımladığımız cam değişkenine comboboxta seçilmş olan kamerayı atıyoruz.

                        cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);

                        cam.Start(); //kamerayı başlatıyoruz.

                                     // kamerayı tekrar açıyoruz

                        // comboBox1.SelectedIndex = 1;
                        //   cam = new

                        // VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString); //başlaya basıldığıdnda yukarda tanımladığımız cam değişkenine comboboxta seçilmş olan kamerayı atıyoruz.

                        // cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);

                        // cam.Start(); //kamerayı başlatıyoruz.

                    }
                    else
                    {
                        button32.Text = "*************"; 

                        //timer2.Start();
                        baglan.Close();

                        timer5.Start();
                        //  MessageBox.Show("Kartınız tanımlı değil. Lütfen yöneticiyle iletişime geçiniz","Bilgilendirme");

                    }
                }
                catch (Exception)
                {

                    baglan.Close();

                }
                cam.Start(); //kamerayı başlatıyoruz.

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            SoundPlayer player = new SoundPlayer();
            string path = Application.StartupPath + "\\Sesler\\kartiniziokutunuz.wav";
            player.SoundLocation = path;
            player.Play(); 

            timer3.Stop();
            textBox1.Clear();
            textBox3.Clear();
            button21.Text = "";
            button22.Text = "";
            button32.Text = "";
            button23.Text = "";
            button33.Text = "";
            button24.Text = "";
            button25.Text = "";
            button4.Text = "";
            pictureBox4.ImageLocation = textBox1.Text;
            timer2.Stop();

            textBox3.Text = "resim-yok-png-7.png".ToString();


            pictureBox4.ImageLocation = textBox3.Text;

            textBox4.Text = "";
            textBox3.Clear(); 
            textBox1.Focus();


        }

        private void button28_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            cam = new

           VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString); //başlaya basıldığıdnda yukarda tanımladığımız cam değişkenine comboboxta seçilmş olan kamerayı atıyoruz.

            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);

            cam.Start(); //kamerayı başlatıyoruz.
        }

        private void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone(); //kısaca bu eventta kameradan alınan görüntüyü picturebox a atıyoruz.

            pictureBox5.Image = bmp;

        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (cam.IsRunning)

            {

                cam.Stop(); // kamerayı durduruyoruz.

            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int sayi1 = rnd.Next(1, 100000);
            label2.Text = Convert.ToString(sayi1);
            pictureBox3.Image.Save(Application.StartupPath + "\\Resimler\\" + textBox1.Text + label2.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            textBox1.Focus();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            DialogResult cikis = new DialogResult();
            cikis = MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
                cam.Stop(); // kamerayı durduruyoruz.
                Application.Exit();
            }
            if (cikis == DialogResult.No)
            {
                MessageBox.Show("The program is not closed.");

            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

            timer3.Stop();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {

            SoundPlayer player = new SoundPlayer();
            string path = Application.StartupPath + "\\Sesler\\tanimsizkart.wav";
            player.SoundLocation = path;
            player.Play();
            timer5.Stop();
            timer2.Start();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            cam.Stop();
            Anasayfa Anasayfa = new Anasayfa();
            Anasayfa.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            cam.Stop();
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
            /* Application.Exit();
             string myPath = @"Debug\PDKS.exe";
             System.Diagnostics.Process prc = new System.Diagnostics.Process();
             prc.StartInfo.FileName = myPath;
             prc.Start();*/
        }

        private void button24_Click(object sender, EventArgs e)
        {

        }
    }
}
