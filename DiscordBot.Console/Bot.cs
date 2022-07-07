﻿using Discord;
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

        public Bot(ILogger<Bot> botLogger, ILogger<RestCommandUtils> restCommandLogger, IConfiguration config, string[] args)
        {
            var discordConfig = new DiscordSocketConfig 
            { 
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
            };

            _client = new DiscordSocketClient(discordConfig);
            _botLogger = botLogger;
            _restCommandLogger = restCommandLogger;
            _config = config;
            _args = args;
        }

        public async Task Start()
        {
            var token = _config["client:token"];

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
            _client.ButtonExecuted += ButtonExecuted;
            _client.SelectMenuExecuted += SelectMenuExecuted;
            _client.ModalSubmitted += ModalSubmitted;
            _client.ReactionAdded += (user, channel, reaction) => Reaction(user, channel, reaction, true);
            _client.ReactionRemoved += (user, channel, reaction) => Reaction(user, channel, reaction, false);
            _client.UserJoined += UserJoined;
            _client.UserLeft += UserLeft;
        }

        private async Task onReady()
        {
            _botLogger.LogInformation($"Bot login as: {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}");

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
            new MessageRecievedHandler(_client, msg).ProcessAsync();
            await Task.CompletedTask;
        }

        private async Task SlashCommandExecuted(SocketSlashCommand cmd)
        {
            new SlashCommandHandler(_client, cmd).ProcessAsync();
            await Task.CompletedTask;
        }

        private async Task MessageCommandExecuted(SocketMessageCommand cmd)
        {
            new MessageCommandHandler(_client, cmd).ProcessAsync();
            await Task.CompletedTask;
        }

        private async Task UserCommandExecuted(SocketUserCommand cmd)
        {
            new UserCommandHandler(_client, cmd).ProcessAsync();
            await Task.CompletedTask;
        }

        private async Task ButtonExecuted(SocketMessageComponent btn)
        {
            new ButtonHandler(_client, btn).ProcessAsync();
            await Task.CompletedTask;
        }

        private async Task SelectMenuExecuted(SocketMessageComponent menu)
        {
            new SelectMenuHandler(_client, menu).ProcessAsync();
            await Task.CompletedTask;
        }

        private async Task ModalSubmitted(SocketModal modal)
        {
            new ModalSubmittedHandler(_client, modal).ProcessAsync();
            await Task.CompletedTask;
        }

        private async Task Reaction(Cacheable<IUserMessage, ulong> user, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction, bool isReaction)
        {
            new ReactionHandler(_client, user, channel, reaction, isReaction).ProcessAsync();
            await Task.CompletedTask;
        }

        private async Task UserJoined(SocketGuildUser user)
        {
            new UserJoinedHandler(_client, user).ProcessAsync();
            await Task.CompletedTask;
        }

        private async Task UserLeft(SocketGuild guild, SocketUser user)
        {
            new UserLeftHandler(_client, guild, user).ProcessAsync();
            await Task.CompletedTask;
        }
    }
}