using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRSimpleTutorial.Controllers
{
    public class HomeController : Controller
    {
        private readonly HubConnection _hubConnection;

        public HomeController(HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
        }

        [HttpGet("trigger")]
        public async Task<IActionResult> RunPushNotification()
        {
            await _hubConnection.SendAsync("SendMessage", Guid.NewGuid().ToString());
            return new OkResult();
        }
    }
}
