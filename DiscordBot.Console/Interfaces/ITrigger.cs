using Discord;
using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface ITrigger : IBotAction
    {
        bool Triggered(IDiscordClient client, IMessage msg);
        Task Execute(IDiscordClient client, IMessage msg);
    }
}
