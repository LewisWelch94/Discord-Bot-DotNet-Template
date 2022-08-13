using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Handlers
{
    public class MessageRecievedHandler : IDiscordHandler
    {
        private readonly SocketMessage socketMessage;
        private IEnumerable<ITrigger>? Triggers { get; set; }

        public MessageRecievedHandler(SocketMessage message)
        {
            socketMessage = message;
        }

        public async Task ProcessAsync()
        {
            if (socketMessage == null) return;
            if (Triggers == null) Triggers = new InterfaceUtils<ITrigger>().GetClasses();

            IEnumerable<ITrigger> triggers;

            if (socketMessage.Author.IsBot)
            {
                triggers = Triggers.Where(x => x.AllowBot && x.Triggered(socketMessage) && x.IsActive);
            }
            else
            {
                triggers = Triggers.Where(x => !x.AllowBot && x.Triggered(socketMessage) && x.IsActive);
            }

            if (!triggers.Any()) return;

            foreach(var trigger in triggers)
            {
                await trigger.Execute(socketMessage);
            }

            await Task.CompletedTask;
        }
    }
}
