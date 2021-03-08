using System;
using System.Net.Http;
using Grpc.Net.Client;

namespace GrpcGreeterClient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;


            // using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });

            System.Console.WriteLine("Greeting: " + reply.Message);
            System.Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
