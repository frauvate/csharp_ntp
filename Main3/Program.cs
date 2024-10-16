using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//N'e Kadar Asal Sayıların Toplamı: Kullanıcıdan alınan N sayısına kadar olan tüm asal sayıların toplamını bulan bir program
namespace Main3
{
    class PrimeSum
    {
        static void Main(string[] args)
        {
            Console.Write("N sayısını girin: "); //son sayı olarak belirlenecek sayı kullanıcıdan alınır
            int n = int.Parse(Console.ReadLine());
            int sum = 0;

            for (int i = 2; i <= n; i++)
            {
                if (IsPrime(i)) // isprime metodunu çağırıp, asalsa toplama ekler, değilse sonraki sayıya geçer
                {
                    sum += i;
                }
            }

            Console.WriteLine($"N'e kadar olan asal sayıların toplamı: {sum}");
            Console.Read();
        }

        // Asal sayı kontrol fonksiyonu
        static bool IsPrime(int number)
        {
            if (number <= 1) //1'den küçük sayılar asal değildir
                return false;
            for (int i = 2; i <= Math.Sqrt(number); i++) //sayının karekökünü tam bölen 1 ve kendisi hariç bir sayı varsa, asal değildir
            {
                if (number % i == 0)
                    return false;
            }
            return true; //kalan durumlarda sayı asaldır
        }
    }
}
