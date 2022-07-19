using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface IDiscordJob : IBotAction
    {
        string Cron();
        Task Execute(DiscordSocketClient client);
    }
}
