using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System;

namespace KafkaExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // Just run once or create with Kafka Tool
            //using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = "localhost:9092" }).Build())
            //{
            //    adminClient.CreateTopicsAsync(new[] { new TopicSpecification
            //    {
            //        Name = "test",
            //        ReplicationFactor = 1,
            //        NumPartitions = 1
            //    }}).Wait();
            //}

            var configProducer = new ProducerConfig { BootstrapServers = "localhost:9092", EnableIdempotence = true };

            using (var producer = new ProducerBuilder<Null, string>(configProducer).Build())
            {
                producer.Produce("test", new Message<Null, string> { Value = "a log message" });
                producer.Flush();
            }

            ///////////////////////////////

            var configConsumer = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "foo", // meaining see VO
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(configConsumer).Build())
            {
                consumer.Subscribe("test");

                var consumeResult = consumer.Consume();

                Console.WriteLine(consumeResult.Message.Value);

                consumer.Close();
            }

            Console.ReadKey();
        }
    }
}
