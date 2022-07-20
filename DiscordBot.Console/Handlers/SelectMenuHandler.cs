using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Handlers
{
    public class SelectMenuHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly SocketMessageComponent _cmd;
        private IEnumerable<IMenu>? Menus { get; set; }

        public SelectMenuHandler(DiscordSocketClient client, SocketMessageComponent cmd)
        {
            _client = client;
            _cmd = cmd;
        }

        public async Task ProcessAsync()
        {
            if (Menus == null) Menus = new InterfaceUtils<IMenu>().GetClasses();

            var button = Menus.Where(x => x.IsActive && x.CustomId() == _cmd.Data.CustomId).FirstOrDefault();

            if (button == null)
            {
                await _cmd.RespondAsync("This menu has been deactivated", ephemeral: true);
                return;
            }

            await button.Execute(_client, _cmd);
            await Task.CompletedTask;
        }   
    }
}
