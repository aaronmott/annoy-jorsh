using System;
using System.Linq;
using System.Threading.Tasks;
using annoyjorsh.Services;
using Microsoft.Extensions.DependencyInjection;
using annoyjorsh.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;
using Discord.WebSocket;
using Tweetinvi.Streaming;

namespace annoyjorsh
{
    class Program
    {

        public static IConfiguration Configuration;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            var services = ConfigureServices();
            var disc = services.GetRequiredService<DiscordSocketClient>();
            disc.StartDiscord(Configuration["DiscordToken"]);
            var twit = services.GetRequiredService<IFilteredStream>();
            twit.startTwitterStream(disc);
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(Configuration);
            services.AddDiscord();
            services.AddTwitter(Configuration);
            return services.BuildServiceProvider();            
        }
    }
}
