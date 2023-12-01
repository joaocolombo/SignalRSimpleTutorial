using System.Threading;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRSimpleTutorial;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
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
app.MapHub<MainHub>("chat");
app.UseCors(pol => pol.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();