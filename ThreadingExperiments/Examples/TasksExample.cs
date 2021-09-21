using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingExperiments.Examples
{
    class TasksExample : Example
    {
        public override int Run()
        {
            //to measure performance
            var sw = new Stopwatch();
            sw.Start();

            var tokenSource = new CancellationTokenSource();

            var taskList = new List<Task<int>>();

            for (var i = 0; i < 60; i++)
            {
                Func<int> action = () =>
                {
                    return Wait1Second();
                };

                Task<int> task = Task<int>.Factory.StartNew(action,tokenSource.Token);
                
                taskList.Add(task);
                RefList.Add(i); // Demonstrates that a ref list is accessible by the main and sub-threads
            }            

            Thread.Sleep(5);

            Task.WaitAll(taskList.ToArray());

            sw.Stop();

            RunCheck(sw.ElapsedMilliseconds);           

            return 0;
        }

        private int Wait1Second()
        {
            Console.WriteLine("I'm Waiting one second");
            Thread.Sleep(1000);
            return 0;
        }
    }
}
