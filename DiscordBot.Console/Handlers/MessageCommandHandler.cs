using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Handlers
{
    public class MessageCommandHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly SocketMessageCommand _cmd;

        public MessageCommandHandler(DiscordSocketClient client, SocketMessageCommand cmd)
        {
            _client = client;
            _cmd = cmd;
        }

        public async void ProcessAsync()
        {
            var command = new InterfaceUtils<IMessageCommand>().GetClasses().Where(x => x.Name() == _cmd.Data.Name && x.IsActive).FirstOrDefault();

            if (command == null)
            {
                await _cmd.RespondAsync("This user command has been deactivated", ephemeral: true);
                return;
            }

            await command.Execute(_client, _cmd);
        }
    }
}
