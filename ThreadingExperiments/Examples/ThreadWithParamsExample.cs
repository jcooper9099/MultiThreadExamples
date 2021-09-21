using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace ThreadingExperiments.Examples
{
    /// <summary>
    /// An example class for starting threads manually
    /// </summary>
    internal class ThreadWithParamsExample : Example
    {       
        public override int Run()
        {
            //to measure performance
            var sw = new Stopwatch();
            sw.Start();

            List<Thread> threads = new List<Thread>();

            for (var i = 0; i < 60; i++)
            {
                var iCopy = i; //neded so that i is not treaded as a reference object;
                var threadX = new Thread(() => DoSomethingWithParams($"Hello the int is {iCopy}", iCopy));
                threadX.Start(); // Start threads here and not wait for join
                threads.Add(threadX);
            }

            foreach(var thread in threads)
            {
                thread.Join(); //synchronization to wait for all threads. Join outside the for loop for async performance.
            }

            sw.Stop();           

            RunCheck(sw.ElapsedMilliseconds);

            return 0;
        }

        private int DoSomethingWithParams(string x, int y)
        {
            Console.WriteLine($"string is {x} on {Thread.CurrentThread.ManagedThreadId}");
            RefList.Add(y);
            Thread.Sleep(1000);
            return y;
        }
    }
}
