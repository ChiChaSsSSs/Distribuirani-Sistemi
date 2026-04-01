using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Zad2;

namespace Zadatak2Server.Services
{
    public class NumberServiceImpl : NumberService.NumberServiceBase
    {
        private static float acc = 1;

        public override Task<Empty> AverageValue(Request request, ServerCallContext context)
        {
            if (request.Value <= 0)
            {
                return Task.FromResult(new Empty());
            }

            float sum = 0;
            for (int i =1; i <= request.Value; i++)
            {
                sum += i;
            }

            acc += sum / (float)request.Value;

            return Task.FromResult(new Empty());
        }

        public override async Task ComputeArray(IAsyncStreamReader<Request> request, IServerStreamWriter<Reply> response, ServerCallContext context)
        {
            int counter = 1;
            await foreach (var req in request.ReadAllAsync())
            {
                if (counter % 3 == 0)
                {
                    await response.WriteAsync(new Reply { Value = req.Value * acc });
                }
                else
                {
                    await response.WriteAsync(new Reply { Value = req.Value - acc });
                }
                counter++;
            }
        }
    }
}
