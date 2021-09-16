using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                var threadX = new Thread(() => DoSomethingWithParams($"Hello{iCopy}", iCopy));
                threadX.Start(); // Start threads here and not wait for join
                threads.Add(threadX);
            }

            foreach(var thread in threads)
            {
                thread.Join(); //synchronization to wait for all threads. Join outside the for loop for async performance.
            }

            sw.Stop();

            Console.WriteLine("----------------"); // A separator for UI

            RefList.OrderBy(x => x).ToList().ForEach((x) => Console.WriteLine($"{x} was found in the reference list"));

            var elapsedSeconds = sw.ElapsedMilliseconds / 1000;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"All threads finished, {elapsedSeconds} seconds elapsed. A reduction to 1/{60 / elapsedSeconds} of the time.");

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
