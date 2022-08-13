namespace DiscordBot.Console
{
    using Discord;
    using Discord.WebSocket;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Services
    {
        public static IServiceProvider? provider;

        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                GatewayIntents = GatewayIntents.All
            }));
            provider = services.BuildServiceProvider();
            services.AddSingleton<Bot>();
            provider = services.BuildServiceProvider();
        }
    }
}
