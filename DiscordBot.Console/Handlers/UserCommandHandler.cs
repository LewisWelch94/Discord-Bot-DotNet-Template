using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Handlers
{
    public class UserCommandHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly SocketUserCommand _cmd;

        public UserCommandHandler(DiscordSocketClient client, SocketUserCommand cmd)
        {
            _client = client;
            _cmd = cmd;
        }

        public async void Process()
        {
            var command = new InterfaceUtils<IUserCommand>().GetClasses().Where(x => x.Name().ToLower() == _cmd.Data.Name && x.IsActive).FirstOrDefault();

            if (command == null)
            {
                await _cmd.RespondAsync("This user command has been deactivated", ephemeral: true);
                return;
            }

            await command.Execute(_client, _cmd);
        }
    }
}
