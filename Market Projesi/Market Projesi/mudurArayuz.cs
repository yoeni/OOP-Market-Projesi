using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Market_Projesi
{
    public partial class mudurArayuz : Form
    {
        private Calisan mevcutKisi;
        public mudurArayuz(Calisan poziyon)
        {
            InitializeComponent();
            mevcutKisi = poziyon;
        }
        Data verilerim = new Data();
        Calisan tempCalisan;
        bool panel1State = false;

        private void mudurArayuz_Load(object sender, EventArgs e)
        {
            verilerim.Load();
            datagridDoldur();
            if (mevcutKisi.Mevki==sistem.pozisyonlar.yonetici)
            {
                tedarikciSil.Enabled = true;
            }
            else
            {
                tedarikciSil.Enabled = false;
            }
        }

        void datagridDoldur()
        {
            int i = 0;
            marketSubeDataGrid.Rows.Clear();
            calisanlarDataGrid.Rows.Clear();
            foreach (MarketSube item in verilerim.marketler)
            {
                marketSubeDataGrid.Rows.Add((i + 1).ToString(), item.SubeAdi, item.marketBilgileriGetir()[0], item.marketBilgileriGetir()[2], item.marketBilgileriGetir()[1]);
                i++;
            }
            calisanSilButon.Enabled = false;
            calisanDuzenleButon.Enabled = false;
            pozisyonDegisikligi.Enabled = false;
            subeDegisikligi.Enabled = false;
            maasDegisikligi.Text = "-";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void marketSubeDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            calisanlarDataGrid.Rows.Clear();
            for (int i = 0; i < verilerim.marketler.Count(); i++)
            {
                if ((i+1)==int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()))
                {
                    int k = 0;
                    foreach (Calisan item in (verilerim.marketler.ToArray()[i]).CalisanlarListe())
                    {
                        calisanlarDataGrid.Rows.Add(k + 1, item.CalisanBilgileriGoster()[0],
                            item.CalisanBilgileriGoster()[1],
                            item.CalisanBilgileriGoster()[2],
                            item.CalisanBilgileriGoster()[3],
                            item.CalisanBilgileriGoster()[4],
                            item.CalisanBilgileriGoster()[5] + " ₺",
                            item.CalisanBilgileriGoster()[6],
                            item.CalisanBilgileriGoster()[7],
                            item.CalisanBilgileriGoster()[8]
                            );
                        k++;
                    }
                }
            }
            calisanekleButton.Enabled = true;
            button8.Enabled = true;
        }
        void panelOpen(bool state)
        {
            panel1State = state;
            if (state)
            {
                button8.Enabled = false;
                button2.Enabled = false;
                marketSubeDataGrid.Enabled = false;
                calisanlarDataGrid.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                button8.Enabled = false;
                button2.Enabled = true;
                marketSubeDataGrid.Enabled = true;
                calisanlarDataGrid.Enabled = true;
                button1.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panelOpen(true);
            panel1.Visible = true;
            panel1.Left = 275;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelOpen(false);
            panel1.Visible = false;
            panel1.Left = 1000;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (subeNameTextBox.Text!=""&&adresTextBox.Text!=""&&mudurDrop.SelectedItem!=null)
            {
                if ((string)mudurDrop.SelectedItem == tempCalisan.IsimSoyisim)
                {

                    MarketSube tempMarket = new MarketSube(adresTextBox.Text, subeNameTextBox.Text);
                    tempMarket.MudurAta(tempCalisan);
                    verilerim.marketler.Add(tempMarket);
                    datagridDoldur();
                    subeNameTextBox.Text = "";
                    adresTextBox.Text = "";
                }
            }
        }
        void CalisanEklePanelKontrol(bool marketSource = false,bool state=true)
        {
            if (state)
            {
                calisanEklePanel.Visible = true;
                calisanEklePanel.Left = 175;
                if (marketSource)
                {
                    button5.Enabled = false;
                    subeText.Text = subeNameTextBox.Text;
                    mevkiText.SelectedItem = "Müdür";
                    mevkiText.Enabled = false;
                    panel1.Visible = false;
                    panel1.Left = 1000;
                }
                else
                {
                    mevkiText.SelectedItem = "Kasiyer";
                    subeText.Text = marketSubeDataGrid.SelectedRows[0].Cells[1].Value.ToString();
                    mevkiText.Enabled = true;
                }
            }
            else
            {
                isimSoyisimText.Text = "";
                mevkiText.SelectedIndex = 0;
                maasText.Text = "";
                adresText.Text = "";
                calisanEklePanelUserTb.Text = "";
                calisanEklePanelSifreTB.Text = "";
                calisanEklePanel.Visible = false;
                calisanEklePanel.Left = 1000;
                if (marketSource)
                {
                    panel1.Visible = true;
                    button5.Enabled = true;
                    panel1.Left = 275;
                }
                else
                {
                    button8.Enabled = true;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (subeNameTextBox.Text!="")
            {
                CalisanEklePanelKontrol(true,true);
            }
            else
            {
                MessageBox.Show("Şube Adı Giriniz!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (isimSoyisimText.Text!=""&&adresText.Text!=""&&maasText.Text!=""&&subeText.Text!=""&&mevkiText.Text!="")
            {
                try
                {
                    tempCalisan = new Calisan(isimSoyisimText.Text, Convert.ToInt32(maasText.Text), adresText.Text, sistem.pozisyonlar.mudur);
                    tempCalisan.GirisBilgileriBelirle(calisanEklePanelUserTb.Text, calisanEklePanelSifreTB.Text);
                    if (panel1State)
                    {
                        mudurDrop.Items.Add(isimSoyisimText.Text);
                        mudurDrop.SelectedItem = isimSoyisimText.Text;
                    }
                    else
                    {
                        switch (mevkiText.SelectedItem)
                        {
                            case "Kasiyer": { tempCalisan.Mevki = sistem.pozisyonlar.kasiyer; } break;
                            case "Müdür Yardımcısı": { tempCalisan.Mevki = sistem.pozisyonlar.mudurYardimcisi; } break;
                            case "Müdür": { tempCalisan.Mevki = sistem.pozisyonlar.mudur; } break;
                            default: { tempCalisan.Mevki = sistem.pozisyonlar.kasiyer; } break;
                        }
                        tempCalisan.MarketSubeAta(verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1]);
                        datagridDoldur();
                    }
                    CalisanEklePanelKontrol(panel1State, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Hatalı Giriş!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CalisanEklePanelKontrol(panel1State, false);
            subeText.Text = "";
            mevkiText.SelectedItem = "Kasiyer";
            subeText.Enabled = true;
            mevkiText.Enabled = true;
        }

        private void marketSubeDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            CalisanEklePanelKontrol(false,true);
            button8.Enabled = false;
        }
        public void urunlerGoster(MarketSube marketim)
        {
            groupBox1.Enabled = false;
            marketUrunlerDataGrid.Rows.Clear();
            tedarikcilerDown.Items.Clear();
            urunDuzenleAd.Text = "";
            urunDuzenleFiyat.Text = "";
            urunDuzenlemeOran.Text = "";
            urunDuzenlemeSonIndırım.Text = "";
            urunDuzenlemeStok.Text = "";
            urunDuzenlemeTedarikci.Text = ""; 
            urunEkleFiyat.Text = "";
            urunEkleAd.Text = "";
            urunEkleBarkod.Text = "";
            urunEkleStok.Text = "";
            int i = 0;
            foreach (urun urunum in marketim.UrunleriListele())
            {
                marketUrunlerDataGrid.Rows.Add((urunum.tedarikciAl().TedarikGrubuOgren() + " - " + urunum.tedarikciAl().TedarikciAdi),urunum.urunBarkod, (i + 1), urunum.UrunAd, urunum.UrunFiyat.ToString() + " ₺", urunum.UrunStok.ToString() + " Adet", urunum.sonIndirim().ToString(), "% " + urunum.sonIndirimOran().ToString());
                i++;
            }
            marketUrunlerDataGrid.Sort(marketUrunlerDataGrid.Columns[0], System.ComponentModel.ListSortDirection.Ascending);

            if (verilerim.tedarikciler.Count!=0)
            {
                foreach (Tedarikci item in verilerim.tedarikciler)
                {
                    tedarikcilerDown.Items.Add(item.TedarikGrubuOgren() + " " + item.TedarikciAdi);
                }
                tedarikcilerDown.SelectedIndex = 0;
            }
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            marketIncelemePanel.Visible = true;
            marketIncelemePanel.Left = 12;
            marketUrunlerDataGrid.Rows.Clear();
            urunlerGoster(verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1]);
            tedarikcilerDown.SelectedIndex = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            marketIncelemePanel.Visible = false;
        }

        private void marketUrunlerDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            urunDuzenlemeTedarikci.Text = marketUrunlerDataGrid.SelectedRows[0].Cells[0].Value.ToString();
            urunDuzenleAd.Text = marketUrunlerDataGrid.SelectedRows[0].Cells[3].Value.ToString();
            urunDuzenleFiyat.Text = marketUrunlerDataGrid.SelectedRows[0].Cells[4].Value.ToString();
            urunDuzenlemeOran.Text = marketUrunlerDataGrid.SelectedRows[0].Cells[7].Value.ToString();
            urunDuzenlemeSonIndırım.Text = marketUrunlerDataGrid.SelectedRows[0].Cells[6].Value.ToString();
            urunDuzenlemeStok.Text = marketUrunlerDataGrid.SelectedRows[0].Cells[5].Value.ToString();
            float fiyat = (float)Convert.ToDouble(urunDuzenleFiyat.Text.Replace(" ₺", ""));
            label24.Text = "" + (fiyat - (fiyat * (float)urunDuzenlemeOranDown.Value / 100)) + "₺";
            if (mevcutKisi.Mevki!=sistem.pozisyonlar.yonetici)
            {
                if (!urunDuzenlemeTedarikci.Text.Contains("Kampanya"))
                {
                    groupBox1.Enabled = false;
                }
                else
                {
                    groupBox1.Enabled = true;
                }
                urunDuzenleSil.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                urunDuzenleSil.Enabled = true;
            }
        }

        private void urunDuzenlemeDuzenle_Click(object sender, EventArgs e)
        {
            MarketSube item = verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1];
            urun tempUrun = item.UrunleriListele()[int.Parse(marketUrunlerDataGrid.SelectedRows[0].Cells[2].Value.ToString()) - 1];
            if (urunDuzenleAd.Text!=""&&urunDuzenlemeStok.Text!="")
            {
                verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].urunGuncelle(tempUrun.urunBarkod, urunDuzenleAd.Text,Convert.ToInt32(urunDuzenlemeStok.Text.Replace(" Adet","")));
            }
            urunlerGoster(verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1]);
        }

        private void urunDuzenlemeIndUyg_Click(object sender, EventArgs e)
        {
            MarketSube item = verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1];
            urun tempUrun = item.UrunleriListele()[int.Parse(marketUrunlerDataGrid.SelectedRows[0].Cells[2].Value.ToString()) - 1];
            if (urunDuzenleAd.Text != "" && urunDuzenlemeStok.Text != "")
            {
                verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].urunGuncelle(tempUrun.urunBarkod,(int)urunDuzenlemeOranDown.Value);
            }
            urunlerGoster(verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1]);
        }

        private void urunDuzenlemeOranDown_ValueChanged(object sender, EventArgs e)
        {
            float fiyat = (float)Convert.ToDouble(urunDuzenleFiyat.Text.Replace(" ₺",""));
            label24.Text = ""+(fiyat - (fiyat * (float)urunDuzenlemeOranDown.Value / 100))+"₺";
        }

        private void tedarikciOlustur_Click(object sender, EventArgs e)
        {
            if (tedarikciAdText.Text != "" && tedarikciAdresText.Text != "")
            {
                sistem.tedarikGrubu tedarikGrubum = sistem.tedarikGrubu.gida;
                switch (tedarikcilerGrubuDown.SelectedItem)
                {
                    case "Gıda": { tedarikGrubum = sistem.tedarikGrubu.gida; break; }
                    case "Temizlik": { tedarikGrubum = sistem.tedarikGrubu.temizlik; break; }
                    case "Kampanya": { tedarikGrubum = sistem.tedarikGrubu.kampanya; break; }
                }
                Tedarikci tedarikci = new Tedarikci(tedarikciAdText.Text, tedarikciAdresText.Text, tedarikGrubum);
                verilerim.tedarikciler.Add(tedarikci);
                tedarikcilerDown.Items.Add(tedarikci.TedarikGrubuOgren() + " " + tedarikci.TedarikciAdi);
                tedarikciPanel.Visible = false;
                tedarikciPanel.Left = 1000;
            }
            else
            {
                MessageBox.Show("Lütfen Girişleri Kontrol Ediniz!");
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Enabled = false;
            tedarikciPanel.Visible = true;
            tedarikciPanel.Left = 263;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button13.Enabled = true;
            tedarikciPanel.Visible = false;
            tedarikciPanel.Left = 1000;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (urunEkleFiyat.Text!=""&&urunEkleBarkod.Text!=""&&urunEkleStok.Text!=""&&urunEkleFiyat.Text!="")
            {
                try
                {
                    urun urunum = new urun(urunEkleBarkod.Text, urunEkleAd.Text, (float)Convert.ToDouble(urunEkleFiyat.Text.Replace(",",".")), int.Parse(urunEkleStok.Text), verilerim.tedarikciler[tedarikcilerDown.SelectedIndex]);
                    verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].UrunEkle(urunum);
                    urunlerGoster(verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1]);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Girişleri Kontrol Ediniz!");
            }
        }

        private void urunDuzenleSil_Click(object sender, EventArgs e)
        {
            MarketSube item = verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1];
            urun tempUrun = item.UrunleriListele()[int.Parse(marketUrunlerDataGrid.SelectedRows[0].Cells[2].Value.ToString()) - 1];
            verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].urunSil(tempUrun.urunBarkod);
            urunlerGoster(verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1]);
        }

        private void tedarikciSil_Click(object sender, EventArgs e)
        {
            verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].TedarikciSil(verilerim.tedarikciler[tedarikcilerDown.SelectedIndex]);
            verilerim.tedarikciler.Remove(verilerim.tedarikciler[tedarikcilerDown.SelectedIndex]);
            tedarikcilerDown.Items.Remove(tedarikcilerDown.SelectedItem);
            urunlerGoster(verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1]);
        }


        private void calisanlarDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string pozisyon = calisanlarDataGrid.SelectedRows[0].Cells[2].Value.ToString();
            pozisyonDegisikligi.SelectedItem = pozisyon;
            subeDegisikligi.Items.Clear();
            foreach (MarketSube item in verilerim.marketler)
            {
                subeDegisikligi.Items.Add(item.SubeAdi);
            }
            subeDegisikligi.SelectedItem = verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].SubeAdi;
            maasDegisikligi.Text = calisanlarDataGrid.SelectedRows[0].Cells[6].Value.ToString();
            calisanSilButon.Enabled = true;
            calisanDuzenleButon.Enabled = true;
            pozisyonDegisikligi.Enabled = true;
            subeDegisikligi.Enabled = true;
        }

        private void calisanDuzenleButon_Click(object sender, EventArgs e)
        {

            int maas = int.Parse(maasDegisikligi.Text.Replace(" ₺", ""));
            if (maas>=int.Parse(calisanlarDataGrid.SelectedRows[0].Cells[6].Value.ToString().Replace(" ₺","")))
            {
                sistem.pozisyonlar tempMevkim=sistem.pozisyonlar.kasiyer;
                switch (pozisyonDegisikligi.SelectedIndex)
                {
                    case 0:tempMevkim = sistem.pozisyonlar.kasiyer;break;
                    case 1: tempMevkim = sistem.pozisyonlar.mudurYardimcisi; break;
                    case 2: tempMevkim = sistem.pozisyonlar.mudur; break;
                }
                verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].CalisanlarListe()[calisanlarDataGrid.CurrentCell.RowIndex].MevkiDegisikligi(tempMevkim);
                verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].CalisanlarListe()[calisanlarDataGrid.CurrentCell.RowIndex].MaasDegisikligi(maas);
                verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].CalisanlarListe()[calisanlarDataGrid.CurrentCell.RowIndex].SubeDegisikligi(verilerim.marketler[subeDegisikligi.SelectedIndex]);
                datagridDoldur();
            }
            
        }

        private void calisanSilButon_Click(object sender, EventArgs e)
        {
            verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].CalisanSil(verilerim.marketler[int.Parse(marketSubeDataGrid.SelectedRows[0].Cells[0].Value.ToString()) - 1].CalisanlarListe()[calisanlarDataGrid.CurrentCell.RowIndex]);
            datagridDoldur();
        }
    }
}
