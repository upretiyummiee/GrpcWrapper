using customstream;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class StreamerService: Streamer.StreamerBase
    {
        public override async Task GetCount(Empty request, IServerStreamWriter<Count> responseStream, ServerCallContext context)
        {
            var count = 0;
            while (!context.CancellationToken.IsCancellationRequested) {
                await responseStream.WriteAsync(new Count { C = count });
                count++;
                Thread.Sleep(200);
            } 
        }
    }
}
