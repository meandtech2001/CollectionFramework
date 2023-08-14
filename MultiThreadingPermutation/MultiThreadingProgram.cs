using ConceptArchitect.Utils;
using System.Diagnostics;

public static class MultiThreadingProgram
{
    public static void CountDown(int max)
    {
        Thread t = Thread.CurrentThread; // Get the current thread
        Console.WriteLine($"Thread {t.ManagedThreadId} started");
        while (max > 0)
        {
            Console.WriteLine($"#{t.ManagedThreadId}: {max}");
            max--;
        }
        Console.WriteLine($"Thread {t.ManagedThreadId} ends");
    }

    public static int Factorial(this int n) //extension method
    {
        //Thread.Sleep(n * 1000); // Sleep for n seconds (just to simulate a long running operation)
        int result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
            Thread.Sleep(1000);
        }

        return result;
    }

    public static int Permutation(this int n, int r)
    {
        //Thread.Sleep((n + (n - r)) * 1000);
        return Factorial(n) / Factorial(n - r);
    }

    private static void Main()
    {
        int n = 7;
        int r = 2;
        int fnResult = 1;
        int pResult = 1;

        var resultFn = PerformanceMonitor.Measure(() => n.Factorial()); //to get the result from a function

        var resultP = PerformanceMonitor.Measure(() => Permutation(n, r));

        Console.WriteLine("time took for Factorial: " + resultFn.TimeTaken); //get the time taken from the result 7000ms approx

        Console.WriteLine("time took for Permutation: " + resultP.TimeTaken); //12000ms approx

        var watchf = Stopwatch.StartNew();
        var watchp = Stopwatch.StartNew();

        watchf.Start();
        var fn = new GenericThread<int>(() => Factorial(n)); //calling the GenericThread class with the method Factorial which internally creats a thread and returns the result
        //var fn = GenericThread.NewThread(() => fnResult = Factorial(n));
        //var fn = new Thread(() => Factorial(n)); //thread takes no parameters so we can use lambda expression to pass the method with parameters
        //fn.Start();

        watchp.Start();
        var p = new GenericThread<int>(() => Permutation(n, r)); //calling the GenericThread class with the method Permutation which internally creats a thread and returns the result

        //var p = GenericThread.NewThread(() => pResult = Permutation(n, r));
        //var p = new Thread(() => Permutation(n, r));
        //p.Start();

        //GenericThread.ThreadJoin();

        Console.WriteLine($"Factorial of {n} is {fn.Result}");
        Console.WriteLine($"Permutation of {n} and {r} is {p.Result}");

        Console.WriteLine("time took for Factorial: " + watchf.ElapsedMilliseconds); //get the time taken from the result 12000ms approx

        Console.WriteLine("time took for Permutation: " + watchp.ElapsedMilliseconds); //12000ms approx

        Console.WriteLine("Main thread ends");
    }
}