using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Handlers
{
    public class ButtonHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly SocketMessageComponent _cmd;

        public ButtonHandler(DiscordSocketClient client, SocketMessageComponent cmd)
        {
            _client = client;
            _cmd = cmd;
        }

        public async void Process()
        {
            var button = new InterfaceUtils<IButton>().GetClasses().Where(x => x.IsActive && x.CustomId() == _cmd.Data.CustomId).FirstOrDefault();

            if (button == null)
            {
                await _cmd.RespondAsync("This button has been deactivated", ephemeral: true);
                return;
            }

            await button.Execute(_client, _cmd);
        }
    }
}
