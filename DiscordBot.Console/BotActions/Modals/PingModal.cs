using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.BotActions.Modals
{
    public class PingModal : IDiscordModal
    {
        public string CustomId() => "ping-modal";

        public ModalBuilder Component()
        {
            return new ModalBuilder()
            {
                CustomId = CustomId(),
                Title = "Ping yourself a message",
            }.AddTextInput("Message", "ping-input");
        }

        public async Task Execute(DiscordSocketClient client, SocketModal modal)
        {
            var msg = modal.Data.Components.ToList().First(x => x.CustomId == "ping-input").Value;
            await modal.RespondAsync($"I got your message: {msg}");
        }
    }
}
