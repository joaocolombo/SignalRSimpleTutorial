using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalRSimpleTutorial;

[Authorize]
public class MainHub : Hub<IMainHub>
{
    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
    {
        while (true)
        {
            yield return DateTime.Now;
            await Task.Delay(1000, cancellationToken);
        }
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).ReceiveNotification($"New client connected: {Context.User?.Identity?.Name}");
        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string message)
    {
        await Clients.All.ReceiveNotification(message);
    }
}

public interface IMainHub
{
    Task ReceiveNotification(string message);
}