namespace DiscordBot.Console.Utils
{
    using Discord;
    using Discord.WebSocket;
    using DiscordBot.Console.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class RestCommandUtils
    {
        private readonly ILogger<RestCommandUtils> _logger;
        private readonly IConfiguration _config;

        public RestCommandUtils(ILogger<RestCommandUtils> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task RegisterSlashCommands(DiscordSocketClient client, SocketGuild? guild = null)
        {
            var commands = new InterfaceUtils<ISlashCommand>().GetClasses().Where(x => x.IsActive);

            if (!commands.Any())
            {
                _logger.LogInformation("No isActive Commands found, aborting");
                await Task.CompletedTask;
                return;
            }

            _logger.LogInformation("Clearing all commands");

            if (guild != null)
            {
                await guild.DeleteApplicationCommandsAsync();
            }

            await client.Rest.DeleteAllGlobalCommandsAsync();

            foreach(var command in commands)
            {
                var guildCommand = new SlashCommandBuilder
                {
                    Name = command.Name().ToLower(),
                    Description = command.Description(),
                    IsDMEnabled = command.IsDMEnabled,
                    IsDefaultPermission = command.IsDefaultPermission,
                    Options = command.Options(),
                };

                try
                {
                    if (guild != null)
                    {
                        await client.Rest.CreateGuildCommand(guildCommand.Build(), guild.Id);
                        _logger.LogInformation($"Slash Command Name locally: {command.Name()} registered");
                    }
                    else
                    {
                        await client.Rest.CreateGlobalCommand(guildCommand.Build());
                        _logger.LogInformation($"Slash Command Name globally: {command.Name()} registered");
                    }   
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Registering a Guild Command", ex);
                    throw;
                }
            }

            await Task.CompletedTask;
        }

        public async Task RegisterMessageCommands(DiscordSocketClient client, SocketGuild? guild = null)
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
                    if (guild != null)
                    {
                        await client.Rest.CreateGuildCommand(messageCommand.Build(), guild.Id);
                        _logger.LogInformation($"Message Command Name locally: {command.Name()} registered");
                    }
                    else
                    {
                        await client.Rest.CreateGlobalCommand(messageCommand.Build());
                        _logger.LogInformation($"Message Command Name globally: {command.Name()} registered");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Registering a Message Command", ex);
                    throw;
                }
            }

            await Task.CompletedTask;
        }

        public async Task RegisterUserCommands(DiscordSocketClient client, SocketGuild? guild = null)
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
                var userCommand = new UserCommandBuilder
                {
                    Name = command.Name()
                };

                try
                {
                    if (guild != null)
                    {
                        await client.Rest.CreateGuildCommand(userCommand.Build(), guild.Id);
                        _logger.LogInformation($"User Command Name locally: {command.Name()} registered");
                    }
                    else
                    {
                        await client.Rest.CreateGlobalCommand(userCommand.Build());
                        _logger.LogInformation($"User Command Name globally: {command.Name()} registered");
                    }
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
