using System;
using System.Net.Http;
using System.Text.Json;
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
            var channel = GrpcChannel.ForAddress("https://localhost:5001/", new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new Customer.CustomerClient(channel);
            var reply = await client.GetCustomerAsync(new CustomerRequest { Id = 1 });

            Console.WriteLine(JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));

            System.Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
