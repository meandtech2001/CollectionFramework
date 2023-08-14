using ConceptArchitect.Utils;
using System.Diagnostics;
using System.Threading.Tasks;

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

        Console.WriteLine("Time Taken by PerformanceMonitor");

        var resultFn = PerformanceMonitor.Measure(() => n.Factorial()); //to get the result from a function

        var resultP = PerformanceMonitor.Measure(() => Permutation(n, r));

        Console.WriteLine("time took for Factorial: " + resultFn.TimeTaken); //get the time taken from the result 7000ms approx

        Console.WriteLine("time took for Permutation: " + resultP.TimeTaken); //12000ms approx

        Console.WriteLine("Time Taken by Threads");
        var watchf = Stopwatch.StartNew();
        var watchp = Stopwatch.StartNew();

        var fnResult = 1;
        watchf.Start();
        Console.WriteLine($"Factorial Thread starts");
        fnResult = new GenericThread<int>(() => n.Factorial()).Result; //to get the result from a Factorial method
        //var fn = ThreadClass.NewThread(() => Factorial(n)).Thread;
        //var fn = new Thread(() => Factorial(n)); //thread takes no parameters so we can use lambda expression to pass the method with parameters
        //fn.Start();

        var pResult = 1;
        watchp.Start();
        Console.WriteLine($"Permutation Thread starts");
        pResult = new GenericThread<int>(() => Permutation(n, r)).Result; //to get the result from a Permutation method
        //var p = ThreadClass.NewThread(() => Permutation(n, r)).Thread;
        //var p = new Thread(() => Permutation(n, r));
        //p.Start();

        //fn.Join();
        //p.Join();

        Console.WriteLine("time took for Factorial: " + watchf.ElapsedMilliseconds); //get the time taken from the result 12000ms approx
        Console.WriteLine($"Factorial of {n} is " + fnResult);

        Console.WriteLine("time took for Permutation: " + watchp.ElapsedMilliseconds); //12000ms approx
        Console.WriteLine($"Permutation of {n} and {r} is " + pResult);

        Console.WriteLine("Main thread ends");
    }
}