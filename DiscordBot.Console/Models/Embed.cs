using Discord;
using Newtonsoft.Json;

namespace DiscordBot.Console.Models
{
    public class Embed : EmbedBuilder
    {
        public new string? Color { get; set; }
    }
}
