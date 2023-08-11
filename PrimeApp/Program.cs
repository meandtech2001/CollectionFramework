using ConceptArchitect.Utils;
using System.Diagnostics;

internal class Program
{
    private static void Main()
    {
        int min = 2;
        int max = 100000;
        int count = 0;

        //using LINQ ---------------method 5

        var primeLinq = (from prime in new PrimeEnumerator(min, max)
                         where prime % 10 == 7
                         select prime)
                         .Skip(10)
                         .Take(5);

        foreach (var prime in primeLinq)
        {
            Console.WriteLine(prime);
        }
    }

    public int PrimeFinderWithCallBack(int min, int max, int count)
    {
        //using callback and action delegate -------------method 2
        PrimeUtils.FindPrimesCb(min, max, (prime, done) =>
        {
            //if (done)
            //    Console.Write($"{prime}\t"); //method 2 earlier version
            if (!done)
            {
                int pc = (prime - min) * 100 / (max - min);
                Console.Write($"\r{pc}%");//loading animation
                count++;
                //Console.Write(" + ");//loading animation
                //count++;
            }
            else
                Console.WriteLine($"\nTotal prime is {count}");
        });
        return count;
    }

    public void PrimeFinderOriginal()
    {
        //earlier version ---------------method 1
        int min = 2;
        int max = 100;

        Console.WriteLine("Please wait...");
        var result = PerformanceMonitor.Measure(() => PrimeUtils.FindPrimes(min, max));
    }

    public void PrimeFinderWithDelegate()
    {
        int min = 2;
        int max = 100;
        int count = 0;

        //using callback and action delegate -------------method 2
        PrimeUtils.FindPrimesCb(min, max, (prime, done) =>
        {
            //if (done)
            //    Console.Write($"{prime}\t"); //method 2 earlier version
            if (!done)
            {
                count++;
                Console.Write(" + "); //loading animation
            }
            else
                Console.WriteLine($"\nTotal prime found was = {count}");
        });
    }

    public void PrimeFinderUsingWhile()
    {
        //using Enumerator and while loop ------------method 3
        int min = 2;
        int max = 100;
        int count = 0;
        var finder = new PrimeEnumerator(min, max);

        while (finder.MoveNext())
        {
            var prime = finder.Current;
            if (prime >= max)
                break;

            if (prime % 10 == 7)
            {
                Console.WriteLine(prime);
                count++;
                if (count == 5)
                    break;
            }
        }
    }

    public void PrimeFinderUsingForeach()
    {
        //using Enumerator and foreach loop ----------------method 4
        int min = 2;
        int max = 100;
        int count = 0;
        var finder = new PrimeEnumerator(min, max);

        foreach (var prime in finder)
        {
            if (prime >= max)
                break;

            if (prime % 10 == 7)
            {
                Console.WriteLine(prime);
                count++;
                if (count == 5)
                    break;
            }
        }
    }
}