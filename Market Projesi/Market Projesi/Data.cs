using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Projesi
{
    public class Data
    {
        public Calisan yonetici;
        public MarketSube inonuMarketA;
        public Calisan inonuMarketAMudur,inonuMarketAKasiyer;
        public List<MarketSube> marketler = new List<MarketSube>();
        public List<Tedarikci> tedarikciler = new List<Tedarikci>();
        public void Load()
        {
            inonuMarketA = new MarketSube("İnönü Mahallesi","İnönü Market A Şubesi");

            yonetici = new Calisan("Mert Bakır", 6500, "İstanbul",sistem.pozisyonlar.yonetici);
            yonetici.GirisBilgileriBelirle("mert123","123");
            
            inonuMarketAMudur = new Calisan("Hakan Baltacı", 4800, "İnönü Mahallesi", sistem.pozisyonlar.mudur);
            inonuMarketAMudur.GirisBilgileriBelirle("hakan465", "159456");
            inonuMarketAMudur.MarketSubeAta(inonuMarketA);

            inonuMarketAKasiyer = new Calisan("Pelin Gök", 2400, "İnönü Mah.", sistem.pozisyonlar.kasiyer);
            inonuMarketAKasiyer.GirisBilgileriBelirle("pelin123","123456");
            inonuMarketAKasiyer.MarketSubeAta(inonuMarketA);

            inonuMarketA.MudurAta(inonuMarketAMudur);

            tedarikciler.Add(new Tedarikci("Aslan Toptancılık", "İstanbul", sistem.tedarikGrubu.gida));
            tedarikciler.Add(new Tedarikci("Kaplan Kozmetik", "İstanbul", sistem.tedarikGrubu.temizlik));
            tedarikciler.Add(new Tedarikci("Hayat Teknoloji", "Tekirdağ", sistem.tedarikGrubu.kampanya));

            urun cikolata = new urun("123","Çikolata",3.5F,165,tedarikciler[0]);
            urun deterjan = new urun("124","Deterjan", 49.9F, 123,tedarikciler[1]);
            urun mp3calar = new urun("125", "MP3 Çalar", 88F,12, tedarikciler[2]);
            urun patates = new urun("126", "Patates", 8F,1562,tedarikciler[0]);
            urun sabun = new urun("127", "Sabun", 4F,487,tedarikciler[1]);
            urun dezenfektan = new urun("128", "Dezenfektan", 2.5F,2145, tedarikciler[1]);

            inonuMarketA.UrunEkle(cikolata);
            inonuMarketA.UrunEkle(deterjan);
            inonuMarketA.UrunEkle(mp3calar);
            inonuMarketA.UrunEkle(patates);
            inonuMarketA.UrunEkle(sabun);
            inonuMarketA.UrunEkle(dezenfektan);

            marketler.Add(inonuMarketA);

            
        }
    }
}
