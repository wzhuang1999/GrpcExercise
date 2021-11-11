using System;
using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;

namespace Server
{
    class OrderServiceImpl : OrderService.OrderServiceBase
    {
        public override Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CreateOrderResponse() { Id = 1 , ItemCount = request.Items.Count +1});
        }

        public override async Task PriceChanges(NoParams request, IServerStreamWriter<Item> responseStream, ServerCallContext context)
        {
            var rnd = new Random();
            var rndNum = rnd.Next(1, 200);
            var item = new Item(){ Id = 3, Price = rndNum};
            await responseStream.WriteAsync(item);
        }
    }

    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            Grpc.Core.Server server = new Grpc.Core.Server
            {
                Services = { OrderService.BindService(new OrderServiceImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();
            
            Console.WriteLine("Greeter server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
