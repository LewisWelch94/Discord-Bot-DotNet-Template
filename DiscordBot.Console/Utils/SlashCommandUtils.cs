using Discord;
using Discord.WebSocket;

namespace DiscordBot.Console.Utils
{
    public static class SlashCommandUtils
    {
        public static T? GetSlashCommandDataOption<T>(this SocketSlashCommand command, string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));
            var value = command.Data.Options.Where(x => x.Name == key).FirstOrDefault();
            return value == null ? default : (T)value.Value;
        }
    }
}

