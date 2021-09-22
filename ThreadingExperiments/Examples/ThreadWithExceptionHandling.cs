using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace ThreadingExperiments.Examples
{
    class ThreadWithExceptionHandling : Example
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

            foreach (var thread in threads)
            {
                thread.Join(); //synchronization to wait for all threads. Join outside the for loop for performance.
            }

            sw.Stop();

            RunCheck(sw.ElapsedMilliseconds);

            return 0;
        }

        private int DoSomethingWithParams(string x, int y)
        {
            try
            {
                Console.WriteLine($"string is {x} on {Thread.CurrentThread.ManagedThreadId}");               

                if (y == 15)
                {
                    throw new Exception("this is a made up exception");
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"There was a problem with the argument {y}");
                RefList.Add(y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The value {y} was added via a finally statement. continuing");
                Console.ForegroundColor = ConsoleColor.White;
            }

            finally
            {
                RefList.Add(y);                
            }
            Thread.Sleep(1000);
            return y;
        }
    }
}
