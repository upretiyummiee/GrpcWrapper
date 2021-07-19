using customstream;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Streamer.StreamerClient(channel);
            var tokensource = new CancellationTokenSource(TimeSpan.FromSeconds(9));
            var result = client.GetCount(new Empty(),cancellationToken:tokensource.Token);
            try
            {
                await foreach (var item in result.ResponseStream.ReadAllAsync(tokensource.Token)) 
                {
                    Console.WriteLine("  "+item.C);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }
            Console.WriteLine(result);
            _ = Console.ReadKey();
        }
    }
}
