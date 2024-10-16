using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Efsanelere göre, ormanın derinliklerinde saklı Altın Tapınak’a ulaşan kişi, tapınağın
içindeki paha biçilemez hazineyi bulacaktır. Ancak bu tapınağa ulaşmak o kadar kolay değildir.
Tapınağın etrafını kuşatan kadim bir labirent, içinde birçok tuzak ve çıkmaz barındırıyor.
Tapınağın içindeki hazineye ulaşabilmek için maceracılar N x N boyutlarındaki bir labirentte
doğru yolu bulmak zorunda. Labirent, 0 ve 1'lerden oluşan bir grid ile temsil ediliyor. 1’ler
yürünebilen yolları, 0’lar ise tuzaklar veya duvarları temsil ediyor. Maceracılar, sadece 1 olan
hücrelerde yürüyebilirler. Macera sırasında, maceracı tapınağın girişinden (0, 0) hücresinden
başlayarak (N-1, N-1) hücresindeki hazinenin bulunduğu noktaya gitmek zorunda. Ancak bu yolculuk
sırasında en kısa rotayı bulmak çok önemlidir, çünkü tapınağın etrafındaki tuzaklar giderek kapanıyor.
Maceracının yukarı, aşağı, sağ ve sola doğru hareket edebildiğini unutmayın. 
Ayrıca sadece geçerli hücrelere (1 olan hücrelere) adım atabilir. 
Görev: Bu N x N boyutlarındaki labirentte maceracının başlangıç noktasından hazinenin bulunduğu
noktaya en kısa yolu bulması gerekiyor. En kısa yolu bulan bir fonksiyon yazın ve kaç adımda hazinenin
bulunduğunu hesaplayın. Eğer hazineye ulaşılamıyorsa, "Yol Yok" sonucunu döndürün.

Örnek:
Labirent:
1 0 0 0 
1 1 0 1 
0 1 1 1 
0 0 0 1
Çıktı:
En Kısa Yol: 5 adım
Açıklama:
Maceracı (0, 0) noktasından başlayarak aşağı, sağ, sağ, aşağı, sağ adımlarıyla hazineye (3, 3) ulaşır. Bu yolculuk toplam 5 adımdır. */

namespace Main5
{
    /*Labirenti ve başlangıç pozisyonunu oku: (0, 0) hücresinden başla.
    Kuyruk ve Ziyaret Edilenler Listesi: BFS algoritması için bir kuyruk kullanılır.
    Bu kuyruk, maceracının şu anda bulunduğu konumu ve oraya kaç adımda ulaştığını tutar.
    Ziyaret edilen hücreler listesi, maceracının aynı hücreye tekrar girmesini engeller.
    Komşu Hücrelere Gitme: Her adımda, komşu hücrelere (yukarı, aşağı, sağ, sol) gitme imkanı
    kontrol edilir. Sadece geçerli (1 olan) hücrelere gidilebilir.
    Sonuç: Eğer (N-1, N-1) hücresine ulaşırsak, kaç adımda ulaşıldığını döndürürüz. Eğer kuyruk boşalır
    ve hazineye ulaşılamazsa, "Yol Yok" sonucu döndürülür. */
    class Labirent
    {
        // Koordinatları temsil eden Tuple yapısı
        static readonly int[] dx = { -1, 1, 0, 0 }; // Yukarı, Aşağı
        static readonly int[] dy = { 0, 0, -1, 1 }; // Sol, Sağ

        // Labirentte en kısa yolu bulma fonksiyonu
        public static int EnKisaYoluBul(int[,] labirent)
        {
            int N = labirent.GetLength(0);
            if (labirent[0, 0] == 0 || labirent[N - 1, N - 1] == 0)
            {
                return -1;  // Başlangıç veya bitiş hücresi yürünebilir değilse "Yol Yok"
            }

            bool[,] ziyaretEdildi = new bool[N, N];  // Ziyaret edilen hücreleri tutan dizi
            Queue<Tuple<int, int, int>> kuyruk = new Queue<Tuple<int, int, int>>();  // BFS kuyruğu

            // Başlangıç pozisyonunu kuyrukta başlat
            kuyruk.Enqueue(Tuple.Create(0, 0, 1));  // (X, Y, Adım sayısı)
            ziyaretEdildi[0, 0] = true;

            while (kuyruk.Count > 0)
            {
                var current = kuyruk.Dequeue();
                int x = current.Item1;
                int y = current.Item2;
                int steps = current.Item3;

                // Hazineye ulaşıldı mı?
                if (x == N - 1 && y == N - 1)
                {
                    return steps;
                }

                // Komşu hücreleri kontrol et
                for (int i = 0; i < 4; i++)
                {
                    int yeniX = x + dx[i];
                    int yeniY = y + dy[i];

                    // Geçerli koordinat mı? (Labirent sınırları içinde ve yürünebilir mi?)
                    if (yeniX >= 0 && yeniX < N && yeniY >= 0 && yeniY < N &&
                        labirent[yeniX, yeniY] == 1 && !ziyaretEdildi[yeniX, yeniY])
                    {
                        kuyruk.Enqueue(Tuple.Create(yeniX, yeniY, steps + 1));
                        ziyaretEdildi[yeniX, yeniY] = true;  // Bu hücreyi ziyaret edilmiş olarak işaretle
                    }
                }
            }

            // Hazineye ulaşılamadıysa
            return -1;
        }

        public static void Main(string[] args)
        {
            // Labirent örneği
            int[,] labirent = {
            { 1, 0, 0, 0 },
            { 1, 1, 0, 1 },
            { 0, 1, 1, 1 },
            { 0, 0, 0, 1 }
            };

            int sonuc = EnKisaYoluBul(labirent);
            if (sonuc != -1)
            {
                Console.WriteLine("En Kısa Yol: " + sonuc + " adım");
            }
            else
            {
                Console.WriteLine("Yol Yok");
            }

            Console.Read();
        }
    }
}
