using Microsoft.AspNetCore.SignalR.Client;

const string url = "https://localhost:7238/chat";

await using var connection = new HubConnectionBuilder()
    .WithUrl(url)
    .Build();

await connection.StartAsync();

await foreach (var item in connection.StreamAsync<DateTime>("Streaming"))
{
    Console.WriteLine(item);
}

Console.WriteLine("Hello, World!");
