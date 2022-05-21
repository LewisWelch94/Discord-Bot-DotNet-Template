using Discord;
using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface IDiscordReaction : IBotAction
    {
        List<Emoji> Emojis();
        Task ExecuteReactionAdded(DiscordSocketClient client, IMessage msg, IChannel channel, SocketReaction reaction);
        Task ExecuteReactionRemoved(DiscordSocketClient client, IMessage msg, IChannel channel, SocketReaction reaction);
    }
}
