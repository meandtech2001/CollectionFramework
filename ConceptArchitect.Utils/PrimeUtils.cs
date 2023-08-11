using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace ConceptArchitect.Utils
{
    public static class PrimeUtils
    {
        public static bool IsPrime(this int value)
        {
            if (value < 2)
                return false;

            for (int i = 2; i < value; i++)
                if (value % i == 0)
                    return false;

            return true;
        }

        public static List<int> FindPrimes(int min, int max)
        {
            var primes = new List<int>();
            for (int i = min; i <= max; i++)
                if (IsPrime(i))
                    primes.Add(i);
            return primes;
        }

        public static void FindPrimesCb(int min, int max, Action<int, bool> primeAlert)
        {
            for (int i = min; i < max; i++)

                if (IsPrime(i))
                    primeAlert(i, false);

            primeAlert(0, true); //job done
        }
    }

    public class PrimeEnumerator : IEnumerator<int>
    {
        private int current;
        private int min;
        private int max;

        public IEnumerator<int> GetEnumerator() //this is the only method that is required to implement IEnumerable<T> and foreach loop
        {
            return this; //this is the enumerator inside the PrimeEnumerator
        }

        public PrimeEnumerator(int min, int max)
        {
            this.current = min - 1;
            this.max = max;
        }

        public int Current
        {
            get
            {
                return current;
            }
        }

        public bool MoveNext()
        {
            do
            {
                current++;
            } while (current <= max && !current.IsPrime());

            return current <= max;
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }

        object System.Collections.IEnumerator.Current => Current;
    }
}