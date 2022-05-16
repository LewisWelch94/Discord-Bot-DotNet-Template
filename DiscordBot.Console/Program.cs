using DiscordBot.Console.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Console; 

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

        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                Services.ConfigureServices(services);

            }).Build();

        var botLogger = loggerFactory.CreateLogger<Bot>();
        var restCommandLogger = loggerFactory.CreateLogger<RestCommandUtils>();
        config = builder.Build();
        await new Bot(botLogger, restCommandLogger, config).Start(args);
    }
}