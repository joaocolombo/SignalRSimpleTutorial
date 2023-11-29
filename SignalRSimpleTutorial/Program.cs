using Microsoft.AspNetCore.SignalR;
using System.Threading;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddScoped(sp=>
{
    var conn = new HubConnectionBuilder()
        .WithUrl("https://localhost:7238/chat")
        .Build();
    conn.StartAsync().GetAwaiter().GetResult();
    return conn;
});

var app = builder.Build();
app.MapHub<MyHub>("/chat");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();


public class MyHub : Hub
{
    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
    {
        while (true)
        {
            yield return DateTime.Now;
            await Task.Delay(1000, cancellationToken);
        }
    }

    public async Task SendMessage(Guid guid)
    {
        await Clients.All.SendAsync("ReceiveMessage", guid);
        await Task.Delay(1000);
    }
}
