using Microsoft.AspNetCore.SignalR.Client;

const string url = "https://localhost:7238/chat";

await using var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7238/chat", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult<string?>("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNvbnNvbGUiLCJzdWIiOiJjb25zb2xlIiwianRpIjoiY2YxNDExZjkiLCJhdWQiOlsiaHR0cDovL2xvY2FsaG9zdDo0NDA0NiIsImh0dHBzOi8vbG9jYWxob3N0OjQ0MzE0IiwiaHR0cDovL2xvY2FsaG9zdDo1MDgyIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIzOCJdLCJuYmYiOjE3MDE0MzQwNjUsImV4cCI6MTcwOTI5NjQ2NSwiaWF0IjoxNzAxNDM0MDY1LCJpc3MiOiJkb3RuZXQtdXNlci1qd3RzIn0.cfuktATDG-zvrSr609DoSsKwu8ZGx1cCGHBmUtthRx0");
    })
    .Build();

await connection.StartAsync();


connection.On<string>("ReceiveNotification", Console.WriteLine);


await foreach (var item in connection.StreamAsync<DateTime>("Streaming"))
{
    Console.WriteLine(item);
}


Console.ReadLine();