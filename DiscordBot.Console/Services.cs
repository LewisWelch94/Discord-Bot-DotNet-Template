using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.Console
{
    public class Services
    {
        public static IServiceProvider? provider;

        public static void ConfigureServices(IServiceCollection services)
        { 
            provider = services.BuildServiceProvider();
        }
    }
}
