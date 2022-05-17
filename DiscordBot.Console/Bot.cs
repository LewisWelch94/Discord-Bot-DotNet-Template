using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Handlers;
using DiscordBot.Console.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Console
{
    public class Bot
    {
        private DiscordSocketClient _client;
        private readonly ILogger<Bot> _botLogger;
        private readonly ILogger<RestCommandUtils> _restCommandLogger;
        private readonly IConfiguration _config;
        private string[] _args;

        public Bot(ILogger<Bot> botLogger, ILogger<RestCommandUtils> restCommandLogger, IConfiguration config)
        {
            var discordConfig = new DiscordSocketConfig { MessageCacheSize = 100 };
            _client = new DiscordSocketClient(discordConfig);
            _botLogger = botLogger;
            _restCommandLogger = restCommandLogger;
            _config = config;
        }

        public async Task Start(string[] args)
        {
            var token = _config["client:token"];
            _args = args;

            await _client.LoginAsync(TokenType.Bot, token);

            registerListeners();
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private void registerListeners()
        {
            _client.Ready += onReady;
            _client.MessageReceived += MessageRecieved;
            _client.SlashCommandExecuted += SlashCommandExecuted;
            _client.MessageCommandExecuted += MessageCommandExecuted;
            _client.UserCommandExecuted += UserCommandExecuted;
        }

        private async Task onReady()
        {
            _botLogger.LogInformation("Bot has been connected");

            if (_args[0] == "register")
            {
                await new RestCommandUtils(_restCommandLogger, _config).RegisterSlashCommands(_client);
                await new RestCommandUtils(_restCommandLogger, _config).RegisterMessageCommands(_client);
                await new RestCommandUtils(_restCommandLogger, _config).RegisterUserCommands(_client);
                Environment.Exit(0);
                return;
            }
        }

        private async Task MessageRecieved(SocketMessage msg)
        {
            new MessageRecievedHandler(_client, msg).Process();
            await Task.CompletedTask;
        }

        private async Task SlashCommandExecuted(SocketSlashCommand cmd)
        {
            new SlashCommandHandler(_client, cmd).Process();
            await Task.CompletedTask;
        }

        private async Task MessageCommandExecuted(SocketMessageCommand cmd)
        {
            new MessageCommandHandler(_client, cmd).Process();
            await Task.CompletedTask;
        }

        private async Task UserCommandExecuted(SocketUserCommand cmd)
        {
            new UserCommandHandler(_client, cmd).Process();
            await Task.CompletedTask;
        }
    }
}
