using Confluent.Kafka;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "rocket-01.srvs.cloudkafka.com:9094,rocket-02.srvs.cloudkafka.com:9094,rocket-03.srvs.cloudkafka.com:9094",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "<username>",
                SaslPassword = "<password>",
                GroupId = Guid.NewGuid().ToString(), // See lecture
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SslCaLocation = @"<put your path here>/ca-root.crt"
            };

            var cts = new CancellationTokenSource();

            // Strg + C
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            var list = new Dictionary<int, Person>();
            var consumedMessages = 0;

            using (var consumer = new ConsumerBuilder<int, Person>(consumerConfig).SetValueDeserializer(new ProtoDeserializer()).Build())
            {
                // REQ-01 subscribe to topic
                
                // REQ-01 end

                while (cts.IsCancellationRequested == false)
                {
                    consumedMessages++;

                    try
                    {
                        // REQ-02: Cosnume (pass cts.Token) message and store result in list


                        // REQ-02 end

                        // REQ-03 Print message to console

                        // REQ-03 End
                    }
                    catch (OperationCanceledException)
                    { }
                }
            }

            Console.WriteLine("PRESS STRG+C to stop");

            Console.WriteLine($"Consumed {consumedMessages} messages. Last known state of the entities:");

            foreach (var item in list.Values.OrderBy(i => i.Id))
            {
                Console.WriteLine(item);
            }
        }
    }
}
