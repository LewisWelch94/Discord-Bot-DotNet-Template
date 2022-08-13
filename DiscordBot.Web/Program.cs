namespace DiscordBot.Web
{
    using DiscordBot.Console;

    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container including Bot
            builder.Services.AddControllersWithViews();
            Services.ConfigureServices(builder.Services, config);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Grab bot from the services
            var bot = app.Services.GetRequiredService<Bot>();
            Parallel.Invoke(async () => await bot.Start(args), () => app.Run());
            await Task.CompletedTask;
        }
    }
}