using Grpc.Core;
using Grpc.Net.Client;
using MojLab;

var channel = GrpcChannel.ForAddress("https://localhost:7199");
var client = new NumberService.NumberServiceClient(channel);
string? input;

try
{
    do
    {
        Console.WriteLine("Perform procedure AverageValue: (Press 1)");
        Console.WriteLine("Perform procedure ChangeInput: (Press 2)");
        Console.WriteLine("Exit: (Press 3)");

        input = Console.ReadLine();

        switch (input)
        {
            case "1":
                await AverageValue();
                break;
            case "2":
                await ChangeInput();
                break;
            case "3":
                Console.WriteLine("Exiting...");
                break;
            default:
                Console.WriteLine("Invalid input! Try again.");
                break;
        }
    } while (input != "3");
}
catch(RpcException ex)
{
    Console.WriteLine($"RPC Error: {ex.Status.Detail}");
}
async Task AverageValue()
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
        var call = client.ComputeAverage();
        foreach (var value in values)
        {
            await call.RequestStream.WriteAsync(new Request { Value = value });
        }
    }
    catch (RpcException ex)
    {
        Console.WriteLine($"RPC Error: {ex.Status.Detail}");
    }
}
async Task ChangeInput()
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
        var call = client.ChangeArray();

        var readTask = Task.Run(async () =>
        {
            await foreach (var res in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(res.Value);
            }
        });

        foreach (var value in values)
        {
            await call.RequestStream.WriteAsync(new Request { Value = value });
        }

        await call.RequestStream.CompleteAsync();
        await readTask;
    }
    catch (RpcException ex)
    {
        Console.WriteLine($"RPC Error: {ex.Status.Detail}");
    }
}


