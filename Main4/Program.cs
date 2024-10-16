using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Bir gün, büyük bir teknoloji şehri olan TechCity büyük bir felaketle karşı karşıya kaldı. 
Şehrin merkezindeki büyük bir veri merkezine kötü amaçlı yazılım bulaştı ve tüm bilgisayarlar 
ve sistemler tehlikeye girdi. Eğer bu yazılım durdurulamazsa, şehir tamamen çökecek ve insanlar
dijital kimliklerini kaybedecekler. TechCity'nin merkezindeki mühendisler, durumu kurtarmak
için çok hızlı düşünmek zorundalar. Şehrin veri merkezindeki bilgisayar ağı, bir N x N boyutunda
bir grid ile temsil ediliyor. Bu grid, birbirine bağlı bilgisayar düğümlerinden oluşuyor ve bazı
düğümler zarar görmüş durumda. Şehir, 3 robot kullanarak zararı durdurmaya çalışacak. Ancak robotlar
birbirlerinden bağımsız çalışıyor ve aynı anda birden fazla düğüme müdahale edebiliyorlar.
Robotlar bir kez çalışmaya başladıklarında, zarar görmemiş bir düğüme geçiş yapabiliyorlar ve komşu
düğümlere (yukarı, aşağı, sağ, sol) müdahale edebiliyorlar. 

Amaç, robotların şehirdeki en fazla düğümü kurtarmasıdır.Ancak robotlar aynı düğüme birden fazla kez
müdahale edemez. Her robot yalnızca bir düğüme bir kez müdahale edebilir ve komşu düğümlere geçebilir.
Görev: Bir N x N boyutundaki grid'i (haritayı) ve robotların başlangıç noktalarını temsil eden bir listeyi
giriş olarak alan bir fonksiyon yazın. Bu fonksiyon, robotların kaç tane düğümü kurtarabileceğini hesaplamalıdır. 
Her bir hücre aşağıdaki değerlerle temsil edilir:
1: Bu düğüm zarar görmemiştir ve robot burayı kurtarabilir.
0: Bu düğüm zarar görmüş ve robot tarafından müdahale edilemez.
Her robot, sadece kendi komşu düğümlerine (yukarı, aşağı, sağ, sol) müdahale edebilir.
Robotlar aynı anda çalışacaklar ve robotlar aynı düğüme iki kez müdahale edemez.


Örnek:
Grid: 
1 1 0 1 
0 1 0 0 
1 1 1 0 
0 0 1 1 

Başlangıç Pozisyonları: 
Robot 1: (0, 0)
Robot 2: (2, 2) 
Robot 3: (3, 3)

Açıklama:
Robot 1 (0, 0) pozisyonundan başlayarak 4 düğüm kurtarır(sol üst köşedeki düğümler).
Robot 2 (2, 2) pozisyonundan başlayarak 3 düğüm kurtarır(alt ortadaki düğümler).
Robot 3 (3, 3) pozisyonundan başlayarak 1 düğüm kurtarır(en sağ alt düğüm).
Aynı düğümün birden fazla robot tarafından kurtarılmaması gerekir.
Robotlar sadece komşu düğümlere hareket edebilir (yukarı, aşağı, sağ, sol).
Her robot en fazla kaç düğüm kurtarabilir hesaplanmalı.
Verilen grid ve başlangıç pozisyonlarına göre robotların optimal hareketi belirlenmelidir. */

namespace Main4
{
    class TechCityRescue
    {
        static int[,] grid;
        static char[,] finalGrid;
        static bool[,] visited;
        static int n;

        // Yönler: yukarı, aşağı, sağ, sol
        static int[] dx = { -1, 1, 0, 0 }; //x ekseninde sol -1 sağ +1
        static int[] dy = { 0, 0, -1, 1 }; //y ekseninde aşağı -1 yukarı +1

        public static void Main(string[] args)
        {
            // Grid ve başlangıç pozisyonları
            grid = new int[,] {
            { 1, 1, 0, 1 },
            { 0, 1, 0, 0 },
            { 1, 1, 1, 0 },
            { 0, 0, 1, 1 }
        };
            n = grid.GetLength(0);

            visited = new bool[n, n]; // Ziyaret edilen düğümleri izlemek için
            finalGrid = new char[n, n]; // Kurtarılan düğümleri göstermek için

            // Başlangıçta grid'i yazdır
            Console.WriteLine("İlk Grid Durumu:");
            PrintGrid(grid);

            // Robot başlangıç pozisyonları
            List<(int, int)> robotPositions = new List<(int, int)> {
            (0, 0),
            (2, 2),
            (3, 3)
             };

            // Final grid'i başlat (ilk grid'e göre)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    finalGrid[i, j] = grid[i, j] == 1 ? '1' : '0'; // 1 olanlar zarar görmemiş, 0 olanlar zarar görmüş
                }
            }

            // Her robot için kurtarılabilecek düğümleri hesapla
            int totalSavedNodes = 0;
            foreach (var (x, y) in robotPositions)
            {
                totalSavedNodes += Bfs(x, y); //Breadth-First Search algoritmasını kullanmak için metod çağrılır 
            }

            // Final grid'i yazdır (robotların müdahale ettiği düğümler)
            Console.WriteLine("\nSon Grid Durumu:");
            PrintGrid(finalGrid);

            Console.WriteLine($"\nToplam kurtarılan düğüm sayısı: {totalSavedNodes}");
            Console.Read();
        }

        // BFS algoritması: önce başlangıç düğümünden tüm komşularına, sonra onların komşularına ve bu şekilde genişleyerek ilerler.
        static int Bfs(int startX, int startY)
        {
            //Başlangıç düğümünden başlar ve sıraya (queue) bu düğümü ekler.
            //Kuyruğun başındaki düğümü(mevcut düğümü) çıkarır ve işleme alır.
            //İşlem sırasında bu düğümün tüm komşularını kontrol eder. Henüz ziyaret edilmemiş komşu düğümleri kuyruğa ekler.
            //Her düğümün yalnızca bir kez ziyaret edilmesini sağlar. Ziyaret edilen düğümler bir "visited"(ziyaret edilenler) listesinde tutulur.
            //Kuyruk boşalana kadar bu süreç devam eder.
            // Başlangıç pozisyonu zarar görmemişse işlem yap
            if (grid[startX, startY] == 0 || visited[startX, startY])
                return 0;

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((startX, startY));
            visited[startX, startY] = true;

            int savedNodes = 0;

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                savedNodes++;

                // Kurtarılan düğüme 'X' koyalım
                finalGrid[x, y] = 'X';

                // 4 komşu düğümde gez
                for (int i = 0; i < 4; i++)
                {
                    int newX = x + dx[i];
                    int newY = y + dy[i];

                    // Geçerli sınırlar içinde ve henüz ziyaret edilmemişse
                    if (IsValid(newX, newY))
                    {
                        queue.Enqueue((newX, newY));
                        visited[newX, newY] = true;
                    }
                }
            }

            return savedNodes;
        }

        // Geçerli bir hücre olup olmadığını kontrol eden fonksiyon
        static bool IsValid(int x, int y)
        {
            return x >= 0 && x < n && y >= 0 && y < n && grid[x, y] == 1 && !visited[x, y];
        }

        // Grid'i yazdıran fonksiyon
        static void PrintGrid(int[,] g)
        {
            for (int i = 0; i < g.GetLength(0); i++)
            {
                for (int j = 0; j < g.GetLength(1); j++)
                {
                    Console.Write(g[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        // Char grid'i(kurtarılmış noktaları x ile işaretleyen grid) yazdıran fonksiyon
        static void PrintGrid(char[,] g)
        {
            for (int i = 0; i < g.GetLength(0); i++)
            {
                for (int j = 0; j < g.GetLength(1); j++)
                {
                    Console.Write(g[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
