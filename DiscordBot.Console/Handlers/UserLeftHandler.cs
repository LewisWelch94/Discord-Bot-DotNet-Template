using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Constants;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.Handlers
{
    public class UserLeftHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly SocketGuild _guild;
        private readonly SocketUser _user;
        private static bool IsActive => false;

        public UserLeftHandler(DiscordSocketClient client, SocketGuild guild, SocketUser user)
        {
            _client = client;
            _guild = guild;
            _user = user;
        }

        public async Task ProcessAsync()
        {
            if (!IsActive) return;

            var userLogChannel = _guild.GetChannel(ChannelConstants.USER_LOG) as IMessageChannel;
            if (userLogChannel is null) return;
            await userLogChannel.SendMessageAsync($"{_user.Username}#{_user.Discriminator} has left the server! :(");
            await Task.CompletedTask;
        }
    }
}
