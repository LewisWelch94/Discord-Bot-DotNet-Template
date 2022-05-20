using Discord;
using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface IMenu : IBotAction
    {
        public string CustomId();
        public SelectMenuBuilder Component();
        Task Execute(DiscordSocketClient client, SocketMessageComponent cmd);
    }
}
