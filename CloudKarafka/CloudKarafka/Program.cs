using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CloudKarafka
{
    class Program
    {
        private static int counter = 0;

        static void Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "rocket-01.srvs.cloudkafka.com:9094,rocket-02.srvs.cloudkafka.com:9094,rocket-03.srvs.cloudkafka.com:9094",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "<username>",
                SaslPassword = "<password>"
            };

            var list = new List<Person>();

            // REQ-01: Create 3 dummy persons
            
            // REQ-01 end

            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            var random = new Random(34787);

            using (var p = new ProducerBuilder<int, Person>(config).SetValueSerializer(new ProtoBufSerializer()).Build())
            {
                // REQ-02: Publish initial state of all persons (use DeliveryHandler). Dont forget to clone the person and use person.id as key
                // Remark: clone is important, if you dont have control on the time when the entity will we serialized. think about why

                // REQ-02 end

                p.Flush();

                // Simulate changes
                while (cts.IsCancellationRequested == false)
                {
                    // REQ-03: Get a random person (1,2,3). Inncrease the Version of Fullname and Email by one. Publish the result (use DeliveryHandler)

                    // REQ-03 end                              

                    Thread.Sleep(TimeSpan.FromMilliseconds(500));
                }

                p.Flush();
            }

            Console.WriteLine("PRESS STRG+C to stop");
            Console.WriteLine($"Wrote {counter} messages");
            list.ForEach(l => Console.WriteLine(l.ToString()));
        }

        private static void DeliveryHandler(DeliveryReport<int, Person> report)
        {
            counter++;
            Console.WriteLine($"Delivered '{report.Value}' to '{report.TopicPartitionOffset}'");
        }
    }
}
