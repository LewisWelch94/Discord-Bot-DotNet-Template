using Discord;
using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface IDiscordModal : IBotAction
    {
        public string CustomId();
        public ModalBuilder Component();
        Task Execute(DiscordSocketClient client, SocketModal modal);
    }
}
