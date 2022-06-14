using Discord;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Triggers
{
    public class PingTrigger : ITrigger
    {
        public bool Triggered(IDiscordClient client, IMessage msg)
        {
            if (msg.Content == "ping")
            {
                return true;
            }
            return false;
        }

        public async Task Execute(IDiscordClient client, IMessage msg)
        {
            await MessageUtils.Reply(msg, "Pong ;)");
            await Task.CompletedTask;
        }
    }
}
