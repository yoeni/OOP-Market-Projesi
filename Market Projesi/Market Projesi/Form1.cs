using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Market_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Data verilerim = new Data();
        private void Form1_Load(object sender, EventArgs e)
        {
            verilerim.Load();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool giris = false;
            if (usernameTextBox.Text!=""&&passwordTextBox.Text!="")
            {
                if (usernameTextBox.Text == verilerim.yonetici.GirisBilgiler()[0] && passwordTextBox.Text == verilerim.yonetici.GirisBilgiler()[1])
                {
                    mudurArayuz mudurum = new mudurArayuz(verilerim.yonetici);
                    mudurum.Show();
                    this.Hide();
                    giris = true;
                }
                else
                {
                    foreach (MarketSube marketim in verilerim.marketler)
                    {
                        foreach (Calisan calisanim in marketim.CalisanlarListe())
                        {
                            if (usernameTextBox.Text == calisanim.GirisBilgiler()[0] && passwordTextBox.Text == calisanim.GirisBilgiler()[1])
                            {
                                if (calisanim.Mevki == sistem.pozisyonlar.mudur)
                                {
                                    mudurArayuz mudurum = new mudurArayuz(calisanim);
                                    mudurum.Show();
                                    this.Hide();
                                }
                                else 
                                {
                                    MessageBox.Show(String.Join("\n - " , calisanim.CalisanBilgileriGoster()),"OLUŞTURULMAYAN ARAYÜZ!");
                                }
                                giris = true;
                                break;
                            }
                        }
                    }
                }
                if (!giris)
                {
                    MessageBox.Show("Yanlış kullanıcı adı veya parola girişi!");
                }
            }
            else
            {
                MessageBox.Show("Eksik veri girişi!");
            }
        }
    }
}
