﻿using Discord.WebSocket;
using DiscordBot.Console.Triggers;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Handlers
{
    public class MessageRecievedHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly SocketMessage _message;
        private IEnumerable<ITrigger>? Triggers { get; set; }

        public MessageRecievedHandler(DiscordSocketClient client, SocketMessage message)
        {
            _client = client;
            _message = message;
        }

        public async void ProcessAsync()
        {
            if (_message == null) return;
            if (_message.Author.IsBot) return;

            if (Triggers == null) Triggers = new InterfaceUtils<ITrigger>().GetClasses();

            var triggers = Triggers.Where(x => x.Triggered(_client, _message) && x.IsActive);

            if (!triggers.Any()) return;

            foreach(var trigger in triggers)
            {
                await trigger.Execute(_client, _message);
            }
        }
    }
}
