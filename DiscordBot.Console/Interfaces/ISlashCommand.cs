using Discord;
using Discord.WebSocket;

namespace DiscordBot.Console.Interfaces
{
    public interface ISlashCommand : IBotAction
    {
        string Name();
        string Description();
        bool IsDMEnabled() => false;
        bool isDefaultPermission() => true;
        List<SlashCommandOptionBuilder>? Options() => null;
        Task Execute(DiscordSocketClient client, SocketSlashCommand command);
    }
}
