using Discord;

namespace DiscordBot.Console.Interfaces
{
    public interface ITrigger : IBotAction
    {
        bool Triggered(IDiscordClient client, IMessage msg);
        bool AllowBot => false;
        Task Execute(IDiscordClient client, IMessage msg);
    }
}
