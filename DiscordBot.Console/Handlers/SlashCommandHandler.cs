using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Handlers
{
    public class SlashCommandHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly SocketSlashCommand _cmd;

        public SlashCommandHandler(DiscordSocketClient client, SocketSlashCommand cmd)
        {
            _client = client;
            _cmd = cmd;
        }

        public async void ProcessAsync()
        {
            var command = new InterfaceUtils<ISlashCommand>().GetClasses().Where(x => x.Name().ToLower() == _cmd.Data.Name && x.IsActive).FirstOrDefault();

            if (command == null)
            {
                await _cmd.RespondAsync("This command has been deactivated", ephemeral: true);
                return;
            }

            await command.Execute(_client, _cmd);
        }
    }
}
