using Cronos;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Handlers
{
    public class JobHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private IEnumerable<IDiscordJob>? DiscordJobs { get; set; }

        public JobHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task ProcessAsync()
        {
            if (DiscordJobs == null) DiscordJobs = new InterfaceUtils<IDiscordJob>().GetClasses();

            var discordJobs = DiscordJobs.Where(x => x.IsActive);

            foreach(var discordJob in discordJobs)
            {
                var timer = new System.Timers.Timer(NextOccurrenceSeconds(discordJob.Cron()));
                timer.Enabled = true;
                timer.Elapsed += async (e, s) =>
                {
                    await discordJob.Execute(_client);
                    timer.Interval = NextOccurrenceSeconds(discordJob.Cron());
                };

                timer.Start();
            }

            await Task.CompletedTask;
        }

        private static double NextOccurrenceSeconds(string cron)
        {
            return (CronExpression.Parse(cron).GetNextOccurrence(DateTime.UtcNow)!.Value - DateTime.UtcNow).TotalSeconds * 1000;
        }
    }
}
