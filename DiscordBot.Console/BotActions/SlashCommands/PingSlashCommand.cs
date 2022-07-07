using Discord;
using Discord.WebSocket;
using DiscordBot.Console.Interfaces;
using DiscordBot.Console.Utils;

namespace DiscordBot.Console.BotActions.SlashCommands
{
    public class PingMessageCommand : ISlashCommand
    {
        public string Name() => "Ping";
        public string Description() => "Replies with Pong";

        public List<SlashCommandOptionBuilder>? Options()
        {
            List<SlashCommandOptionBuilder> options = new List<SlashCommandOptionBuilder>();

            options.Add(new SlashCommandOptionBuilder()
            {
                Name = "user",
                Description = "Tag someone that you want to ping",
                Type = ApplicationCommandOptionType.User,
                IsRequired = true
            });

            return options;
        }

        public async Task Execute(DiscordSocketClient client, SocketSlashCommand command)
        {
            var userFromOption = command.GetSlashCommandDataOption<IUser>("users");
            //await command.RespondAsync(embed: MessageUtils.EmbedFromJson("ping"));
            //await command.RespondAsync($"Pong <@{userFromOption!.Id}>", components: ComponentUtils.GetComponentFromButtonCustomId("ping-button"));
            //await command.RespondAsync($"Pong <@{userFromOption!.Id}>", components: ComponentUtils.GetComponentFromMenuCustomId("ping-menu"));
            //await command.RespondWithModalAsync(ComponentUtils.GetModalFromCustomId("ping-modal"));
            await Task.CompletedTask;
        }
    }
}
