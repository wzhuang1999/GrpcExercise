using System;
using System.IO;
using System.Threading.Tasks;
using Consul;

namespace ConsoleApp1
{
    public class Config
    {
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                var serializer = new YamlDotNet.Serialization.Serializer();
                // REQ: Serialize config

                // REQ END

                serializer.Serialize(stringWriter, configuration);               
            }

            using (var client = new ConsulClient(ConfigOverride))
            {
                // REQ: Store the config

                // REQ End
            }

            using (var client = new ConsulClient(ConfigOverride))
            {
                // REQ: Retreive the config

                // REQ End
            }

            Console.ReadKey();
        }

        private static void ConfigOverride(ConsulClientConfiguration obj)
        {
            obj.Address = new Uri(@"http://127.0.0.1:8500/");
        }
    }
}
