using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.BotActions.Menu
{
    public class PingMenu : IMenu
    {
        public string CustomId() => "ping-menu";

        public SelectMenuBuilder Component()
        {
            var option = new List<SelectMenuOptionBuilder>
            {
                new SelectMenuOptionBuilder
                {
                    Label = "Ping me",
                    Value = "me"
                }
            };

            return new SelectMenuBuilder
            {
                CustomId = CustomId(),
                MinValues = 1,
                MaxValues = 1,
                Placeholder = "Select a ping",
                Options = option
            };
        }

        public async Task Execute(DiscordSocketClient client, SocketMessageComponent cmd)
        {
            await cmd.RespondAsync($"You have selected {cmd.Data.Values.First()}");
        }
    }
}
