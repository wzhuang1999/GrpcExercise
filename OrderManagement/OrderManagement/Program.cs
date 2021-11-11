﻿using EasyNetQ;
using EasyNetQ.Topology;
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
                Console.WriteLine(Encoding.UTF8.GetString(body));
            });

            var properties = new MessageProperties();

            bus.Publish(new Exchange("Billing"), "US", false, properties, Encoding.UTF8.GetBytes("First Order!"));

            Console.ReadKey();
        }
    }
}
