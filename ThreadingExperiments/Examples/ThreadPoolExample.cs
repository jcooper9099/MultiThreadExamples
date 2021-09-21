using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

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

            sw.Stop();

            RunCheck(sw.ElapsedMilliseconds);

            return 0;
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
