using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

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

            Console.WriteLine("----------------"); // A separator for UI

            RefList.OrderBy(x => x).ToList().ForEach((x) => Console.WriteLine($"{x} was found in the reference list"));

            var elapsedSeconds = sw.ElapsedMilliseconds / 1000;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"All threads finished, {elapsedSeconds} seconds elapsed. A reduction to 1/{60 / elapsedSeconds} of the time.");

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
