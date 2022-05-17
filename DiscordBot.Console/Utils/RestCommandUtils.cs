using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Console.Utils
{
    public class RestCommandUtils
    {
        private readonly ILogger<RestCommandUtils> _logger;
        private readonly IConfiguration _config;

        public RestCommandUtils(ILogger<RestCommandUtils> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task RegisterSlashCommands(DiscordSocketClient client)
        {
            var commands = new InterfaceUtils<ISlashCommand>().GetClasses().Where(x => x.IsActive);

            if (!commands.Any())
            {
                _logger.LogInformation("No isActive Commands found, aborting");
                await Task.CompletedTask;
                return;
            }

            _logger.LogInformation("Clearing all commands");
            await client.Guilds.First(x => x.Id == Convert.ToUInt64(_config["guild:id"])).DeleteApplicationCommandsAsync();
            await client.Rest.DeleteAllGlobalCommandsAsync();

            foreach(var command in commands)
            {
                var guildCommand = new SlashCommandBuilder
                {
                    Name = command.Name().ToLower(),
                    Description = command.Description(),
                    IsDMEnabled = command.IsDMEnabled(),
                    IsDefaultPermission = command.isDefaultPermission(),
                    Options = command.Options(),
                };

                try
                {
                    await client.Rest.CreateGuildCommand(guildCommand.Build(), Convert.ToUInt64(_config["guild:id"]));
                    _logger.LogInformation($"Slash Command Name: {command.Name()} registered");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Registering a Guild Command", ex);
                    throw;
                }
            }

            await Task.CompletedTask;
        }

        public async Task RegisterMessageCommands(DiscordSocketClient client)
        {
            var commands = new InterfaceUtils<IMessageCommand>().GetClasses().Where(x => x.IsActive);

            if (!commands.Any())
            {
                _logger.LogInformation("No isActive Message Commands found, aborting");
                await Task.CompletedTask;
                return;
            }

            foreach (var command in commands)
            {
                var messageCommand = new MessageCommandBuilder
                {
                    Name = command.Name()
                };

                try
                {
                    await client.Rest.CreateGuildCommand(messageCommand.Build(), Convert.ToUInt64(_config["guild:id"]));
                    _logger.LogInformation($"Message Command Name: {command.Name()} registered");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Registering a Message Command", ex);
                    throw;
                }
            }

            await Task.CompletedTask;
        }

        public async Task RegisterUserCommands(DiscordSocketClient client)
        {
            var commands = new InterfaceUtils<IUserCommand>().GetClasses().Where(x => x.IsActive);

            if (!commands.Any())
            {
                _logger.LogInformation("No isActive User Commands found, aborting");
                await Task.CompletedTask;
                return;
            }

            foreach (var command in commands)
            {
                var messageCommand = new UserCommandBuilder
                {
                    Name = command.Name()
                };

                try
                {
                    await client.Rest.CreateGuildCommand(messageCommand.Build(), Convert.ToUInt64(_config["guild:id"]));
                    _logger.LogInformation($"User Command Name: {command.Name()} registered");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Registering a User Command", ex);
                    throw;
                }
            }

            await Task.CompletedTask;
        }
    }
}
