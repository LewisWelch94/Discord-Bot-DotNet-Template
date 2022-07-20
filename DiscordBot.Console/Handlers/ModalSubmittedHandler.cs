using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Console.Handlers
{
    public class ModalSubmittedHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly SocketModal _modal;
        private IEnumerable<IDiscordModal>? DiscordModals { get; set; }

        public ModalSubmittedHandler(DiscordSocketClient client, SocketModal modal)
        {
            _client = client;
            _modal = modal;
        }

        public async Task ProcessAsync()
        {
            if (DiscordModals == null) DiscordModals = new InterfaceUtils<IDiscordModal>().GetClasses();

            var modal = DiscordModals.Where(x => x.IsActive && x.CustomId() == _modal.Data.CustomId).FirstOrDefault();

            if (modal == null)
            {
                await _modal.RespondAsync("This modal has been deactivated", ephemeral: true);
                await Task.CompletedTask;
                return;
            }

            await modal.Execute(_client, _modal);
            await Task.CompletedTask;
        }
    }
}
