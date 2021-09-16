using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// This example is strange. Why does it not write to the screen 60 times? 
/// </summary>
namespace ThreadingExperiments.Examples
{
    internal class ThreadPoolExample : Example
    {       
        public override  int Run()
        {
            //to measure performance
            var sw = new Stopwatch();
            sw.Start();

            List<EventWaitHandle> resetEvents = new List<EventWaitHandle>();

            for (var i = 0; i < 60; i++) {
                var iCopy = i;
                var resetEvent = new EventWaitHandle(false, EventResetMode.ManualReset);

                resetEvents.Add(resetEvent); // add this outside of the call to QueuserWorkItem otherwise reset events might be empty;

                ThreadPool.QueueUserWorkItem(callBack =>
                {                                    
                    Wait1Second(iCopy, resetEvent);
                });
            }

            WaitHandle.WaitAll(resetEvents.ToArray());

            Console.WriteLine("----------------"); // A separator for UI

            RefList.OrderBy(x => x).ToList().ForEach((x) => Console.WriteLine($"{x} was found in the reference list"));

            var elapsedSeconds = sw.ElapsedMilliseconds / 1000;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"All threads finished, {elapsedSeconds} seconds elapsed. A reduction to 1/{60/elapsedSeconds} of the time.");

            return 1;
        }

        private void Wait1Second(int x, EventWaitHandle resetEvent)
        {
            Console.WriteLine($"I'm waiting one second the int is {x}");
            RefList.Add(x);
            resetEvent.Set();
            Thread.Sleep(1000);
        }
    }
}
