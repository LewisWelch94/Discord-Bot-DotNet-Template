using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface IMessageCommand : IDiscordCommand
    {
        Task Execute(DiscordSocketClient client, SocketMessageCommand cmd);
    }
}
