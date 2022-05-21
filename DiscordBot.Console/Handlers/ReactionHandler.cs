using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.Handlers
{
    public class ReactionHandler : IDiscordHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly Cacheable<IUserMessage, ulong> _msg;
        private readonly Cacheable<IMessageChannel, ulong> _channel;
        private readonly SocketReaction _reaction;
        private readonly bool _isReaction;

        public ReactionHandler(DiscordSocketClient client, Cacheable<IUserMessage, ulong> msg, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction, bool isReaction)
        {
            _client = client;
            _msg = msg;
            _channel = channel;
            _reaction = reaction;
            _isReaction = isReaction;
        }

        public async void ProcessAsync()
        {
            var reaction = new InterfaceUtils<IDiscordReaction>().GetClasses().Where(x => x.IsActive & x.Emojis().Contains(_reaction.Emote)).FirstOrDefault();

            if (reaction == null)
            {
                return;
            }

            IMessage msg = _msg.HasValue ? _msg.Value : await _msg.GetOrDownloadAsync();
            if (!msg.Author.IsBot) return;

            IChannel channel = _channel.HasValue ? _channel.Value : await _channel.GetOrDownloadAsync();

            if (_isReaction)
            {
                await reaction.ExecuteReactionAdded(_client, msg, channel, _reaction);
                await Task.CompletedTask;
                return;
            }

            if (!_isReaction)
            {
                await reaction.ExecuteReactionRemoved(_client, msg, channel, _reaction);
                await Task.CompletedTask;
                return;
            }

            throw new Exception("Reaction was not caught properly");
        }
    }
}
