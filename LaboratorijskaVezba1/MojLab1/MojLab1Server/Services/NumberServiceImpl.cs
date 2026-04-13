using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MojLab;

namespace MojLab1Server.Services
{
    public class NumberServiceImpl : NumberService.NumberServiceBase
    {
        private static float acc = 12;
        public override async Task<Empty> ComputeAverage(IAsyncStreamReader<Request> request, ServerCallContext context)
        {
            int counter = 0;
            float sum = 0;
            await foreach (var req in request.ReadAllAsync())
            {
                sum += req.Value;
                counter++;
                acc = sum / (float)counter;
            }
            return new Empty();
        }

        public override async Task ChangeArray(IAsyncStreamReader<Request> request, IServerStreamWriter<Response> response, ServerCallContext context)
        {
            int counter = 1;
            await foreach (var req in request.ReadAllAsync())
            {
                if (counter % 2 == 0)
                {
                    await response.WriteAsync(new Response { Value = acc + req.Value });
                } else
                {
                    await response.WriteAsync(new Response { Value = acc * req.Value });
                }
                counter++;
            }
        }
    }
}
