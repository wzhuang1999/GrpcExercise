using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace ELK.Core
{
    public class Foo
    {
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Foo f = new Foo
            {
                Prop1 = "AAAAA",
                Prop2 = "BBBB"
            };

            Log.Error("{@Foo}", f);

            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }
    }
}
