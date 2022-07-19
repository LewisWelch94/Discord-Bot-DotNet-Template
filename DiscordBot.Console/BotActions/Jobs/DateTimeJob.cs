using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Constants;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.BotActions.Jobs
{
    public class DateTimeJob : IDiscordJob
    {
        public bool IsActive => false;
        public string Cron() => "* * * * *";

        public async Task Execute(DiscordSocketClient client)
        {
            var dateTimeChannel = await client.GetChannelAsync(ChannelConstants.DATE_TIME_JOB) as IMessageChannel;
            if (dateTimeChannel is null) return;
            await dateTimeChannel.SendMessageAsync($"DateTime Job is activated! This should happen every min: DateTime: {DateTime.UtcNow}");
        }
    }
}
