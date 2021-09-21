using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingExperiments.Examples
{
    class AysncRefExample : Example
    {
        public override int Run()
        {
            //to measure performance
            var sw = new Stopwatch();
            sw.Start();

            var taskList = new List<Task<int>>();

            for (var i = 0; i < 60; i++)
            {
                taskList.Add(AddIntAsync(i));
            }

            Thread.Sleep(5);

            Task.WaitAll(taskList.ToArray());

            sw.Stop();
          
            RunCheck(sw.ElapsedMilliseconds);

            return 0;
        }

        private async Task<int> AddIntAsync(int x)
        {
            Console.WriteLine("I'm delaying one second");
            RefList.Add(x);
            await Task.Delay(1000);
            return x;
        }
    }
}
