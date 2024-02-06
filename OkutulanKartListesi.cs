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
    public partial class OkutulanKartListesi : Form
    {
        MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=pdks;Uid=root;Pwd=''");

        public OkutulanKartListesi()
        {
            InitializeComponent();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            button26.Text = DateTime.Now.ToLongTimeString();
            button27.Text = DateTime.Now.ToShortDateString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PersonelListesi pl = new PersonelListesi();
            pl.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
       
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    // Veritabanından Kartı Sorguluyooruz
                    baglan.Open();
                    string sorgu = "select * from personel_bilgileri where kartid='" + textBox3.Text + "'";
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


                        textBox3.Text = read["kartid"].ToString();
                        button23.Text = read["kartid"].ToString();
                        textBox1.Text = read["adi"].ToString();
                        textBox5.Text = read["soyadi"].ToString();
                        textBox2.Text = read["sicil_no"].ToString();
                        button18.Text = read["maas"].ToString();
                        comboBox3.Text = read["birimi"].ToString();
                        comboBox2.Text = read["gorevi"].ToString();
                        comboBox1.Text = read["gorev_yeri"].ToString();
                        textBox4.Text = "ProfilResimleri//" + read["resim"].ToString();



                        pictureBox1.ImageLocation = textBox4.Text;
                         

                        baglan.Close();

                        try
                        {
                             
                            string sorgu_1 = "SELECT * FROM `personel_kart_okuma`  where kartid='" + textBox3.Text + "' and goster='1'";

                            MySqlDataAdapter da = new MySqlDataAdapter(sorgu_1, baglan);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;

                            baglan.Close();
                        }
                        catch
                        {

                            MessageBox.Show("Personnel Information List Error.");
                        }

                        baglan.Close();

                        groupBox1.Visible = true;
                      
                    }
                    else
                    {


                        baglan.Close();



                    }
                }
                catch (Exception)
                {

                    baglan.Close();

                }

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.Focus();
            textBox3.Text = "";
            textBox1.Text = "";
            textBox5.Text = "";
            textBox2.Text = "";
            comboBox3.Text = "";
            comboBox2.Text = "";
            comboBox1.Text = "";
            textBox4.Text = "";
            groupBox1.Visible = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            { 

                baglan.Open();
                string sorgu = "SELECT SUM(fazla_mesai) AS FazlaMesai FROM `personel_kart_okuma` WHERE kartid = '" + button23.Text + "' and  `kart_okuma_tarihi` BETWEEN  '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "' ";
                MySqlCommand cmd = new MySqlCommand(sorgu, baglan);
                int Count = Convert.ToInt32(cmd.ExecuteScalar());
                MySqlDataReader read = cmd.ExecuteReader();
                read.Read();

                panel3.Visible = true;

                button11.Text = read["FazlaMesai"].ToString(); 



                Double sayi1 = Convert.ToDouble(button18.Text);
                Double sayi2 = Convert.ToDouble(textBox6.Text);
                button24.Text = (sayi1 / sayi2).ToString();

                Double sayi3 = Convert.ToDouble(button24.Text);
                Double sayi4 = Convert.ToDouble(textBox7.Text);
                button25.Text = (sayi3 / sayi4).ToString();

                Double sayi5 = Convert.ToDouble(button25.Text);
                Double sayi6 = Convert.ToDouble(textBox8.Text);
                button14.Text = (sayi5 / sayi6).ToString();

                
                Double sayi7 = Convert.ToDouble(button25.Text);
                Double sayi8 = Convert.ToDouble(button11.Text);
                button28.Text = (sayi7 * sayi8).ToString();
               


                Double sayi9 = Convert.ToDouble(button28.Text);
                Double sayi10 = Convert.ToDouble(button18.Text);
                button19.Text = (sayi9 + sayi10).ToString();

                /*
                int sayi1, sayi2, sayi3, sayi4, sayi5;
                sayi1 = Convert.ToInt32(button18.Text);
                sayi2 = Convert.ToInt32(textBox6.Text);
                sayi4 = Convert.ToInt32(textBox4.Text);
                sayi5 = Convert.ToInt32(button11.Text);
                sayi3 = Convert.ToInt32(((sayi1 / sayi2) / sayi4) * sayi5); // gün olarak
                

                button14.Text = sayi3.ToString();
                */


                //  com.CommandText = "SELECT * FROM Tartim2  WHERE Tarih1 BETWEEN  '" + dateTimePicker1.Value + "' AND '" + dateTimePicker2.Value + "' "; // Veri çektiğim tablo
                baglan.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("ERROR CODE : 7 Couldn't Get Data by Date.", "!:::...ERROR...:::!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglan.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void OkutulanKartListesi_Load(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OkutulanKartListesi kol = new OkutulanKartListesi();
            kol.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
    }
}
