using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//İki Matrisin Çarpımı: Kullanıcıdan alınan iki NxN matrisin çarpımını gerçekleştiren bir program
namespace Main2
{
    class MatrixMultiplication
    {
        static void Main(string[] args)
        {
            Console.Write("Matris boyutunu girin (N): "); //matris boyutları için kullanıcı girdisi (karesel matris)
            int n = int.Parse(Console.ReadLine()); //parse metodu string girdiyi int'e çevirmek için kullanıldı

            int[,] matrix1 = new int[n, n];
            int[,] matrix2 = new int[n, n];
            int[,] resultMatrix = new int[n, n];

            Console.WriteLine("1. matrisi girin:");
            for (int i = 0; i < n; i++) //kullanıcıdan matrisin her bir elemanını tek tek almak için oluşturulan döngü
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"Matris1[{i},{j}] = ");
                    matrix1[i, j] = int.Parse(Console.ReadLine()); 
                }
            }

            Console.WriteLine("2. matrisi girin:"); 
            for (int i = 0; i < n; i++) // döngü 0,0 elemanından başlayarak girdiğimiz boyuta kadar devam eder
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"Matris2[{i},{j}] = ");
                    matrix2[i, j] = int.Parse(Console.ReadLine()); 
                }
            }

            // matris çarpımı algoritması. örneğin birinci matrisin 0,0 elemanıyla ikinci matrisin 0,1 elemanı,
            //sonuç matrisinin 0,1 elemanını verecektir
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    resultMatrix[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        resultMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            // Sonuç matrisini yazdırma
            Console.WriteLine("Sonuç matrisi:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) // satır ve sütunları doğru şekilde yazdırmak için iç içe iki döngüye ihtiyaç duyuldu
                {
                    Console.Write(resultMatrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.Read();
        }
    }
}
