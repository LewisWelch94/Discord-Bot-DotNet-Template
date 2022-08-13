namespace DiscordBot.Console
{
    using Discord;
    using Discord.WebSocket;
    using DiscordBot.Console.Handlers;
    using DiscordBot.Console.Utils;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class Bot
    {
        private DiscordSocketClient _client;
        private readonly ILogger<Bot> _botLogger;
        private readonly ILogger<RestCommandUtils> _restCommandLogger;
        private readonly IConfiguration _config;
        private string[]? _args;

        public Bot(DiscordSocketClient client, IConfiguration config, ILogger<Bot> botLogger, ILogger<RestCommandUtils> restCommandLogger)
        {
            _client = client;
            _config = config;
            _botLogger = botLogger;
            _restCommandLogger = restCommandLogger;
        }

        public async Task Start(string[] args)
        {
            _args = args;

            var token = _config["client:token"];
            await _client.LoginAsync(TokenType.Bot, token);

            _client.Ready += onReady;
            _client.MessageReceived += MessageRecieved;
            _client.SlashCommandExecuted += SlashCommandExecuted;
            _client.MessageCommandExecuted += MessageCommandExecuted;
            _client.UserCommandExecuted += UserCommandExecuted;
            _client.ButtonExecuted += ButtonExecuted;
            _client.SelectMenuExecuted += SelectMenuExecuted;
            _client.ModalSubmitted += ModalSubmitted;
            _client.ReactionAdded += (user, channel, reaction) => Reaction(user, channel, reaction, true);
            _client.ReactionRemoved += (user, channel, reaction) => Reaction(user, channel, reaction, false);
            _client.UserJoined += UserJoined;
            _client.UserLeft += UserLeft;

            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task onReady()
        {
            var guild = _client.GetGuild(Convert.ToUInt64(_config["guild:id"]));
            _botLogger.LogInformation($"Bot login as: {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}\n" +
                                      $"Fully configured to Guild: {guild.Name}");

            switch (_args![0])
            {
                case "register":
                    await new RestCommandUtils(_restCommandLogger, _config).RegisterSlashCommands(_client, guild);
                    await new RestCommandUtils(_restCommandLogger, _config).RegisterMessageCommands(_client, guild);
                    await new RestCommandUtils(_restCommandLogger, _config).RegisterUserCommands(_client, guild);
                    Environment.Exit(0);
                    return;

                case "global":
                    await new RestCommandUtils(_restCommandLogger, _config).RegisterSlashCommands(_client);
                    await new RestCommandUtils(_restCommandLogger, _config).RegisterMessageCommands(_client);
                    await new RestCommandUtils(_restCommandLogger, _config).RegisterUserCommands(_client);
                    Environment.Exit(0);
                    return;

                default:
                    await LoadJobs();
                    break;
            }
        }

        private async Task MessageRecieved(SocketMessage msg) => await new MessageRecievedHandler(msg).ProcessAsync();
        private async Task SlashCommandExecuted(SocketSlashCommand cmd) => await new SlashCommandHandler(_client, cmd).ProcessAsync();
        private async Task MessageCommandExecuted(SocketMessageCommand cmd) => await new MessageCommandHandler(_client, cmd).ProcessAsync();
        private async Task UserCommandExecuted(SocketUserCommand cmd) => await new UserCommandHandler(_client, cmd).ProcessAsync();
        private async Task ButtonExecuted(SocketMessageComponent btn) => await new ButtonHandler(_client, btn).ProcessAsync();
        private async Task SelectMenuExecuted(SocketMessageComponent menu) => await new SelectMenuHandler(_client, menu).ProcessAsync();
        private async Task ModalSubmitted(SocketModal modal) => await new ModalSubmittedHandler(_client, modal).ProcessAsync();
        private async Task UserJoined(SocketGuildUser user) => await new UserJoinedHandler(_client, user).ProcessAsync();
        private async Task UserLeft(SocketGuild guild, SocketUser user) => await new UserLeftHandler(_client, guild, user).ProcessAsync();
        private async Task LoadJobs() => await new JobHandler(_client).ProcessAsync();
        private async Task Reaction(Cacheable<IUserMessage, ulong> user, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction, bool isReaction)
            => await new ReactionHandler(_client, user, channel, reaction, isReaction).ProcessAsync();
    }
}