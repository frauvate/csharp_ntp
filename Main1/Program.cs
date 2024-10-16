using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Spiral Matris: NxN boyutlarında oluşturulan bir matrisin ve bu matrisi spiral şekilde yazdıracak program
namespace Main1
{
    class SpiralMatrix
    {
        static void Main(string[] args)
        {
            Console.Write("Matris boyutunu girin (N): "); //kullanıcıdan spiralin sonu için bir değer alınması
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[n, n]; //alınan n değeri matrixin boyutlarını oluşturur

            int value = 1; // Başlangıç değeri. yazdırma işlemibu değerden başlar
            int minRow = 0, maxRow = n - 1;
            int minCol = 0, maxCol = n - 1;

            // Spiral matris doldurma işlemi
            while (value <= n * n)
            {
                // Yukarıdan sağa doğru
                for (int i = minCol; i <= maxCol; i++)
                    matrix[minRow, i] = value++;
                minRow++;

                // Sağdan aşağıya doğru
                for (int i = minRow; i <= maxRow; i++)
                    matrix[i, maxCol] = value++;
                maxCol--;

                // Aşağıdan sola doğru
                for (int i = maxCol; i >= minCol; i--)
                    matrix[maxRow, i] = value++;
                maxRow--;

                // Soldan yukarıya doğru
                for (int i = maxRow; i >= minRow; i--)
                    matrix[i, minCol] = value++;
                minCol++;
            }

            // Spiral matrisin yazdırılması
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
                Console.Read();
            }
        }
    }
}
