using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Projesi
{
    public class Tedarikci
    {
        
        public string TedarikciAdi { get; private set; }
        private string Adres { get; set; }
        private sistem.tedarikGrubu TedarikGrubu { get; set; }
        private List<urun> Urunlerim = new List<urun>();
        public Tedarikci(string tedarikciAdi,string adres, sistem.tedarikGrubu tedarikGrubu)
        {
            this.TedarikciAdi = tedarikciAdi;
            this.TedarikGrubu = tedarikGrubu;
            this.Adres = adres;
        }
        public void urunEkle(urun urunum)
        {
            Urunlerim.Add(urunum);
        }
        public string TedarikGrubuOgren()
        {
            if (this.TedarikGrubu==sistem.tedarikGrubu.gida)
            {
                return "Gıda";
            }
            else if (this.TedarikGrubu==sistem.tedarikGrubu.temizlik)
            {
                return "Temizlik";
            }
            else
            {
                return "Haftalık Kampanya Ürünleri";
            }
        }
        public string AdresOgren()
        {
            return Adres;
        }
    }
}
