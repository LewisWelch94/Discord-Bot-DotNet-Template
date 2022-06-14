using Discord.WebSocket;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.BotActions.UserCommands
{
    public class PingUserCommand : IUserCommand
    {
        public string Name() => "User Ping";

        public async Task Execute(DiscordSocketClient client, SocketUserCommand cmd)
        {
            await cmd.RespondAsync($"<@{cmd.User.Id}> you have been pinged!");
            await Task.CompletedTask;
        }
    }
}
