using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Constants;
using DiscordBot.Console.Interfaces;

namespace DiscordBot.Console.Handlers
{
    public class UserJoinedHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly SocketGuildUser _user;
        private static bool IsActive => false;

        public UserJoinedHandler(DiscordSocketClient client, SocketGuildUser user)
        {
            _client = client;
            _user = user;
        }

        public async Task ProcessAsync()
        {
            if (!IsActive) return;

            var userLogChannel = await _client.GetChannelAsync(ChannelConstants.USER_LOG) as IMessageChannel;
            if (userLogChannel is null) return;
            await userLogChannel.SendMessageAsync($"{_user.Username}#{_user.Discriminator} has joined the server!");
            await Task.CompletedTask;
        }
    }
}
