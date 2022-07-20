namespace DiscordBot.Console.Interfaces
{
    public interface IDiscordCommand : IBotAction
    {
        string Name();
        bool IsDefaultPermission => true;
        bool IsDMEnabled => false;
    }
}
