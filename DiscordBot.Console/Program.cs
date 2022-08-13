namespace DiscordBot.Console
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        public static ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddFilter("System", LogLevel.Debug).AddConsole());
        public IConfiguration? config { get; set; }

        static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            config = builder.Build();

            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    Services.ConfigureServices(services, config);
                }).Build();

            var bot = host.Services.GetRequiredService<Bot>();
            await bot.Start(args);
        }
    }
}