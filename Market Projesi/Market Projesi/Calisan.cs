using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Projesi
{
    public class Calisan
    {
        public string IsimSoyisim { get; private set; }
        private string KullaniciAdi { get; set; }
        private string KullaniciSifre { get; set; }
        private bool HaftalikIzinDurumu { get; set; }
        private int YillikIzinGun { get; set; }
        private string Adres { get; set; }
        public sistem.pozisyonlar Mevki { get; set; }
        private DateTime IseGiris { get; set; }
        private int Maas { get; set; }
        public MarketSube Sube { get; set; }
        public Calisan(string isimSoyisim,int maas, string adres,sistem.pozisyonlar mevki)
        {
            this.IsimSoyisim = isimSoyisim;
            this.Adres = adres;
            this.Mevki = mevki;
            this.Maas = maas;
            this.YillikIzinGun = 14;
            this.HaftalikIzinDurumu = false;
            this.IseGiris = DateTime.Now;
        }
        public void MarketSubeAta(MarketSube sube)
        {
            if (Mevki!=sistem.pozisyonlar.yonetici)
            {
                this.Sube = sube;

                if (Mevki==sistem.pozisyonlar.mudur)
                {
                    sube.MudurAta(this);

                }
                else
                {
                    sube.CalisanEkle(this);
                }
            }

        }
        public void MaasDegisikligi(int maas)
        {
            this.Maas = maas;
        }
        public void MevkiDegisikligi(sistem.pozisyonlar mevkim)
        {
            if (this.Mevki==sistem.pozisyonlar.mudur)
            {
                Sube.MudurSil();
            }
            this.Mevki = mevkim;
        }
        public void SubeDegisikligi(MarketSube sube)
        {
            if (this.Sube!=null)
            {
                if (Mevki==sistem.pozisyonlar.mudur)
                {
                    sube.CalisanSil(sube.Mudur);
                    this.Sube.MudurSil();
                }
                this.Sube.CalisanSil(this);
                this.MarketSubeAta(sube);
            }
        }
        public void GirisBilgileriBelirle(string kullanciAdi,string kullaniciSifre)
        {
            this.KullaniciAdi = kullanciAdi;
            this.KullaniciSifre = kullaniciSifre;
        }
        public string[] CalisanBilgileriGoster()
        {
            List<string> bilgi = new List<string>();
            bilgi.Add(IsimSoyisim);
            switch (Mevki)
            {
                case sistem.pozisyonlar.mudur:bilgi.Add("Müdür");
                    break;
                case sistem.pozisyonlar.mudurYardimcisi:bilgi.Add("Müdür Yardımcısı");
                    break;
                case sistem.pozisyonlar.kasiyer:bilgi.Add("Kasiyer");
                    break;
                default:
                    bilgi.Add("Kasiyer");
                    break;
            }
            bilgi.Add(Sube.SubeAdi);
            bilgi.Add(Adres);
            bilgi.Add(IseGiris.ToString("dd/MM/yyyy"));
            bilgi.Add(Maas.ToString());
            bilgi.Add(Convert.ToInt32((CalismaSuresi().Day/365.24219)) + " Yıl " + CalismaSuresi().Day + " Gün");
            bilgi.Add(YillikIzinKalanGünHesapla().ToString());
            bilgi.Add(HaftalikIzinDurumu == false ? "Kullanılmadı" : "Kullanıldı");
            return bilgi.ToArray();
        }
        public int YillikIzinKalanGünHesapla()
        {
            return YillikIzinGun;
        }
        public DateTime CalismaSuresi()
        {
            return (new DateTime() +(DateTime.Now-IseGiris));
        }
        public string[] GirisBilgiler()
        {
            List<string> returnBilgi = new List<string>();
            returnBilgi.Add(KullaniciAdi);
            returnBilgi.Add(KullaniciSifre);
            return returnBilgi.ToArray();
        }
    }
}
