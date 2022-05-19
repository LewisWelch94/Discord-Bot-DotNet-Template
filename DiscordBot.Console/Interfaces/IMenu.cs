using Discord;
using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface IButton : IBotAction
    {
        public string CustomId();
        public ButtonBuilder Component();
        Task Execute(DiscordSocketClient client, SocketMessageComponent cmd);
    }
}
