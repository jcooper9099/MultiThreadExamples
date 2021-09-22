using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingExperiments.Examples
{
    class TasksWithExceptions : Example
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
                var iCopy = i;
                Task<int> task = new Task<int>(() => Wait1Second(iCopy));

                taskList.Add(task);
                task.Start();
            }

            Thread.Sleep(500);

            try
            {
                Task.WaitAll(taskList.ToArray());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("AN exception was caught in a task");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Exception handled in main thread");
                Console.ForegroundColor = ConsoleColor.White;
            }

            sw.Stop();

            RunCheck(sw.ElapsedMilliseconds);

            return 0;
        }

        private int Wait1Second(int x)
        {
            Console.WriteLine($"I'm Waiting one second and adding {x}");
            try
            {
                if (x == 15)
                {
                    throw new Exception("This is a made up exception");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Exception is with {x}");
                Console.ForegroundColor = ConsoleColor.White;
                throw ex;
            }
            finally
            {
                RefList.Add(x);
                Thread.Sleep(1000);
            }
           
            return x;
        }
    }
}
