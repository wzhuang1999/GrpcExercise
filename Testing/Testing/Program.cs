using System;
using Autofac;
using Testing.Interfaces;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Bootstrapper().BuildContainer();

            var result = container.Resolve<IClassA>().Method1(2, 3);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
