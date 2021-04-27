using System;
using System.Collections.Generic;

namespace Assignment7
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var threadExecutor = new TaskExecutor();
            var clock = new Clock()
            {
                Interval = new TimeSpan(0, 0, 0, 2)
            };

            var rateLimitedExecutor = new RateLimitedExecutor(threadExecutor, clock, new List<Action>()
            {
                () => { Console.WriteLine("1");},
                () => { Console.WriteLine("2");},
                () => { Console.WriteLine("3");},
                () => { Console.WriteLine("4");},
                () => { Console.WriteLine("5");},
                () => { Console.WriteLine("6");},
            });

            rateLimitedExecutor.Enqueue(() => Console.WriteLine("Another Action."));

            Console.ReadKey();
        }
    }
}
