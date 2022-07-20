using Discord;
using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface ISlashCommand : IDiscordCommand
    {
        string Description();
        List<SlashCommandOptionBuilder>? Options() => null;
        Task Execute(DiscordSocketClient client, SocketSlashCommand command);
    }
}
