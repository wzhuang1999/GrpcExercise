using Consul;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var programm = new Program();
            var tasks = new Task[10];

            for (int i = 0; i < 10; i++)
            {
                var tempId = i;
                tasks[i] = Task.Run(async () => await programm.Instance(tempId));
            }

            await Task.WhenAll(tasks);

            Console.ReadKey();
        }

        public async Task Instance(int id)
        {
            Console.WriteLine($"{id} started");

            using (var client = new ConsulClient())
            {
                Console.WriteLine($"{id} waiting for lock ...");
                
                // REQ Acquire a lock

                // REQ End

                Console.WriteLine(lockInstance.IsHeld + " by " + id);

                await Task.Delay(3000);

                await lockInstance.Release();
            }
        }
    }
}
