using Grpc.Core;
using Grpc.Net.Client;
using Zad2;

var channel = GrpcChannel.ForAddress("https://localhost:7080");
var client = new NumberService.NumberServiceClient(channel);
string? input;

try
{
    do
    {
        Console.WriteLine("Perform procedure AverageValue. (Press 1)");
        Console.WriteLine("Perform procedure ComputeArray. (Press 2)");
        Console.WriteLine("Exit. (Press 0)");
        input = Console.ReadLine();

        switch (input)
        {
            case "0": Console.WriteLine("Exiting..."); break;
            case "1": await AverageValue(); break;
            case "2": await ComputeArray(); break;
            default: Console.WriteLine("Invalid input. Please try again."); break;
        }
    } while (input != "0");
}
catch (RpcException ex)
{
    Console.WriteLine($"RPC Error: {ex.Status.Detail}");
}
async Task AverageValue()
{
    Console.WriteLine("Enter the border number for average procedure:");
    int border = int.Parse(Console.ReadLine());

    try
    {
        var request = new Request { Value = border };
        var response = await client.AverageValueAsync(request);
        Console.WriteLine(response.ToString());
    }
    catch (RpcException ex)
    {
        Console.WriteLine($"RPC Error: {ex.Status.Detail}");
    }
}
async Task ComputeArray()
{
    Console.WriteLine("Enter the umber of elements in the array:");
    int n = int.Parse(Console.ReadLine());
    List<int> values = new List<int>();
    Console.WriteLine("Enter the elements of the array:");
    for (int i = 0; i < n; i++)
    {
        values.Add(int.Parse(Console.ReadLine()));
    }

    try
    {
        var call = client.ComputeArray();

        var readTask = Task.Run(async () =>
        {
            await foreach (var response in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(response.Value);
            }
        });

        foreach (var v in values)
        {
            await call.RequestStream.WriteAsync(new Request { Value = v});
        }

        await call.RequestStream.CompleteAsync();
        await readTask;
    }
    catch (RpcException ex)
    {
        Console.WriteLine($"RPC Error: {ex.Status.Detail}");
    }
}
