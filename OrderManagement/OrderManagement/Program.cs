using EasyNetQ;
using EasyNetQ.Topology;
using Entities;
using System;
using System.Text;

namespace OrderManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin").Advanced;
          
            var exchange = bus.ExchangeDeclare("Billing", ExchangeType.Topic);
            var queue = bus.QueueDeclare("Billing.US", false, false, false);
            bus.Bind(exchange, queue, "US");

            bus.Consume(queue, (body, properties, info) =>
            {
                // var @object = ProtoSerializer.Deserialize<Address>(body);

                Console.WriteLine(Encoding.UTF8.GetString(body));
            });

            var properties = new MessageProperties();

            //var stream = ProtoSerializer.Serialize(new Address());

            bus.Publish(new Exchange("Billing"), "US", false, properties, Encoding.UTF8.GetBytes("First Order!"));

            Console.ReadKey();
        }
    }
}
