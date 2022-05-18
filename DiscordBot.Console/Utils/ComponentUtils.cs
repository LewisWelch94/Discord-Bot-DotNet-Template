using Discord;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.Utils
{
    public static class ComponentUtils
    {
        public static MessageComponent GetComponentFromButtonCustomId(string btnId)
        {
            var btn = new InterfaceUtils<IButton>().GetClasses().Where(x => x.IsActive && x.CustomId() == btnId).FirstOrDefault();
            if (btn == null) throw new Exception($"Button was not found with the ID: {btnId}");
            return new ComponentBuilder().WithButton(btn.Component()).Build();
        }
    }
}
