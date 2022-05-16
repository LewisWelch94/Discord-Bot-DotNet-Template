using Discord;

namespace DiscordBot.Console.Utils
{
    public static class MessageUtils
    {
        public async static Task Reply(IMessage msg, string replyMessage)
        {
            if (msg == null) throw new ArgumentNullException(nameof(msg));

            await msg.Channel.SendMessageAsync(replyMessage, messageReference: new MessageReference(msg.Id));
            await Task.CompletedTask;
        }
    }
}
