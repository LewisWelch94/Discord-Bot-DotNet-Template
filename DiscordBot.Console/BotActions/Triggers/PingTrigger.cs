using Discord;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.BotActions.Triggers
{
    public class PingTrigger : ITrigger
    {
        public bool Triggered(IMessage msg)
        {
            if (msg.Content != "ping") return false;
            return true;
        }

        public async Task Execute(IMessage msg)
        {
            await MessageUtils.Reply(msg, "Pong ;)");
            await Task.CompletedTask;
        }
    }
}
