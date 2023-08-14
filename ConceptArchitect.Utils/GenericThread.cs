using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.Utils
{
    public delegate T LongRunTask<T>();

    public class GenericThread<Return> //GenericThread return type is Return
    {
        public Thread thread;
        private Return result;

        public Return Result //Result is a property of GenericThread
        {
            get
            {
                if (thread.IsAlive) //if thread is still running
                    thread.Join(); //hiding the join method inside the result property
                return result; //return the result
            }
        }

        public GenericThread(Func<Return> func) //func is a function that returns Return
        {
            thread = new Thread(() => result = func()); //thread is a new thread that runs the function
            thread.Start(); //start the thread
        }

        public void Wait() //wait for the thread to complete
        {
            thread.Join();
        }

        //-------old code
        //public static Thread /*NewThreadResult<T>*/ NewThread<T>(LongRunTask<T> task)
        //{
        //    T result;
        //    newThread = new Thread(() => result = task());
        //    newThread.Start();

        //    return newThread;
        //    //return new NewThreadResult<T>()
        //    //{
        //    //    Thread = newThread,
        //    //    Result = result
        //    //};
        //}

        ////public static List<Thread> ThreadJoin<T>(LongRunTask<T> task)
        ////{
        ////    task.Join();
        ////}
        //public static void ThreadJoin()
        //{
        //    newThread.Join();
        //}
    }
}