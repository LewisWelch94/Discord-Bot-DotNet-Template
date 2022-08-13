using Discord;

namespace DiscordBot.Console.Interfaces
{
    public interface ITrigger : IBotAction
    {
        bool Triggered(IMessage msg);
        bool AllowBot => false;
        Task Execute(IMessage msg);
    }
}
