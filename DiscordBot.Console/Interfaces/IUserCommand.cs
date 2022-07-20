using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface IUserCommand : IDiscordCommand
    {
        Task Execute(DiscordSocketClient client, SocketUserCommand cmd);
    }
}
