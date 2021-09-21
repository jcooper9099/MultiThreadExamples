using System;
using System.Collections.Generic;

namespace ThreadingExperiments.Examples
{
    internal abstract class Example
    {
        public List<int> RefList = new List<int>();
        public abstract int Run();

        public void RunCheck(long milliseconds)
        {
            var elapsedSeconds = milliseconds / 1000;

            Console.WriteLine("----------------"); // A separator for UI
           
            for (int i = 0; i < 60; i++)
            {
                if (!RefList.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: {i} was NOT FOUND in the list");
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{i} was found in the referenced list");
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"All threads finished, {elapsedSeconds} seconds elapsed. A reduction to 1/{60 / elapsedSeconds} of the time.");
        }
       
    }
}
