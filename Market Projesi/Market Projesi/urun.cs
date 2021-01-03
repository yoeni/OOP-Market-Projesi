using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Projesi
{
    public class urun
    {
        public string urunBarkod { get; set; }
        public string UrunAd { get; set; }
        public float UrunFiyat { get; set; }
        private Tedarikci UrunTedarikci { get; set; }
        public int UrunStok { get; set; }
        private DateTime SonIndirimTarihi { get; set; }
        private float sonIndirimOrani { get; set; }
        public urun(string barkod, string urunAd,float UrunFiyat,int stok,Tedarikci urunTedarikci)
        {
            this.urunBarkod = barkod;
            this.UrunAd = urunAd;
            this.UrunFiyat = UrunFiyat;
            this.UrunTedarikci = urunTedarikci;
            this.UrunStok = stok;
            urunTedarikci.urunEkle(this);
        }
        public void urunIndirimUygula(float oran)
        {
            SonIndirimTarihi = DateTime.Now;
            sonIndirimOrani = oran;
            UrunFiyat -= UrunFiyat * oran / 100;
        }
        public void StokDuzenle(int stokDüzenlemsi)
        {
            UrunStok += stokDüzenlemsi;
        }
        public Tedarikci tedarikciAl()
        {
            return UrunTedarikci;
        }
        public string sonIndirim()
        {
            if (SonIndirimTarihi.Year==1)
            {
                return "---";
            }
            else
            {
                return SonIndirimTarihi.ToString();
            }
        }
        public float sonIndirimOran()
        {
            return sonIndirimOrani;
        }
    }
}
