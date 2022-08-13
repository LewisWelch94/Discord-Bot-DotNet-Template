namespace DiscordBot.Web.Controllers
{
    using Discord;
    using Discord.WebSocket;
    using DiscordBot.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DiscordSocketClient _client;

        public HomeController(ILogger<HomeController> logger, DiscordSocketClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_client.ConnectionState != ConnectionState.Connected) return Ok(new { Message = "Bot is offline" }); 
            return Ok(new { Message = $"Bot is Online! Name {_client.CurrentUser.Username}#{_client.CurrentUser.DiscriminatorValue}" });
        }
    }
}