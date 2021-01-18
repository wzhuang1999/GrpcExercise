using System;
using Prometheus;
using System.Threading;
using System.Threading.Tasks;

namespace Metrics
{
    class Program
    {
        static void Main()
        {
            for (int i = 0; i < 30; i++)
            {
                Task.Run(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(100);
                    }
                });
            }

            Console.ReadKey();
        }
    }
}