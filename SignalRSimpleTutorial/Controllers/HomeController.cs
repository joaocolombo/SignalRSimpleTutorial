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


        [HttpGet("triggerUser/{name}")]
        public async Task<IActionResult> RunPushNotificationUser(string name)
        {
            await _hub.Clients.User(name).ReceiveNotification($"Just for {name} - {DateTime.Now}");
            return Ok();
        }
    }
}
