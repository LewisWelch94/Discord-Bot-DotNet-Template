using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Console.Interfaces
{
    public interface IUserCommand : IBotAction
    {
        string Name();
        Task Execute(DiscordSocketClient client, SocketUserCommand cmd);
    }
}
