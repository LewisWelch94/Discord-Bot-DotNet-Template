using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.BotActions.Reactions
{
    public class ThumbsUpAndPexelReaction : IDiscordReaction
    {
        public List<Emoji> Emojis()
        {
            return new List<Emoji>
            {
                Emoji.Parse("👍")
            };
        }

        public List<Emote> Emotes()
        {
            return new List<Emote>
            {
                Emote.Parse("<:pexel:986214270297591859>")
            };
        }

        public async Task ExecuteReactionAdded(DiscordSocketClient client, IMessage msg, IChannel channel, SocketReaction reaction)
        {
            if (reaction.Emote.Name == "pexel")
            {
                await MessageUtils.Reply(msg, "You have added a custom emote!");
            }
            else
            {
                await MessageUtils.Reply(msg, "You have added a thumbs up emote!");
            }
        }

        public async Task ExecuteReactionRemoved(DiscordSocketClient client, IMessage msg, IChannel channel, SocketReaction reaction)
        {
            if (reaction.Emote.Name == "pexel")
            {
                await MessageUtils.Reply(msg, "You have removed a pexel emote!");
            }
            else
            {
                await MessageUtils.Reply(msg, "You have removed a thumbs up!");
            }
        }
    }
}
