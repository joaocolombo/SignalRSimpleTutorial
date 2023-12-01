using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRSimpleTutorial.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<MainHub, IMainHub> _hub;

        public HomeController(IHubContext<MainHub, IMainHub> hub)
        {
            _hub = hub;
        }

        [HttpGet("triggerAll")]
        public async Task<IActionResult> RunPushNotificationAll()
        {
            await _hub.Clients.All.ReceiveNotification($"Hub Server Time {DateTime.Now}");
            return Ok();
        }
    }
}
