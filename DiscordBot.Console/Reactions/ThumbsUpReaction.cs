using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Reactions
{
    public class ThumbsUpReaction : IDiscordReaction
    {
        public List<Emoji> Emojis()
        {
            return new List<Emoji>
            {
                Emoji.Parse("👍")
            };
        }

        public async Task ExecuteReactionAdded(DiscordSocketClient client, IMessage msg, IChannel channel, SocketReaction reaction)
        {
            await MessageUtils.Reply(msg, "You have added a thumbs up!");
        }

        public async Task ExecuteReactionRemoved(DiscordSocketClient client, IMessage msg, IChannel channel, SocketReaction reaction)
        {
            await MessageUtils.Reply(msg, "You have removed a thumbs up!");
        }
    }
}
