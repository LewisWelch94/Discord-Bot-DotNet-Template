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

        public static MessageComponent GetComponentFromMenuCustomId(string menuId)
        {
            var menu = new InterfaceUtils<IMenu>().GetClasses().Where(x => x.IsActive && x.CustomId() == menuId).FirstOrDefault();
            if (menu == null) throw new Exception($"Menu was not found with the ID: {menuId}");
            return new ComponentBuilder().WithSelectMenu(menu.Component()).Build();
        }

        public static Modal GetModalFromCustomId(string modalId)
        {
            var modal = new InterfaceUtils<IDiscordModal>().GetClasses().Where(x => x.IsActive && x.CustomId() == modalId).FirstOrDefault();
            if (modal == null) throw new Exception($"Menu was not found with the ID: {modalId}");
            return modal.Component().Build();
        }
    }
}
