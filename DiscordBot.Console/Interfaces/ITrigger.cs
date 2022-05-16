using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.Triggers
{
    public interface ITrigger : IBotAction
    {
        bool Triggered(IDiscordClient client, IMessage msg);
        Task Execute(IDiscordClient client, IMessage msg);
    }
}
