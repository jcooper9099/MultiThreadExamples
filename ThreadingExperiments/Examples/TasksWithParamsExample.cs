using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace ThreadingExperiments.Examples
{
    internal class TasksWithParamsExample : Example
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
                var x = i;
                Func<int> action = () =>
                {
                    return DoSomethingWithParams("Hello", x);
                };

                Task<int> task = Task<int>.Factory.StartNew(action, tokenSource.Token);
                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());

            sw.Stop();           

            RunCheck(sw.ElapsedMilliseconds);

            return 0;
        }
   

        private int DoSomethingWithParams(string x, int y)
        {
            Console.WriteLine($"The string was {x} the int was {y} ");
            RefList.Add(y);
            Thread.Sleep(1000);
            return y;
        }
    }
}
