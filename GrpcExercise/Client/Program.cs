using System;
using Grpc.Core;
using Helloworld;

namespace Client
{


    class Program
    {
        static void Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
            
            var client = new OrderService.OrderServiceClient(channel);
            
            var item1 = new Item {Id = 1, Price = 25.0};
            var item2 = new Item {Id = 2, Price =50.5};
            var item3 = new Item {Id = 3, Price = 37.0};
            var item4 = new Item {Id = 4, Price =12.0};
            var item5 = new Item {Id = 5, Price = 9.99};
            
            var order1 = client.CreateOrder(new CreateOrderRequest {UserId = 1, Items = {item1, item2}});
            var order2 = client.CreateOrder(new CreateOrderRequest {UserId = 2, Items = {item3, item4, item5}});
            
            Console.WriteLine("Order-1 item count: " + order1.ItemCount);
            Console.WriteLine("Order-2 item count: " + order2.ItemCount);
            
            client.PriceChanges(new NoParams());

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
