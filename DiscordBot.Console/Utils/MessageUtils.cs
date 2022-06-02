using Discord;
using DiscordBot.Console.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace DiscordBot.Console.Utils
{
    public static class MessageUtils
    {
        public async static Task Reply(IMessage msg, string replyMessage)
        {
            if (msg == null) throw new ArgumentNullException(nameof(msg));

            await msg.Channel.SendMessageAsync(replyMessage, messageReference: new MessageReference(msg.Id));
            await Task.CompletedTask;
        }

        public static Discord.Embed EmbedFromJson(string location, Dictionary<string, string>? variables = null)
        {
            JObject wholeMessage = JObject.Parse(File.ReadAllText("messages.json"));
            if (wholeMessage == null) throw new ArgumentNullException(nameof(wholeMessage));

            var message = wholeMessage.SelectToken(location)!.ToString();

            if (variables != null)
            {
                foreach (var variable in variables)
                {
                    message = message.Replace(variable.Key, variable.Value);
                }
            }

            var messageObj = JsonConvert.DeserializeObject<Models.Embed>(message);
            if (messageObj!.Color != null)
            {
                var color = messageObj.Color.Replace("#", "0x");
                messageObj.WithColor(new Color((uint)Convert.ToInt32(color, 16)));
            }

            return messageObj.Build();
        }
    }
}
