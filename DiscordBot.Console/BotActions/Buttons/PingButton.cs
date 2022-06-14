using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.BotActions.Buttons
{
    public class PingButton : IButton
    {
        public string CustomId() => "ping-button";

        public ButtonBuilder Component()
        {
            return new ButtonBuilder
            {
                CustomId = CustomId(),
                Label = "Ping Button",
                Style = ButtonStyle.Secondary
            };
        }

        public async Task Execute(DiscordSocketClient client, SocketMessageComponent cmd)
        {
            await cmd.RespondAsync($"<@{cmd.User.Id}> has pressed the ping button!");
            await Task.CompletedTask;
        }
    }
}
