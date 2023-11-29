using Microsoft.AspNetCore.SignalR.Client;

const string url = "https://localhost:7238/chat";

await using var connection = new HubConnectionBuilder()
    .WithUrl(url)
    .Build();

await connection.StartAsync();


connection.On<Guid>("ReceiveMessage", x =>
{
    Console.WriteLine(x);
});


await foreach (var item in connection.StreamAsync<DateTime>("Streaming"))
{
    Console.WriteLine(item);
}





Console.ReadLine();