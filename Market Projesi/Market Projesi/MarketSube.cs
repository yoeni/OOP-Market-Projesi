using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Projesi
{
    public class MarketSube
    {
        public string SubeAdi { get; private set; }
        private List<Calisan> Calisanlar = new List<Calisan>();
        private List<urun> Urunlerim = new List<urun>();
        private DateTime AcilisTarihi { get; set; }
        private string Adres { get; set; }
        private Calisan mudur;
        public Calisan Mudur { 
            get
            {
                if (mudur == null)
                    return new Calisan("null", 0, "null", sistem.pozisyonlar.mudur);
                else
                    return mudur;
            }
            private set
            {
                if (value.Mevki==sistem.pozisyonlar.mudur)
                {
                    mudur = value;
                }
                else
                {
                    Console.WriteLine("Yanlış pozisyon ataması!");
                }
            }
        }
        public MarketSube(string adres,string subeAdi)
        {
            this.Adres=adres;
            this.SubeAdi = subeAdi;
            this.AcilisTarihi = DateTime.Now;
        }
        public MarketSube(string adres, string subeAdi,Calisan mudur)
        {
            this.Adres = adres;
            this.SubeAdi = subeAdi;
            if (mudur.Mevki == sistem.pozisyonlar.mudur)
            {
                this.Mudur = mudur;
                CalisanEkle(Mudur);
            }
        }
        public void MudurSil()
        {
            this.mudur = null;
        }
        public void MudurAta(Calisan mudur)
        {
            if (mudur.Sube==null)
            {
                mudur.MarketSubeAta(this);
            }
            Calisanlar.Remove(Mudur);
            if (!Calisanlar.Contains(mudur))
            {
                Calisanlar.Add(mudur);
            }
            Mudur = mudur;

        }
        public void CalisanSil(Calisan calisanim)
        {
            Calisanlar.Remove(calisanim);
            if (calisanim.Mevki==sistem.pozisyonlar.mudur)
            {
                this.mudur = null;
            }
        }
        public string[] marketBilgileriGetir()
        {
            List<string> bilgiler = new List<string>();
            bilgiler.Add(Mudur.IsimSoyisim);
            bilgiler.Add(Adres);
            bilgiler.Add(AcilisTarihi.ToString("dd/MM/yyyy"));
            return bilgiler.ToArray();
        }
        public string CalisanEkle(Calisan calisan)
        {
            try
            {
                if (!Calisanlar.Contains(calisan))
                {
                    Calisanlar.Add(calisan);
                }
                return "Başarıyla Eklendi";
            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }
        public List<Calisan> CalisanlarListe()
        {
            return Calisanlar;
        }
        public List<urun> UrunleriListele()
        {
            return Urunlerim;
        }
        public string UrunEkle(urun urun)
        {
            try
            {
                this.Urunlerim.Add(urun);
                return "Başarıyla Eklendi";
            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }
        public void urunGuncelle(string urunBarkod,string urunAd,int urunStok)
        {
            for (int i = 0; i < Urunlerim.Count; i++)
            {
                if (Urunlerim[i].urunBarkod==urunBarkod)
                {
                    Urunlerim[i].UrunAd = urunAd;
                    Urunlerim[i].UrunStok = urunStok;
                    break;
                }
            }
        }
        public void urunGuncelle(string urunBarkod, int oran)
        {
            for (int i = 0; i < Urunlerim.Count; i++)
            {
                if (Urunlerim[i].urunBarkod == urunBarkod)
                {
                    Urunlerim[i].urunIndirimUygula(oran);
                    break;
                }
            }
        }
        public void urunSil(string urunBarkod)
        {
            for (int i = 0; i < Urunlerim.Count; i++)
            {
                if (Urunlerim[i].urunBarkod == urunBarkod)
                {
                    Urunlerim.Remove(Urunlerim[i]);
                    break;
                }
            }
        }
        public void TedarikciSil(Tedarikci tedarikci)
        {
            foreach (urun item in Urunlerim.ToArray())
            {
                if (item.tedarikciAl()==tedarikci)
                {
                    Urunlerim.Remove(item);
                }
            }
        }
    }
}
