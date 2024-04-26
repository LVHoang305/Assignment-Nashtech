using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace PrimeNumber
{
    static class Globals
    {
        public static List<int> primes;

        public static Task final;
    }
    class Program
    {
        public static async Task GetPrimeNumberAsync(List<int> splitPart)
        {
            await Task.Run(() =>
        {
            foreach (var num in splitPart)
            {
                if (IsPrime(num))
                {
                    Globals.primes.Add(num);
                }
            }
        });
            await Task.Delay(2000);
        }

        static bool IsPrime(int number)
        {
            if (number <= 1)
                return false;
            if (number == 2 || number == 3)
                return true;
            if (number % 2 == 0 || number % 3 == 0)
                return false;
            int i = 5;
            while (i * i <= number)
            {
                if (number % i == 0)
                    return false;
                i += 2;
            }
            return true;
        }

        static List<List<int>> SplitRangeIntoParts(int a, int b, int parts)
        {
            List<List<int>> splitParts = new List<List<int>>();
            int range = b - a + 1;
            int partSize = range / parts;
            int remainder = range % parts;

            int start = a;
            for (int i = 0; i < parts; i++)
            {
                int end = start + partSize - 1;
                if (i < remainder)
                {
                    end++;
                }
                List<int> part = Enumerable.Range(start, end - start + 1).ToList();
                splitParts.Add(part);
                start = end + 1;
            }

            return splitParts;
        }

        static async Task Main(string[] arg)
        {
            Globals.primes = [];
            int a = 0;
            int b = 10000000;
            int parts = 2;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            List<List<int>> splitParts = SplitRangeIntoParts(a, b, parts);

            for (int i = 0; i < splitParts.Count; i++)
            {
                Globals.final = GetPrimeNumberAsync(splitParts[i]);
            }
            await Task.WhenAll(Globals.final);
            foreach (var prime in Globals.primes)
            {
                Console.Write(prime);
                Console.Write(" ");
            }
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}