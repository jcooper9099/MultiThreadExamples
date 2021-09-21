using ThreadingExperiments.Examples;
using System;

namespace ThreadingExperiments
{
    class Program
    {
        private static bool DoRun = true;
        static void Main(string[] args)
        {
            while (DoRun)
            {
                PresentMenu();

                var choice = Console.ReadKey();

                if (char.IsDigit(choice.KeyChar))
                {

                    RunExample(int.Parse(choice.KeyChar.ToString()));
                }
                else if(choice.KeyChar.ToString().ToUpper() == "X")
                {
                    DoRun = false;
                    Environment.Exit(0);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\nInvalid selection");
                }
            }
        }

        private static void RunExample(int exampleId)
        {
            Console.WriteLine(Environment.NewLine);

            switch (exampleId)
            {
                case 1:
                    new TasksExample().Run();
                    break;
                case 2:
                    new TasksWithParamsExample().Run();
                    break;
                case 3:
                    new ThreadPoolExample().Run();
                    break;
                case 4:
                    new ThreadWithParamsExample().Run();
                    break;
                case 5:
                    new AysncRefExample().Run();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\nInvalid  Example selection");
                    break;
            }
        }

        private static void PresentMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Welcome to multi-threading examples by JB Cooper.\r\n");
            Console.WriteLine("\r\nPlease choose a number to see an example of multi-threading and how it improves performance.");
            Console.WriteLine("\r\nEach example will run a task 60 times using different strategies, each taking 1 second.\r\nBecause of multi threading you will see that performance is greatly improved.\r\n");
            Console.WriteLine("Each example will also run a check to show that the threads all ran.\r\n");
            Console.WriteLine("1 - Task Example");
            Console.WriteLine("2 - Tasks with parameters example");
            Console.WriteLine("3 - ThreadPool Example");
            Console.WriteLine("4 - Threads with parameters example");
            Console.WriteLine("5 - Async Tasks example");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press X to exit this program");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("***************************************");

        }
    }
}
