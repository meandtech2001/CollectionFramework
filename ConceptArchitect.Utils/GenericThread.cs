using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.Utils
{
    public delegate T LongRunTask<T>();

    public class GenericThread<Return> //generic class to handle threads and return values of any type
    {
        public Thread Thread { get; set; }
        private Return result;

        public Return Result
        {
            get
            {
                if (Thread.IsAlive) //if thread is still running, wait for it to finish
                    Thread.Join(); //hiding the join method from the user
                return result;
            }
        }

        public GenericThread(LongRunTask<Return> task) //constructor will start the thread
        {
            Thread = new Thread(() => result = task()); //Thread will execute the task and store the result
            Thread.Start();
        }

        public void Wait()
        {
            Thread.Join();
        }

        //old code
        //public Thread NewThread<T>(LongRunTask<T> task)
        //{
        //    T result;
        //    Thread = new Thread(() => result = task());
        //    Thread.Start();

        //    return Thread;
        //}

        //public void ThreadJoin()
        //{
        //    Thread.Join();
        //}
    }
}