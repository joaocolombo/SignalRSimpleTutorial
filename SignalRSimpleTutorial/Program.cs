using Microsoft.AspNetCore.SignalR;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

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


class MyHub : Hub
{
    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
    {
        while (true)
        {
            yield return DateTime.Now;
            await SendMessage();
            await Task.Delay(1000, cancellationToken);
        }
    }

    public async Task SendMessage()
    {

            await Clients.All.SendAsync("ReceiveMessage", Guid.NewGuid());
            await Task.Delay(1000);
    }
}
