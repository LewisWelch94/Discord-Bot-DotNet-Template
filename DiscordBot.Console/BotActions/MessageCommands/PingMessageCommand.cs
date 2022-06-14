using Discord.WebSocket;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.BotActions.MessageCommands
{
    public class PingUserCommand : IMessageCommand
    {
        public string Name() => "Message Ping";

        public async Task Execute(DiscordSocketClient client, SocketMessageCommand cmd)
        {
            await cmd.RespondAsync("Message has been pinged!", ephemeral: true);
        }
    }
}
